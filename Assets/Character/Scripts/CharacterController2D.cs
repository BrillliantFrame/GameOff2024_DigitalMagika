using UnityEngine;
using UnityEngine.InputSystem;

enum SkillState
{
    READY, ACTIVE, COOLDOWN
}

public class CharacterController2D : MonoBehaviour
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

    //Movement
    private float _horizontalMovement;
    private float _originalGravity = 0f;
    private bool _isFacingRight = true;
    private bool _isDashing = false;

    //Skill Cooldown
    private float _cooldownTime = 0.0f;
    private float _activeTime = 0.0f;
    private SkillState _currentState = SkillState.READY;

    private void Awake()
    {
        _originalGravity = _rigidBody.gravityScale;
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
                        _rigidBody.linearVelocity = new Vector2(0f, 0f);
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
                }
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

        if (IsGrounded())
        {
            _animator.SetBool("IsGrounded", true);
            _rigidBody.linearVelocity = new Vector2(_horizontalMovement * _movementSpeed, _rigidBody.linearVelocity.y);
        }
        else
        {
            _animator.SetBool("IsGrounded", false);
            _rigidBody.linearVelocity = new Vector2(_rigidBody.linearVelocity.x, _rigidBody.linearVelocity.y);
        }
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
            _rigidBody.linearVelocity = new Vector2(transform.localScale.x * _dashingPower, 0f);
            useSkill(_dashDurationTime);
            AkSoundEngine.PostEvent("Player_Dash", gameObject);
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
            Vector3 localScale = transform.localScale;
            _isFacingRight = !_isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void useSkill(float activeTime)
    {
        _currentState = SkillState.ACTIVE;
        _activeTime = activeTime;
    }
}