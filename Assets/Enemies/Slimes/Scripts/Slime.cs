using UnityEngine;

public class Slime : MonoBehaviour
{

    [SerializeField] private float _bowDrawTime = 1f;
    [SerializeField] private float _skillCooldownTime = 1f;
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _horizontalDetectionSize = 2f;
    [SerializeField] private Transform _projectileOrigin;

    private Animator _animator;

    //Skill Cooldown
    private float _cooldownTime = 0.0f;
    private float _drawTime = 0.0f;
    private SkillState _currentState = SkillState.READY;

    private int _shootDirection = 1;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_currentState == SkillState.READY)
        {
            if (checkForPlayer())
            {
                useSkill();
            }
        }
        else
        {
            switch (_currentState)
            {
                case SkillState.ACTIVE:
                    if (_drawTime > 0)
                    {
                        _drawTime -= Time.deltaTime;
                    }
                    else
                    {
                        _currentState = SkillState.COOLDOWN;
                        _cooldownTime = _skillCooldownTime;
                        shootArrow();
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
                        Debug.Log("Ready to shoot");
                    }
                    break;
            }
        }
    }

    private bool checkForPlayer()
    {
        var collider = Physics2D.OverlapBox(transform.position, new Vector2(_horizontalDetectionSize, 2), 0, _playerLayer);
        if (collider == null)
            return false;

        if (collider.tag == "Player")
        {
            if (collider.transform.position.x < transform.position.x)
            {
                _animator.SetTrigger("TurnLeft");
                _shootDirection = -1;
            }
            else
            {
                _animator.SetTrigger("TurnRight");
                _shootDirection = 1;
            }
            return true;
        }
        return false;
    }

    private void useSkill()
    {
        _currentState = SkillState.ACTIVE;
        _drawTime = _bowDrawTime;
        AkSoundEngine.PostEvent("Slime_Charge", gameObject);
        Debug.Log("Taking aim");
    }

    private void shootArrow()
    {
        Arrow prefab = Instantiate(_arrowPrefab, _projectileOrigin.position, Quaternion.identity, transform);
        _animator.SetTrigger("Shoot");
        prefab.Shoot(_shootDirection);
        AkSoundEngine.PostEvent("Slime_Shot", gameObject);
    }

    void OnDrawGizmos()
    {
        // Displays the detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(_horizontalDetectionSize, 2));
    }
}
