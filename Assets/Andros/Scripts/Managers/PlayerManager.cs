using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerManager : BaseManager
{
    [HideInInspector]
    public GameObject Player;

    private readonly PrefabsLoaderManager _prefabsLoaderManager;

    private readonly GameManager _gameManager;

    public PlayerManager(PrefabsLoaderManager prefabsLoaderManager, GameManager gameManager)
    {
        _prefabsLoaderManager = prefabsLoaderManager;
        _gameManager = gameManager;
        StartPlayerInScene(_prefabsLoaderManager.PlayerLoader.PlayerPrefab);
    }

    public void StartPlayerInScene(GameObject player)
    {
        Player = _gameManager.InstantiateInGameManager(player, new Vector3(0, 0, 0), Quaternion.identity);
        Player.name = "Player";
    }

  
   
}
