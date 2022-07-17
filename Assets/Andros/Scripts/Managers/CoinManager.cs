using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : BaseManager
{
    private readonly GameManager _gameManager;
    private readonly PrefabsLoaderManager _prefabsLoaderManager;
    private readonly CoinPoolerManager _poolerManager;
    public CoinManager(GameManager gameManager, PrefabsLoaderManager prefabsLoaderManager, CoinPoolerManager coinPoolerManager)
    {
        _gameManager = gameManager;
        _prefabsLoaderManager = prefabsLoaderManager;
        coinPoolerManager = _poolerManager;
        coinPoolerManager.InstantiatePooledObjectsIntoParent(new GameObject(), new GameObject(), 100);
    }

}
