using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameInCurrentSceneManager : BaseManager
{

    private readonly PlayerManager _playerManager;

    public StartGameInCurrentSceneManager(PrefabsLoaderManager prefabsLoaderManager, GameManager gameManager, PlayerManager playerManager)
    {
        _playerManager = playerManager;

        //Debug.Log("StartGameInCurrentSceneManager");
        //playerManager.StartPlayerInSceneWithEquipedWeapons(prefabsLoaderManager.PlayerLoader.PlayerPrefab, Vector3.down, new List<GameObject> {  prefabsLoaderManager.WeaponsLoader.Shootgun, prefabsLoaderManager.WeaponsLoader.Gatling });
    }
}