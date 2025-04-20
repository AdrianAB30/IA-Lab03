using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Jugar : Humano
{
    private PlayerMovement _playerMovement;
    [SerializeField] private GameObject jugar;
    [SerializeField] private float _wanderRadius = 10f;
    [SerializeField] private float _wanderDelay = 3f;
    
    private float _timer;
    private Vector3 _wanderTarget;

    private void Awake()
    {
        typestate = TypeState.Jugar;
        LocadComponent();
    }

    public override void LocadComponent()
    {
        base.LocadComponent();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public override void Enter()
    {
        _timer = _wanderDelay; 
        _playerMovement.MoveToTarget(jugar);
    }

    public override void Execute()
    {
        _DataAgent.DiscountEnergy();
        _DataAgent.DiscountSleep();
        _DataAgent.DiscountWC();

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            SetRandomDestination();
            _timer = _wanderDelay;
        }

        if (_DataAgent.Energy.value < 0.25f)
        {
            _StateMachine.ChangeState(TypeState.Comer);
        }
        else if (_DataAgent.Sleep.value < 0.25f)
        {
            _StateMachine.ChangeState(TypeState.Dormir);
        }
        else if (_DataAgent.WC.value < 0.25f)
        {
            _StateMachine.ChangeState(TypeState.Banno);
        }

        base.Execute();
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _wanderRadius;
        randomDirection += transform.position;
        
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _wanderRadius, NavMesh.AllAreas))
        {
            _wanderTarget = hit.position;
            _playerMovement.MoveToTargetPosition(_wanderTarget);
        }
    }
}