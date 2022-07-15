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

    public void ChangeCurrentState(BaseState state)
    {
        CurrentState = state;
    }
}