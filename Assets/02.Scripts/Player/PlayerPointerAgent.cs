using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using static Monster;

public class PlayerPointerAgent : MonoBehaviour
{
    private RaycastHit _rayHit = new RaycastHit();
    private Camera _mainCamera;
    private NavMeshAgent _agent;
    private PlayerStats _stats;
    private PlayerAnimator _animator;

    private float _jumpDuration = 0.8f;
    private float _jumpHeight = 2f;
    private float _endPositionOffsetY = 1.1f;
    private bool _isJumping = false;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _stats = GetComponent<PlayerStats>();
        _animator = GetComponent<PlayerAnimator>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _stats.MoveSpeed.Value;
    }

    private void Update()
    {
        if (_isJumping || !GameManager.Instance.AutoMode) return;

        Move();
        Jump();

        _animator.PlayMoveAnimation(_agent.velocity.magnitude);
    }

    private void Move()
    {
        if (!Input.GetMouseButtonDown(1)) return;

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _rayHit))
        {
            _agent.SetDestination(_rayHit.point);
        }
    }

    private void Jump()
    {
        if (!_agent.isOnOffMeshLink) return;

        _agent.isStopped = true;
        _isJumping = true;

        OffMeshLinkData data = _agent.currentOffMeshLinkData;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = data.endPos;
        endPosition.y += _endPositionOffsetY;

        transform.LookAt(new Vector3(endPosition.x, transform.position.y, endPosition.z));

        DOVirtual.Float(0f, 1f, _jumpDuration, (float timeRate) =>
        {
            Vector3 position = Vector3.Lerp(startPosition, endPosition, timeRate);
            position.y += _jumpHeight * Mathf.Sin(timeRate * Mathf.PI);

            transform.position = position;
        })
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            _agent.CompleteOffMeshLink();
            _agent.isStopped = false;
            _isJumping = false;
        });
    }
}
