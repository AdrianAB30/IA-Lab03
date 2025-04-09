using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comer : Humano
{
    public Transform comidaTarget;
    private void Awake()
    {
        typestate = TypeState.Comer;
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

        _StateMachine.GetComponent<SteeringAgent>().target = comidaTarget;
        _StateMachine.GetComponent<SteeringAgent>().behavior = SteeringBehavior.Arrive;

        if (_DataAgent.Hunger.value > 0)
        {
            _DataAgent.Hunger.value -= Time.deltaTime * 0.3f;
            _DataAgent.Energy.value += Time.deltaTime * 0.1f;
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
