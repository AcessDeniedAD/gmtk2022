using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatesEvents
{
    public StatesEvents()
    {

    }
    //TODO USE static String instead  unityAction;
    public UnityAction<Args> OnTitleScreenIn;
    public UnityAction<Args> OnTitleScreenOut;

    public  UnityAction<Args> OnBeginIn;
    public  UnityAction<Args> OnBeginOut;

    public UnityAction<Args> OnCountDownIn;
    public UnityAction<Args> OnCountDownOut;

    public  UnityAction<Args> OnRollDiceIn;
    public  UnityAction<Args> OnRollDiceOut;

    public UnityAction<Args> OnDiceIsShowedIn;
    public UnityAction<Args> OnDiceIsShowedOut;

    public UnityAction<Args> OnCoinTimeIn;
    public UnityAction<Args> OnCoinTimeOut;

    public UnityAction<Args> OnLooseIn;
    public  UnityAction<Args> OnLooseOut;

    public UnityAction<Args> OnPauseIn;
    public UnityAction<Args> OnPauseOut;

    public UnityAction<Args> OnResultMenuIn;
    public UnityAction<Args> OnResultMenuOut;
    
    public class StatesEventArgs : Args
    {
        
    }
}
