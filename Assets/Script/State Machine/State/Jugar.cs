using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugar : Humano
{
    public Transform jugarTarget; 
    private void Awake()
    {
        typestate = TypeState.Jugar;
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

        _StateMachine.GetComponent<SteeringAgent>().target = jugarTarget;
        _StateMachine.GetComponent<SteeringAgent>().behavior = SteeringBehavior.Arrive;

        _DataAgent.DiscountEnergy();
        _DataAgent.Hunger.value += Time.deltaTime * 0.05f;

        base.Execute();
    }
    public override void Exit()
    {

    }
}
