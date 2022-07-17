using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterManager : BaseManager
{
    public int DifficultyLevel=0;

    private readonly StatesManager _statesManager;
    private readonly GameManager _gameManager;
    private readonly DiceManager _diceManager;
    private readonly LevelManager _levelManager;
    private float _difficultyTimer =0 ;
    private bool _isDifficultyTimerEnabled = true;
    private List<List<float>> _rangesToRollingDiceTime = new List<List<float>>();

    private List<List<float>> _rangesToCoinTime = new List<List<float>>();

    private List<float> _timesToChangeLevels = new List<float> ();
    private List<float> _timesToWaitBeforePlatformsFall = new List<float>();
    private bool isLoose =false;
    public GameMasterManager(StatesManager statesManager, GameManager gameManager, DiceManager diceManager, LevelManager levelManager)
    {
        BuildDifficultiesLists();

        _statesManager = statesManager;
        _gameManager = gameManager;
        _diceManager = diceManager;
        _levelManager = levelManager;

        GameManager.GameUpdateHandler += DifficultyChanger;
        _levelManager = levelManager;
        EventsManager.StartListening(nameof(StatesEvents.OnCountDownIn), StartCountdown);
        EventsManager.StartListening(nameof(StatesEvents.OnRollDiceIn), StartRollDice);
        EventsManager.StartListening(nameof(StatesEvents.OnCoinTimeIn), StartCoinTime);
        EventsManager.StartListening(nameof(StatesEvents.OnDiceIsShowedIn), StartShowedDiceTime);
        EventsManager.StartListening(nameof(StatesEvents.OnLooseIn), Loose);

        _statesManager.ChangeCurrentState(new States.CountDown());
    }
    public void BuildDifficultiesLists()
    {
        _rangesToRollingDiceTime.Add(new List<float>() { 4, 5 });//1
        _rangesToRollingDiceTime.Add(new List<float>() { 3.5f, 5 });//2
        _rangesToRollingDiceTime.Add(new List<float>() { 2.5f, 7 });//3
        _rangesToRollingDiceTime.Add(new List<float>() { 2.5f, 6 });//4
        _rangesToRollingDiceTime.Add(new List<float>() { 2.5f, 5 });//5
        _rangesToRollingDiceTime.Add(new List<float>() { 2.5f, 4 });//6
        _rangesToRollingDiceTime.Add(new List<float>() { 2.5f, 3 });//7
        _rangesToRollingDiceTime.Add(new List<float>() { 1.5f, 3 });//8
        _rangesToRollingDiceTime.Add(new List<float>() { 1.5f, 2.5f });//9
        _rangesToRollingDiceTime.Add(new List<float>() { 1.5f, 2f });//10
        _rangesToRollingDiceTime.Add(new List<float>() { 1f, 1.5f });//11
        _rangesToRollingDiceTime.Add(new List<float>() { 0.5f, 1f });//12
        _rangesToRollingDiceTime.Add(new List<float>() { 0.3f, 1f });//13
        _rangesToRollingDiceTime.Add(new List<float>() { 0.3f, 0.5f });//14

        _rangesToCoinTime.Add(new List<float>() { 4, 5 });//1
        _rangesToCoinTime.Add(new List<float>() { 4, 5 });//2
        _rangesToCoinTime.Add(new List<float>() { 4, 5 });//3
        _rangesToCoinTime.Add(new List<float>() { 4, 5 });//4
        _rangesToCoinTime.Add(new List<float>() { 4, 5 });//5
        _rangesToCoinTime.Add(new List<float>() { 4, 5 });//6
        _rangesToCoinTime.Add(new List<float>() { 4, 5 });//7
        _rangesToCoinTime.Add(new List<float>() { 4, 5 });//8
        _rangesToCoinTime.Add(new List<float>() { 4, 5 });//9
        _rangesToCoinTime.Add(new List<float>() { 4, 5 });//10
        _rangesToCoinTime.Add(new List<float>() { 4, 5 });//11
        _rangesToCoinTime.Add(new List<float>() { 4, 5 });//12
        _rangesToCoinTime.Add(new List<float>() { 4, 5 });//13
        _rangesToCoinTime.Add(new List<float>() { 4, 5 });//14

        _timesToChangeLevels.Add(6);//1
        _timesToChangeLevels.Add(10);//2
        _timesToChangeLevels.Add(20);//3
        _timesToChangeLevels.Add(25);//4
        _timesToChangeLevels.Add(30);//5
        _timesToChangeLevels.Add(40);//6
        _timesToChangeLevels.Add(50);//7
        _timesToChangeLevels.Add(60);//8
        _timesToChangeLevels.Add(70);//9
        _timesToChangeLevels.Add(80);//10
        _timesToChangeLevels.Add(90);//11
        _timesToChangeLevels.Add(100);//12
        _timesToChangeLevels.Add(110);//13
        _timesToChangeLevels.Add(120);//14

        _timesToWaitBeforePlatformsFall.Add(5);//1
        _timesToWaitBeforePlatformsFall.Add(4.5f);//2
        _timesToWaitBeforePlatformsFall.Add(3);//3
        _timesToWaitBeforePlatformsFall.Add(3);//4
        _timesToWaitBeforePlatformsFall.Add(3);//5
        _timesToWaitBeforePlatformsFall.Add(2.5f);//6
        _timesToWaitBeforePlatformsFall.Add(2.5f);//7
        _timesToWaitBeforePlatformsFall.Add(2.5f);//8
        _timesToWaitBeforePlatformsFall.Add(2);//9
        _timesToWaitBeforePlatformsFall.Add(2);//10
        _timesToWaitBeforePlatformsFall.Add(2);//11
        _timesToWaitBeforePlatformsFall.Add(2);//12
        _timesToWaitBeforePlatformsFall.Add(1.5f);//13
        _timesToWaitBeforePlatformsFall.Add(1);//14
    }
    public void Loose(Args args)
    {
        isLoose = true;
    }
    public void DifficultyChanger()
    {
        if (_isDifficultyTimerEnabled)
        {
            _difficultyTimer += Time.deltaTime * Time.timeScale;
        }
        
        if (DifficultyLevel < _timesToChangeLevels.Count -1)
        {
            if (_difficultyTimer > _timesToChangeLevels[DifficultyLevel])
            {
                DifficultyLevel++;
            }
        }
    }

    public void StartCountdown(Args args)
    {
        _difficultyTimer = 0;
        _gameManager.StartCoroutine(CountDownCoroutine());
        isLoose = false;
    }
    public void StartShowedDiceTime(Args args)
    {
        _gameManager.StartCoroutine(StartShowedDiceTimeCoroutine());
    }
    public void StartRollDice(Args args)
    {
        _gameManager.StartCoroutine(StartRollDiceCoroutine());
    }

    IEnumerator CountDownCoroutine()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("1");
        yield return new WaitForSeconds(1);
        Debug.Log("2");
        yield return new WaitForSeconds(1);
        Debug.Log("3");
        yield return new WaitForSeconds(1);
     
        _statesManager.ChangeCurrentState(new States.RollDice());
    }

    IEnumerator StartRollDiceCoroutine()
    {
        _diceManager.EnableRollingDiceInScene();
        _diceManager.BuildNewFacesOnDice(DifficultyLevel);
        _isDifficultyTimerEnabled = true;

        var timeToRoll = Random.Range(_rangesToRollingDiceTime[DifficultyLevel][0], _rangesToRollingDiceTime[DifficultyLevel][1]);
        _diceManager.RollDice(timeToRoll);
        
        var timer = 0f;
        while (timer < timeToRoll)
        {
            timer += Time.deltaTime * Time.timeScale;
            if(timer % 1 == 0)
            Debug.Log(".");
            yield return 0;
        }
        if (isLoose)
        {
            _statesManager.ChangeCurrentState(new States.CountDown());
        }
        else
        {
            _statesManager.ChangeCurrentState(new States.DiceIsShowed());
        }
        
    }

    IEnumerator StartShowedDiceTimeCoroutine()
    {
        _isDifficultyTimerEnabled = false;
        var timer = 0f;
        while (timer < _timesToWaitBeforePlatformsFall[DifficultyLevel])
        {
            timer += Time.deltaTime * Time.timeScale;
            if (timer % 1 == 0)
                Debug.Log(".");
            yield return 0;
        }

        _levelManager.DropHexa(_diceManager.GetDiceFace(), DifficultyLevel);

        _diceManager.DisableRollingDiceInScene();
        yield return new WaitForSeconds(1);
        if (isLoose)
        {
            _statesManager.ChangeCurrentState(new States.CountDown());
        }
        else
        {
            _statesManager.ChangeCurrentState(new States.CoinTime());
        }
    }
    private void StartCoinTime(Args args)
    {
        _gameManager.StartCoroutine(CoinTimeCoroutine());
    }

    IEnumerator CoinTimeCoroutine()
    {
        Debug.Log("TIME TO GET COIN");
        var timeToGetCoin = Random.Range(_rangesToRollingDiceTime[DifficultyLevel][0], _rangesToRollingDiceTime[DifficultyLevel][1]);
        yield return new WaitForSeconds(timeToGetCoin);
        Debug.Log("STOP TIME TO GET COIN");

        if (isLoose)
        {
            _statesManager.ChangeCurrentState(new States.CountDown());
        }
        else
        {
            _statesManager.ChangeCurrentState(new States.RollDice());
        }
    }

}
