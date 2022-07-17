using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : BaseManager
{
    private readonly GameManager _gameManager;
    private readonly PrefabsLoaderManager _prefabsLoaderManager;
    private readonly CoinPoolerManager _poolerManager;
    private readonly LevelManager _levelManager;
    private List<GameObject> coinsOnScene = new List<GameObject>();
    public CoinManager(GameManager gameManager, PrefabsLoaderManager prefabsLoaderManager, CoinPoolerManager coinPoolerManager, LevelManager levelManager)
    {
        _gameManager = gameManager;
        _prefabsLoaderManager = prefabsLoaderManager;
        _poolerManager = coinPoolerManager;
        _levelManager = levelManager;

        coinPoolerManager.InstantiatePooledObjectsIntoParent(prefabsLoaderManager.LevelLoader.coinPrefab, new GameObject("coinsParent"), 100);
        EventsManager.StartListening(nameof(StatesEvents.OnCoinTimeIn), PopCoin);
        EventsManager.StartListening(nameof(StatesEvents.OnDiceIsShowedOut), DeleteCoin);
    }

    private void PopCoin(Args args)
    {
       _gameManager.StartCoroutine(PopCoinCoroutine());
    }

    private void DeleteCoin(Args args)
    {
        _gameManager.StartCoroutine(DeleteCoinCoroutine());
    }
    IEnumerator PopCoinCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject go in _levelManager.Hexas.Values)
        {
            if (!go.activeSelf)
            {
                continue;
            }
            var coin = _poolerManager.GetPooledObject();
            coin.SetActive(true);
            EventsManager.TriggerEvent("CoinFalling");
            coin.transform.position = new Vector3(go.transform.position.x, 10, go.transform.position.z);
            coin.GetComponent<Coin>().Down(new Vector3(go.transform.position.x,0.2f,go.transform.position.z),false);
            coinsOnScene.Add(coin);
        }
        yield return null;
    }

    IEnumerator DeleteCoinCoroutine()
    {
        foreach (GameObject coin in coinsOnScene)
        {
            coin.GetComponent<Coin>().StopAllCoroutines();
            coin.GetComponent<Coin>().Down(new Vector3(coin.transform.position.x, -8f, coin.transform.position.z),true);
        }
        coinsOnScene.Clear();
        yield return 0;
    }
}
