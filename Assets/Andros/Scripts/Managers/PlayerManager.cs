using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerManager : BaseManager
{
    [HideInInspector]
    public GameObject Player;

    private readonly StatesManager _statesManager;

    private readonly PrefabsLoaderManager _prefabsLoaderManager;

    private readonly GameManager _gameManager;
    public PlayerManager(PrefabsLoaderManager prefabsLoaderManager, GameManager gameManager, StatesManager statesManager)
    {
        _prefabsLoaderManager = prefabsLoaderManager;
        _statesManager = statesManager;
        _gameManager = gameManager;
        StartPlayerInScene(_prefabsLoaderManager.PlayerLoader.PlayerPrefab);
    }

    public void StartPlayerInScene(GameObject player)
    {
        Player = _gameManager.InstantiateInGameManager(player, new Vector3(0.0f, 0.75f, 0.0f), Quaternion.identity);
        Player.GetComponent<PlayerMovement>()._statesManager = _statesManager;
        Player.name = "Player";
    }

  
   
}
