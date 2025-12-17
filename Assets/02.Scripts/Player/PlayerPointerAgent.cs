using UnityEngine;
using UnityEngine.AI;

public class PlayerPointerAgent : MonoBehaviour
{
    private RaycastHit _rayHit = new RaycastHit();
    private Camera _mainCamera;
    private NavMeshAgent _agent;
    private PlayerStats _stats;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _stats = GetComponent<PlayerStats>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _stats.MoveSpeed.Value;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(1) || !GameManager.Instance.AutoMode) return;
        
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out _rayHit))
        {
            _agent.SetDestination(_rayHit.point);
        }
    }
}
