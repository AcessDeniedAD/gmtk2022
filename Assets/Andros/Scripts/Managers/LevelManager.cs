using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class LevelManager : BaseManager
{
    private readonly PrefabsLoaderManager _prefabsLoaderManager;
    private readonly GameManager _gameManager;
    private readonly Dictionary<string, GameObject> hexa_prefabs;
    private Dictionary<int, GameObject> _hexas ;
    private int _id = 0;
    private List<Vector3> _position ;
    private List<int> _order;
    private float _y_pos = 0.0f;

    public LevelManager(PrefabsLoaderManager prefabsLoaderManager, GameManager gameManager)
    {
        _prefabsLoaderManager = prefabsLoaderManager;
        _gameManager = gameManager;
        _position = new List<Vector3>();
        _hexas = new Dictionary<int, GameObject>();
        _order = new List<int>();
        hexa_prefabs = new Dictionary<string, GameObject>() {
           {"red", _prefabsLoaderManager.LevelLoader.hexaPrefabRed},
           {"blue", _prefabsLoaderManager.LevelLoader.hexaPrefabBlue},
           {"green", _prefabsLoaderManager.LevelLoader.hexaPrefabGreen},
           {"yellow", _prefabsLoaderManager.LevelLoader.hexaPrefabYellow},
           {"orange", _prefabsLoaderManager.LevelLoader.hexaPrefabOrange},
            {"purple", _prefabsLoaderManager.LevelLoader.hexaPrefabPurple},
        };
        InitLevel();
    }


    private void InitLevel()
    {
        int z = 0;
        foreach (string color in hexa_prefabs.Keys)
        {
            AddNewHexa(color+"Base", color, new Vector3(0, 0, z));
            z += 10;
        }
        _gameManager.StartCoroutine(HexaDrop());
    }

    IEnumerator HexaDrop()
    {
        var dropYPosition = -10.0f;
        var dropSpeed = Time.fixedDeltaTime * 0.5f;
        Debug.Log(dropSpeed);
        while (_y_pos > dropYPosition) {
            _y_pos -= dropSpeed;
            foreach (GameObject hexa in _hexas.Values)
            {
                hexa.transform.position = new Vector3(hexa.transform.position.x, _y_pos, hexa.transform.position.z);
            }
            yield return 0;
        }
        ColorSwitch();

    }

    IEnumerator HexaBackUp()
    {
        var UpYPosition = 0.0f;
        var upSpeed = Time.fixedDeltaTime * 0.5f;
        Debug.Log(upSpeed);
        while (_y_pos < UpYPosition)
        {
            _y_pos += upSpeed;
            foreach (GameObject hexa in _hexas.Values)
            {
                hexa.transform.position = new Vector3(hexa.transform.position.x, _y_pos, hexa.transform.position.z);
            }
            yield return 0;
        }
    }


    public void AddNewHexa(string name, string color, Vector3 position)
    {
        GameObject hexa = _gameManager.InstantiateInGameManager(hexa_prefabs[color], position, Quaternion.identity);
        _hexas.Add(_id, hexa);
        _position.Add(hexa.transform.position);
        _order.Add(_id);
        _id += 1;
    }

    public void ChangeHexaSize(int id, Vector3 size)
    {
        _hexas[id].transform.localScale = size;
    }
    public void ColorSwitch()
    {
        var shuffledOrder = _order.OrderBy(a => Guid.NewGuid()).ToList();
        var i = 0;
        foreach (int order in shuffledOrder) {
            var oldPosition = _hexas[order].transform.position;
            _hexas[order].transform.position = new Vector3(_position[i].x, oldPosition.y, _position[i].z);
            _position[i] = oldPosition;
            i += 1;
        }
        _gameManager.StartCoroutine(HexaBackUp());
        
    }
}
