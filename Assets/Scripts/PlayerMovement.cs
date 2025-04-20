using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _stoppingDistance = 0.5f;

    private NavMeshAgent _navMeshAgent;
    [SerializeField] private GameObject _currentTarget;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        ConfigureNavMeshAgent();
    }

    private void ConfigureNavMeshAgent()
    {
        _navMeshAgent.angularSpeed = _rotationSpeed * 100; 
        _navMeshAgent.stoppingDistance = _stoppingDistance;
        _navMeshAgent.updateRotation = true; 
        _navMeshAgent.updateUpAxis = false; 
    }

    public void MoveToTarget(GameObject target)
    {
        if (_navMeshAgent != null && target != null)
        {
            _navMeshAgent.SetDestination(target.transform.position);
        }
    }
    public void MoveToTargetPosition(Vector3 position)
    {
        Vector3 targetPosition = new Vector3(
            position.x,
            transform.position.y,
            position.z
        );

        _navMeshAgent.SetDestination(targetPosition);
        _currentTarget = null; 
    }
    public void MoveToPosition(Vector3 position)
    {
        _navMeshAgent.SetDestination(new Vector3(
            position.x,
            transform.position.y,
            position.z
        ));
    }

    public void FollowToy(Transform toy)
    {
        if (toy == null) return;
        MoveToPosition(toy.position);
    }
    private void Update()
    {
        if (_currentTarget != null &&
            !_navMeshAgent.pathPending &&
            _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            Debug.Log($"Lleg� a {_currentTarget.tag}");
            _currentTarget = null;
        }
    }
}