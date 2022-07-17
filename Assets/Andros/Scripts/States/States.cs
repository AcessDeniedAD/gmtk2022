using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public interface IState
    {

    }
    public abstract class BaseState : IState
    {
        public bool ElementsCanMove;
        public virtual void In()
        {

        }
        public virtual void Out()
        {

        }
    }

    public class TitleScreen : BaseState
    {
        public TitleScreen()
        {
            ElementsCanMove = false;
        }
        public override void In()
        {
            EventsManager.TriggerEvent(nameof(StatesEvents.OnTitleScreenIn));
        }
        public override void Out()
        {

            EventsManager.TriggerEvent(nameof(StatesEvents.OnTitleScreenOut));
        }
    }

    public class Begin : BaseState
    {
        public Begin()
        {
            ElementsCanMove = false;
        }
        public override void In()
        {
            EventsManager.TriggerEvent(nameof(StatesEvents.OnBeginOut));
        }
        public override void Out()
        {

            EventsManager.TriggerEvent(nameof(StatesEvents.OnBeginIn));
        }
    }
    public class CountDown : BaseState
    {
        public CountDown()
        {
            ElementsCanMove = false;
        }
        public override void In()
        {
            EventsManager.TriggerEvent(nameof(StatesEvents.OnCountDownIn));
        }
        public override void Out()
        {

            EventsManager.TriggerEvent(nameof(StatesEvents.OnCountDownOut));
        }
    }

    public class RollDice : BaseState
    {
        public RollDice()
        {
            ElementsCanMove = true;
        }
        public override void In()
        {
            EventsManager.TriggerEvent(nameof(StatesEvents.OnRollDiceIn));
        }
        public override void Out()
        {

            EventsManager.TriggerEvent(nameof(StatesEvents.OnRollDiceOut));
        }
    }

    public class DiceIsShowed : BaseState
    {
        public DiceIsShowed()
        {
            ElementsCanMove = false;
        }
        public override void In()
        {
            EventsManager.TriggerEvent(nameof(StatesEvents.OnDiceIsShowedIn));
        }
        public override void Out()
        {

            EventsManager.TriggerEvent(nameof(StatesEvents.OnDiceIsShowedOut));
        }
    }

    public class Loose : BaseState
    {
        public Loose()
        {
            ElementsCanMove = false;
        }
        public override void In()
        {
            EventsManager.TriggerEvent(nameof(StatesEvents.OnLooseIn));
        }
        public override void Out()
        {
            EventsManager.TriggerEvent(nameof(StatesEvents.OnLooseOut));
        }
    }

    public class CoinTime : BaseState
    {
        public CoinTime()
        {
            ElementsCanMove = true;
        }
        public override void In()
        {
            EventsManager.TriggerEvent(nameof(StatesEvents.OnCoinTimeIn));
        }
        public override void Out()
        {
            EventsManager.TriggerEvent(nameof(StatesEvents.OnCoinTimeOut));
        }
    }

    public class Pause : BaseState
    {
        public Pause()
        {
            ElementsCanMove = false;
        }
        public override void In()
        {
            EventsManager.TriggerEvent(nameof(StatesEvents.OnPauseIn));
        }
        public override void Out()
        {
            EventsManager.TriggerEvent(nameof(StatesEvents.OnPauseOut));
        }
    }

    public class ResultMenu : BaseState
    {
        public ResultMenu()
        {
            ElementsCanMove = false;
        }
        public override void In()
        {
            EventsManager.TriggerEvent(nameof(StatesEvents.OnResultMenuIn));
        }
        public override void Out()
        {
            EventsManager.TriggerEvent(nameof(StatesEvents.OnResultMenuOut));
        }
    }

}
