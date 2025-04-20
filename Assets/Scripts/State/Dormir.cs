using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dormir : Humano
{
    private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _dormitorio;

    private void Awake()
    {
        typestate = TypeState.Dormir;
        LocadComponent();
    }

    public override void LocadComponent()
    {
        base.LocadComponent();
        _playerMovement = GetComponent<PlayerMovement>();
        if (_dormitorio == null) _dormitorio = GameObject.FindWithTag("Sleep");
    }

    public override void Enter()
    {
        if (_dormitorio != null)
        {
            _playerMovement.MoveToTarget(_dormitorio);
            _DataAgent.LoadSleep(); 
        }
    }

    public override void Execute()
    {
        _DataAgent.DiscountEnergy(); 
        _DataAgent.DiscountWC();     

        if (_DataAgent.Sleep.value >= _DataAgent.Sleep.valueMax)
        {
            _StateMachine.ChangeState(TypeState.Jugar);
        }

        base.Execute();
    }

    public override void Exit()
    {
    }
}