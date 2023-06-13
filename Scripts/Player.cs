using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour, IControllable
{
    [Header("Character stats")]
    [SerializeField] private float _strafeRange = 3;
    [SerializeField] private float _strafeSpeed = 5;
    [SerializeField] private AnimationCurve _jumpPattern;

    private Animator _animator;

    private CharacterController _controller;
    private float _moveTarget;
    private bool _isSliding = false;

    private bool _isJumping = false;
    private float _jumpTime;
    private float _currentTime;

    public void Strafe(Direction direction)
    {
        if (direction == Direction.Left && _moveTarget > -_strafeRange)
            _moveTarget -= _strafeRange;

        if (direction == Direction.Right && _moveTarget < _strafeRange)
            _moveTarget += _strafeRange;

        Debug.Log($"Strafe to {direction.ToString()}");
    }

    public void Jump()
    {
        _isJumping = true;
    }

    public void Slide()
    {
        if (!_isSliding)
        {
            StartCoroutine(WaitForSlide(1));
            Debug.Log("Slide");
        }      
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        _moveTarget = 0;
    }

    private void Update()
    {
        MoveToTargetPosition(new Vector3(_moveTarget, transform.position.y, transform.position.z), _strafeSpeed);

        if (_isJumping)
        {
            MoveToTargetPosition(new Vector3(transform.position.x, _jumpPattern.Evaluate(_currentTime), transform.position.z), 5, 0.1f);
            _currentTime += Time.deltaTime;

            if (_currentTime >= _jumpTime)
            {
                _currentTime = 0;
                _isJumping = false;
            }
        }
    }

    private void Start()
    {
        _jumpTime = _jumpPattern.keys[_jumpPattern.keys.Length - 1].time;
    }

    private void MoveToTargetPosition(Vector3 target, float speed, float magnitude = 0.05f)
    {
        var offset = target - transform.position;

        if (offset.magnitude > magnitude)
        {
            offset = offset.normalized;
            _controller.Move(offset * speed * Time.deltaTime);
        }
    }

    private IEnumerator WaitForSlide(float seconds)
    {
        _controller.center = new Vector3(0, _controller.center.y / 2, 0);
        _controller.height /= 2;
        _isSliding = true;

        yield return new WaitForSeconds(seconds);

        _controller.center = new Vector3(0, _controller.center.y * 2, 0);
        _controller.height *= 2;
        _isSliding = false;
    }
}