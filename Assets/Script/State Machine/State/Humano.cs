using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humano : State
{
    public DataAgent _DataAgent;

     
    public override void LocadComponent()
    {
        base.LocadComponent();
        _DataAgent = GetComponent<DataAgent>();
    }
    public TypeState GetMostUrgentState()
    {
        if (_DataAgent.Energy.value < 0.2f) return TypeState.Dormir;
        if (_DataAgent.Hunger.value > 0.8f) return TypeState.Comer;
        if (_DataAgent.Bathroom.value > 0.8f) return TypeState.Banno;
        return typestate;
    }
    public override void Execute()
    {
        _DataAgent.UpdateStats(); 
    }

}
