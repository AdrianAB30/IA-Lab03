using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dormir : Humano
{
    public Transform camaTarget;
    private void Awake()
    {
        typestate = TypeState.Dormir;
        LocadComponent();
    }
    public override void LocadComponent()
    {
        base.LocadComponent();

    }
    public override void Enter()
    {

    }
    public override void Execute()
    {
        TypeState urgent = GetMostUrgentState();
        if (urgent != typestate)
        {
            _StateMachine.ChangeState(urgent);
            return;
        }

        _StateMachine.GetComponent<SteeringAgent>().target = camaTarget;
        _StateMachine.GetComponent<SteeringAgent>().behavior = SteeringBehavior.Arrive;

        if (_DataAgent.Energy.value < 1f)
        {
            _DataAgent.Energy.value += Time.deltaTime * 0.3f;
        }
        else
        {
            _StateMachine.ChangeState(TypeState.Jugar);
        }

        base.Execute();
    }
    public override void Exit()
    {

    }
}
