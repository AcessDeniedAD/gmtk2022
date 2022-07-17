using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class PlayerManager : BaseManager
{
    [HideInInspector]
    public GameObject Player;

    private readonly StatesManager _statesManager;

    private readonly PrefabsLoaderManager _prefabsLoaderManager;
    private readonly DiceManager _diceManager;
    private readonly HudManager _hudManager;
    private readonly LevelManager _levelManager;
    private readonly ZenjectSceneLoader _sceneLoader;

    private readonly GameManager _gameManager;
    private Vector3 _initialPosition;
    public bool IsDead;
    public PlayerManager(ZenjectSceneLoader sceneLoader, PrefabsLoaderManager prefabsLoaderManager, GameManager gameManager, StatesManager statesManager, DiceManager diceManager, HudManager hudManager, LevelManager levelManager)
    {
        _prefabsLoaderManager = prefabsLoaderManager;
        _statesManager = statesManager;
        _gameManager = gameManager;
        _diceManager = diceManager;
        _hudManager = hudManager;
        _levelManager = levelManager;
        _sceneLoader = sceneLoader;
        StartPlayerInScene(_prefabsLoaderManager.PlayerLoader.PlayerPrefab);
    }

    private void StartPlayerInScene(GameObject player)
    {
        Player = _gameManager.InstantiateInGameManager(player, new Vector3(0.0f, 0.75f, 0.0f), Quaternion.identity);
        PlayerMovement playerMove = Player.GetComponent<PlayerMovement>();
        playerMove._statesManager = _statesManager;
        playerMove._playerManager = this;
        _initialPosition = Player.transform.position;
        Player.name = "Player";
    }

    private void ResetPlacement()
    {
        Player.transform.position = new Vector3(0.0f, 0.75f, 0.0f);
        Player.transform.rotation = Quaternion.identity;
    }

    public void SetIsDead()
    {
        Player.transform.position = _initialPosition;
    }

  
   
}
