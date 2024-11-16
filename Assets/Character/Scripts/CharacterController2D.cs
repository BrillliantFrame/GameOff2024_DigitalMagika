using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : Singleton<CharacterController2D>
{

    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _movementSpeed = 8f;
    [SerializeField] private float _jumpingPower = 16f;
    [SerializeField] private float _dashingPower = 24f;

    //[SerializeField] private TrailRenderer tr;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _skillCooldownTime = 10f;
    [SerializeField] private float _dashDurationTime = 0.5f;

    [SerializeField] private PlayerInput _playerInput;

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

    protected override void Awake()
    {
        base.Awake();
        _originalGravity = _rigidBody.gravityScale;
        _playerLives = 3;
        _respawnPoint = _rigidBody.transform.position; //Should be set as the door location when entering a room
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

        if (isGrounded)
            _rigidBody.linearVelocity = new Vector2(_horizontalMovement * _movementSpeed, _rigidBody.linearVelocity.y);
        else
            _rigidBody.linearVelocity = new Vector2(_rigidBody.linearVelocity.x, _rigidBody.linearVelocity.y);

        if (_isGrounded != isGrounded)
        {
            _isGrounded = isGrounded;
            if (_isGrounded)
                AkSoundEngine.PostEvent("Player_Landing", gameObject);
        }
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
        //Jumping while grounded
        if (context.performed && IsGrounded())
        {
            Flip();
            _rigidBody.linearVelocity = new Vector2(_horizontalMovement * _movementSpeed, _jumpingPower);
            _animator.SetTrigger("Jump");
            AkSoundEngine.PostEvent("Player_Jump", gameObject);
        }

        if (context.performed && !IsGrounded() && _currentState == SkillState.READY)
        {
            Flip();
            useSkill(0f);
            _rigidBody.linearVelocity = new Vector2(_horizontalMovement * _movementSpeed, _jumpingPower);
            _animator.SetTrigger("Jump");
            AkSoundEngine.PostEvent("Player_DoubleJump", gameObject);
        }

        if (context.canceled && _rigidBody.linearVelocity.y > 0f)
        {
            _rigidBody.linearVelocity = new Vector2(_horizontalMovement * _movementSpeed, _rigidBody.linearVelocity.y * 0.5f);
            _animator.SetTrigger("Jump");
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && _currentState == SkillState.READY)
        {
            Flip();
            _isDashing = true;
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
        StartCoroutine(onDamage());
    }

    public void TeleportCharacter(Vector2 position)
    {
        _rigidBody.transform.gameObject.SetActive(false);
        _rigidBody.transform.position = position;
        _respawnPoint = position;
        _rigidBody.transform.gameObject.SetActive(true);
        //_isGrounded = false;
    }

    public void DisableInput()
    {
        _playerInput.enabled = false;
    }

    public void EnableInput()
    {
        _playerInput.enabled = true;
    }

    private IEnumerator onDamage()
    {
        _rigidBody.transform.gameObject.SetActive(false);
        OnPlayerDamaged?.Invoke();
        _playerLives--;
        if (_playerLives > 0)
        {
            _rigidBody.transform.position = _respawnPoint;
            yield return new WaitForSeconds(2);
            _rigidBody.transform.gameObject.SetActive(true);
        }
        else
        {
            OnPlayerLivesEnded?.Invoke();
        }
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