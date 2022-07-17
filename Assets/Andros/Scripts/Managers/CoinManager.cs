using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : BaseManager
{
    private readonly GameManager _gameManager;
    private readonly PrefabsLoaderManager _prefabsLoaderManager;
    private readonly CoinPoolerManager _poolerManager;
    private readonly LevelManager _levelManager;
    public CoinManager(GameManager gameManager, PrefabsLoaderManager prefabsLoaderManager, CoinPoolerManager coinPoolerManager, LevelManager levelManager)
    {
        _gameManager = gameManager;
        _prefabsLoaderManager = prefabsLoaderManager;
        _poolerManager = coinPoolerManager;
        _levelManager = levelManager;

        coinPoolerManager.InstantiatePooledObjectsIntoParent(prefabsLoaderManager.LevelLoader.coinPrefab, new GameObject("coinsParent"), 100);
        EventsManager.StartListening(nameof(StatesEvents.OnCoinTimeIn), PopCoin);
    }

    private void PopCoin(Args args)
    {
       _gameManager.StartCoroutine(PopCoinCoroutine());
    }
    IEnumerator PopCoinCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject go in _levelManager.Hexas.Values)
        {
            var coin = _poolerManager.GetPooledObject();
            coin.SetActive(true);
            coin.transform.position = go.transform.position;
        }
        
    }

}
