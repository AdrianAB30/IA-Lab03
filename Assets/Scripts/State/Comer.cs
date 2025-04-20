using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comer : Humano
{
    private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _comedor;

    private void Awake()
    {
        typestate = TypeState.Comer;
        LocadComponent();
    }

    public override void LocadComponent()
    {
        base.LocadComponent();
        _playerMovement = GetComponent<PlayerMovement>();
        if (_comedor == null) _comedor = GameObject.FindWithTag("Repas");
    }

    public override void Enter()
    {
        if (_comedor != null)
        {
            _playerMovement.MoveToTarget(_comedor);
            _DataAgent.LoadEnergy(); 
        }
    }

    public override void Execute()
    {
        _DataAgent.DiscountSleep(); 
        _DataAgent.DiscountWC();    

        if (_DataAgent.Energy.value >= _DataAgent.Energy.valueMax)
        {
            _StateMachine.ChangeState(TypeState.Jugar);
        }
        base.Execute();

    }
}