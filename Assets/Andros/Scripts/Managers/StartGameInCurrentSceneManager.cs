using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameInCurrentSceneManager : BaseManager
{
    public StartGameInCurrentSceneManager(PrefabsLoaderManager prefabsLoaderManager, GameManager gameManager, PlayerManager playerManager)
    {
        //Debug.Log("StartGameInCurrentSceneManager");
        //playerManager.StartPlayerInSceneWithEquipedWeapons(prefabsLoaderManager.PlayerLoader.PlayerPrefab, new Vector3(1, 1, 0), new List<GameObject> { prefabsLoaderManager.WeaponsLoader.Gatling,  prefabsLoaderManager.WeaponsLoader.Shootgun }) ;
        //playerManager.StartPlayerInSceneWithEquipedWeapons(prefabsLoaderManager.PlayerLoader.PlayerPrefab, Vector3.down, new List<GameObject> {  prefabsLoaderManager.WeaponsLoader.Shootgun, prefabsLoaderManager.WeaponsLoader.Gatling });
    }
}