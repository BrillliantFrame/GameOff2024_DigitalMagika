using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : Singleton<CharacterController2D>
{

    [Header("Physics")]
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _movementSpeed = 8f;
    [Range(0f, 1f)]
    [SerializeField] private float _jumpingHorizontalPercent = 0.5f;
    [SerializeField] private float _jumpingPower = 16f;
    [SerializeField] private float _dashingPower = 24f;
    [SerializeField] private PlayerInput _playerInput;

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterAnimationEvents _characterAnimationEvents;

    [Header("Skills")]
    [SerializeField] private float _skillCooldownTime = 10f;
    [SerializeField] private float _dashDurationTime = 0.5f;

    [Header("Particles and Trails")]
    [SerializeField] private ParticleSystem _dashParticleSystem;
    [SerializeField] private TrailRenderer _dashTrailRenderer;
    [SerializeField] private ParticleSystem _jumpParticleSystem;
    
    [Header("Cheats")]
    [SerializeField] private bool _isGodMode = false;

    //Movement
    private float _horizontalMovement;
    private float _originalGravity = 0f;
    private bool _isFacingRight = true;
    private bool _isDashing = false;
    private bool _isGrounded = false;

    //Skill Cooldown
    private float _cooldownTime = 0.0f;
    private float _activeTime = 0.0f;
    private SkillState _currentState = SkillState.READY;
    public event Action<float> OnCooldownTick;

    //Damage handling
    private int _playerLives = 0;
    private Vector2 _respawnPoint = Vector2.zero; //Should be set as the door location when entering a room
    //Damage events
    public event Action OnPlayerHealed;
    public event Action OnPlayerDamaged;
    public event Action OnPlayerLivesEnded;

    private bool _jumpPerformed = false;
    private bool _jumpReleased = false;

    private bool _invulnerable = false;

    protected override void Awake()
    {
        base.Awake();
        _originalGravity = _rigidBody.gravityScale;
        _playerLives = 3;
        _respawnPoint = _rigidBody.transform.position; //Should be set as the door location when entering a room
        _characterAnimationEvents.OnDeathAnimationEnd += onDeathAnimationEnd;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case SkillState.ACTIVE:
                if (_activeTime > 0)
                {
                    _activeTime -= Time.deltaTime;
                }
                else
                {
                    //Skill ended, so reset the state
                    if (_isDashing)
                    {
                        _rigidBody.gravityScale = _originalGravity;
                        _rigidBody.linearVelocity = new Vector2(_horizontalMovement * _movementSpeed, 0f);
                        _isDashing = false;
                        _animator.SetBool("IsDashing", false);
                        _dashParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                        _dashTrailRenderer.emitting = false;
                    }
                    _currentState = SkillState.COOLDOWN;
                    _cooldownTime = _skillCooldownTime;
                }
                break;
            case SkillState.COOLDOWN:
                if (_cooldownTime > 0)
                {
                    _cooldownTime -= Time.deltaTime;
                }
                else
                {
                    _currentState = SkillState.READY;
                    AkSoundEngine.PostEvent("Player_AbilityReady", gameObject);
                }
                OnCooldownTick?.Invoke(_cooldownTime);
                break;
        }

        if (_isDashing || !IsGrounded())
            return;

        Flip();
    }

    private void FixedUpdate()
    {
        if (_isDashing)
            return;

        bool isGrounded = IsGrounded();

        if (_jumpPerformed)
        {
            _jumpPerformed = false;
            if (isGrounded)
            {
                Flip();
                _rigidBody.linearVelocity = new Vector2(_horizontalMovement * _movementSpeed * _jumpingHorizontalPercent, _jumpingPower);
                _animator.SetTrigger("Jump");
                AkSoundEngine.PostEvent("Player_Jump", gameObject);
            }
            else if (_currentState == SkillState.READY)
            {
                Flip();
                useSkill(0f);
                _rigidBody.linearVelocity = new Vector2(_horizontalMovement * _movementSpeed * _jumpingHorizontalPercent, _jumpingPower);
                _animator.SetTrigger("Jump");
                _jumpParticleSystem.Play();
                AkSoundEngine.PostEvent("Player_DoubleJump", gameObject);
            }
        }
        else if (_jumpReleased && _rigidBody.linearVelocity.y > 0f)
        {
            _rigidBody.linearVelocity = new Vector2(_horizontalMovement * _movementSpeed * _jumpingHorizontalPercent, _rigidBody.linearVelocity.y * 0.5f);
            _animator.SetTrigger("Jump");
        }
        else
        {
            if (isGrounded)
                _rigidBody.linearVelocity = new Vector2(_horizontalMovement * _movementSpeed, _rigidBody.linearVelocity.y);
            else
                _rigidBody.linearVelocity = new Vector2(_rigidBody.linearVelocity.x, _rigidBody.linearVelocity.y);
        }

        _jumpReleased = false;

        if (_isGrounded != isGrounded)
        {
            _isGrounded = isGrounded;
            if (_isGrounded)
                AkSoundEngine.PostEvent("Player_Landing", gameObject);
        }
        _animator.SetFloat("VerticalSpeed", _rigidBody.linearVelocity.y);
        _animator.SetBool("IsGrounded", _isGrounded);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _horizontalMovement = context.ReadValue<Vector2>().x;

        if (_horizontalMovement != 0)
            _animator.SetBool("IsRunning", true);
        else
            _animator.SetBool("IsRunning", false);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _jumpPerformed = false;
            _jumpReleased = true;
        }
        else
        {
            _jumpPerformed = context.performed;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && _currentState == SkillState.READY)
        {
            Flip();
            _isDashing = true;
            _animator.SetBool("IsDashing", true);
            _dashParticleSystem.Play();
            _dashTrailRenderer.emitting = true;
            _originalGravity = _rigidBody.gravityScale;
            _rigidBody.gravityScale = 0f;
            _rigidBody.linearVelocity = new Vector2(_rigidBody.transform.localScale.x * _dashingPower, 0f);
            useSkill(_dashDurationTime);
            AkSoundEngine.PostEvent("Player_Dash", gameObject);
        }
    }

    public void ReceiveHealing(int healAmount)
    {
        _playerLives += healAmount;
        OnPlayerHealed?.Invoke();
    }

    public void OnDamage()
    {
        if (!_invulnerable)
        {
            DisableInput();
            _playerLives--;
            OnPlayerDamaged?.Invoke();
            if (_playerLives > 0)
            {
                _animator.SetBool("IsDead", true);
            }
            else
            {
                OnPlayerLivesEnded?.Invoke();
            }
        }
    }

    public void TeleportCharacter(Vector2 position)
    {
        _rigidBody.transform.gameObject.SetActive(false);
        _rigidBody.transform.position = position;
        _respawnPoint = position;
        _rigidBody.transform.gameObject.SetActive(true);
    }

    public void DisableInput()
    {
        _invulnerable = true;
        _playerInput.enabled = false;
    }

    public void EnableInput()
    {
        _playerInput.enabled = true;
        _invulnerable = false || _isGodMode;
    }

    private void onDeathAnimationEnd()
    {
        StartCoroutine(_onDeathAnimationEnd());
    }

    private IEnumerator _onDeathAnimationEnd()
    {
        _animator.SetBool("IsDead", false);
        _rigidBody.transform.gameObject.SetActive(false);
        _rigidBody.transform.position = _respawnPoint;
        yield return new WaitForSeconds(1);
        _rigidBody.transform.gameObject.SetActive(true);
        EnableInput();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    private void Flip()
    {
        if (_isFacingRight && _horizontalMovement < 0f || !_isFacingRight && _horizontalMovement > 0f)
        {
            Vector3 localScale = _rigidBody.transform.localScale;
            _isFacingRight = !_isFacingRight;
            localScale.x *= -1f;
            _rigidBody.transform.localScale = localScale;
        }
    }

    private void useSkill(float activeTime)
    {
        if (_currentState != SkillState.READY)
            return;
        _currentState = SkillState.ACTIVE;
        _activeTime = activeTime;
    }

    void OnDrawGizmos()
    {
        // Display the ground check radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, 0.2f);
    }
}