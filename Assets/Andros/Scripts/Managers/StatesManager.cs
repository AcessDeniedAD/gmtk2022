using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using States;
using Zenject;

public class StatesManager
{
    BaseState currentState;
    public BaseState CurrentState {
        get
        {
            return currentState;
        }
        set
        {
            currentState?.Out();
            currentState = value;
            currentState.In();
        }
    }

    public bool IsCurrentState(BaseState state){
        if(currentState.GetType().Name == state.GetType().Name)
        {
            return true;
        }
        return false;
    }

    public void ChangeCurrentState(BaseState state)
    {
        CurrentState = state;
    }
}