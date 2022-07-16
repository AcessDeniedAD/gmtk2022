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
    private Dictionary<string, int> _hexa_id_by_color;
    private int _id = 0;
    private List<Vector3> _position ;
    private List<int> _order;
    private float _y_pos = 0.0f;
    private GameObject _parentLevel;
    private float _startTime;
    private float _journeyLength;
    private float _upPosition = 0.0f;
    private GameObject _hexa_locked;
    public float speed = 2.0f;
    public float downPosition = -1.0f;

    // Constructor
    public LevelManager(PrefabsLoaderManager prefabsLoaderManager, GameManager gameManager)
    {
        
        _prefabsLoaderManager = prefabsLoaderManager;
        _gameManager = gameManager;
        _position = new List<Vector3>();
        _hexa_id_by_color = new Dictionary<string, int>();
        _hexas = new Dictionary<int, GameObject>();
        _order = new List<int>();
        _parentLevel = _gameManager.InstantiateInGameManager(new GameObject("Level"), new Vector3(0, 0, 0), Quaternion.Euler(0, 30, 0));
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

    public void SelectedColor(string color){
        _hexa_locked = _hexas[_hexa_id_by_color["red"]];

    }

    private void InitLevel()
    {
        int z = 0;
        foreach (KeyValuePair<string,GameObject> hexa_info in hexa_prefabs)
        {
            AddNewHexa(hexa_info.Key, hexa_info.Value.transform.position, hexa_info.Value.transform.rotation);
            z += 10;
        }
        SelectedColor("red");
        _gameManager.StartCoroutine(HexaDrop());
    }


    public void AddNewHexa(string color, Vector3 position, Quaternion rotation)
    {
        GameObject hexa = _gameManager.InstantiateInGameManager(hexa_prefabs[color], position, rotation);
        hexa.transform.parent = _parentLevel.transform;
        _hexa_id_by_color.Add(color, _id);
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
        
    }


    // COROUTINE 
    IEnumerator HexaDrop()
    {
        _startTime = Time.time;
        _journeyLength = Vector3.Distance(new Vector3(0, 0, 0), new Vector3(0, downPosition, 0));
        while (_y_pos > downPosition)
        {
            float distCovered = (Time.time - _startTime) * speed;
            float fractionOfJourney = distCovered / _journeyLength;

            foreach (GameObject hexa in _hexas.Values)
            {
                if (!System.Object.ReferenceEquals(hexa, _hexa_locked))
                {
                    hexa.transform.position = Vector3.Lerp(
                   new Vector3(hexa.transform.position.x, 0, hexa.transform.position.z),
                   new Vector3(hexa.transform.position.x, downPosition, hexa.transform.position.z),
                   fractionOfJourney);
                    _y_pos = hexa.transform.position.y;
                }
               
            }

            yield return 0;
        }
        _gameManager.StartCoroutine(HexaBackUp());
    }

    IEnumerator HexaBackUp()
    {
        _startTime = Time.time;
        _journeyLength = Vector3.Distance(new Vector3(0, 0, 0), new Vector3(0, downPosition, 0));
        while (_y_pos < _upPosition)
        {
            float distCovered = (Time.time - _startTime) * speed;
            float fractionOfJourney = distCovered / _journeyLength;
            foreach (GameObject hexa in _hexas.Values)
            {
                if (!System.Object.ReferenceEquals(hexa,_hexa_locked))
                {
                    hexa.transform.position = Vector3.Lerp(
                   new Vector3(hexa.transform.position.x, downPosition, hexa.transform.position.z),
                   new Vector3(hexa.transform.position.x, 0, hexa.transform.position.z),
                   fractionOfJourney);
                    _y_pos = hexa.transform.position.y;
                }
            }
            Debug.Log("Here");  
            Debug.Log(_y_pos);
            yield return 0;
        }
        ColorSwitch();
    }
}
