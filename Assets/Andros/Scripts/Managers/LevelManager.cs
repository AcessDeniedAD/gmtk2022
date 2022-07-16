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
    private int _hexa_locked_id;
    private bool _random_hexa_move = false;
    public float speed = 10.0f;
    public float downPosition = -10.0f;
    public int platformMoveProba = 25;

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
        _hexa_locked_id = _hexa_id_by_color["red"];
        _hexa_locked = _hexas[_hexa_locked_id];

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
        //StartRandomHexaMove();
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
        int old_hexa_locked_index = _order.FindIndex(id => id == +_hexa_locked_id);
        List<int> shuffledOrder = _order.OrderBy(a => Guid.NewGuid()).ToList();
        int new_hexa_locked_index = shuffledOrder.FindIndex(id => id == +_hexa_locked_id);
        (shuffledOrder[new_hexa_locked_index], shuffledOrder[old_hexa_locked_index]) = (shuffledOrder[old_hexa_locked_index], shuffledOrder[new_hexa_locked_index]);
        int i = 0;
        foreach (int order in shuffledOrder) {
            var oldPosition = _hexas[order].transform.position;
            _hexas[order].transform.position = new Vector3(_position[i].x, oldPosition.y, _position[i].z);
            _position[i] = oldPosition;
            i += 1;
        }
        
    }

    public void StartRandomHexaMove()
    {
        _random_hexa_move = true;
        _gameManager.StartCoroutine(RandomHexaMoveCoro());
    }


    public void StopRandomHexaMove()
    {
        _random_hexa_move = false;
    }

    IEnumerator RandomHexaMoveCoro()
    {
        System.Random rnd = new System.Random();
        while (_random_hexa_move)
        {
 
            if (rnd.Next(0, 100) < platformMoveProba){
                _random_hexa_move = false;
                GameObject current_hexa = _hexas[rnd.Next(0, _hexas.Count)];
                _gameManager.StartCoroutine(HexaMoveTopCoro(current_hexa));
                yield return new WaitForSecondsRealtime(3);
                _gameManager.StartCoroutine(HexaMoveDownCoro(current_hexa));
            }

            yield return 0;
        }
    }

    IEnumerator HexaMoveTopCoro(GameObject hexa)
    {
        Vector3 down_position = hexa.transform.position;
        float startTime = Time.time;
        Vector3 top_position = new Vector3(down_position.x, down_position.y + 0.2f, down_position.z);
        float journeyLength = Vector3.Distance(down_position, top_position);
        while (top_position.y > hexa.transform.position.y)
        {
            float distCovered = (Time.time - startTime) * 0.3f;
            float fractionOfJourney = distCovered / journeyLength;
                if (true)
                {
                    hexa.transform.position = Vector3.Lerp(
                   down_position,
                   top_position,
                   fractionOfJourney);
                }
            yield return 0;
        }
    }

    IEnumerator HexaMoveDownCoro(GameObject hexa)
    {
        Vector3 top_position = hexa.transform.position;
        Vector3 down_position = new Vector3(hexa.transform.position.x, top_position.y - 0.2f, hexa.transform.position.z);
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(top_position,down_position);
        while (down_position.y < hexa.transform.position.y)
        {
            float distCovered = (Time.time - startTime) * 0.3f;
            float fractionOfJourney = distCovered / journeyLength;
            if (true)
            {
                hexa.transform.position = Vector3.Lerp(
               top_position,
               down_position,
               fractionOfJourney);
            }
            yield return 0;
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
        foreach (GameObject hexa in _hexas.Values)
        {
            if (!System.Object.ReferenceEquals(hexa, _hexa_locked))
            {
                hexa.SetActive(false);
            }
        }
        ColorSwitch();
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
            yield return 0;
        }
        foreach (GameObject hexa in _hexas.Values)
        {
            hexa.SetActive(true);
        }
    }
}
