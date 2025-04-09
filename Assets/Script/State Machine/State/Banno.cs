using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banno : Humano
{
    public Transform bannoTarget; 
    private void Awake()
    {
        typestate = TypeState.Banno;
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

        _StateMachine.GetComponent<SteeringAgent>().target = bannoTarget;
        _StateMachine.GetComponent<SteeringAgent>().behavior = SteeringBehavior.Arrive;

        _DataAgent.Bathroom.value -= Time.deltaTime * 0.5f;
        _DataAgent.Sleep.value += Time.deltaTime * 0.4f;

        base.Execute();
    }
    public override void Exit()
    {

    }
}
