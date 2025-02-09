using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class LevelManager : BaseManager
{
    private readonly PrefabsLoaderManager _prefabsLoaderManager;
    private readonly GameManager _gameManager;
    private readonly Dictionary<string, GameObject> _hexaPrefabs;
    public Dictionary<int, GameObject> Hexas;
    private Dictionary<string, int> _hexaIdByColor;
    private int _id = 0;
    private List<Vector3> _position ;
    private List<int> _order;
    private float _yPos = 0.0f;
    private GameObject _parentLevel;
    private float _startTime;
    private float _journeyLength;
    private float _onStageYPosition = 0.0f;
    private float _upYPosition = 10.0f;
    private GameObject _hexaLocked;
    private int _hexaLockedId;
    private bool _randomHexaMove = false;
    private Vector3 _orignal_size;
    private System.Random _rnd;
    private GameObject _locked_hexa_mid_out;
    public float Speed = 10.0f;
    public float DownPosition = -10.0f;
    public int pPatformMoveProba = 25;

    // Constructor
    public LevelManager(PrefabsLoaderManager prefabsLoaderManager, GameManager gameManager)
    {
        _rnd = new System.Random();
        _prefabsLoaderManager = prefabsLoaderManager;
        _gameManager = gameManager;
        _position = new List<Vector3>();
        _hexaIdByColor = new Dictionary<string, int>();
        Hexas = new Dictionary<int, GameObject>();
        _order = new List<int>();
        _parentLevel = _gameManager.InstantiateInGameManager(new GameObject("Level"), new Vector3(0, 0, 0), Quaternion.Euler(0, 30, 0));
        
        //TODO change namescolors depends of faces name
        _hexaPrefabs = new Dictionary<string, GameObject>() {
           {"red", _prefabsLoaderManager.LevelLoader.hexaPrefabRed},
           {"blue", _prefabsLoaderManager.LevelLoader.hexaPrefabBlue},
           {"green", _prefabsLoaderManager.LevelLoader.hexaPrefabGreen},
           {"yellow", _prefabsLoaderManager.LevelLoader.hexaPrefabYellow},
           {"orange", _prefabsLoaderManager.LevelLoader.hexaPrefabOrange},
           {"purple", _prefabsLoaderManager.LevelLoader.hexaPrefabPurple},
            {"out", _prefabsLoaderManager.LevelLoader.hexaPrefabOut},
        };
        InitLevel();
    }

    private void InitLevel()
    {
        int z = 0;
        foreach (KeyValuePair<string,GameObject> hexaInfo in _hexaPrefabs)
        {
            AddNewHexa(hexaInfo.Key, hexaInfo.Value.transform.position, hexaInfo.Value.transform.rotation);
            z += 10;
        }
        //StartRandomHexaMove();
    }

    public void DropHexa(string colorName, int difficultyLevel)
    {
        if (colorName == "red")
        {
            if (_hexaLockedId == _hexaIdByColor["out"])
            {
                colorName = "out";
            }
            else if (!Hexas[_hexaIdByColor["red"]].activeSelf)
            {
                colorName = "out";
            }
        }

        _hexaLockedId = _hexaIdByColor[colorName];
        _hexaLocked = Hexas[_hexaLockedId];
        _gameManager.StartCoroutine(DropHexaCoroutine(difficultyLevel));
    }

    public void AddNewHexa(string color, Vector3 position, Quaternion rotation)
    {
        GameObject hexa = _gameManager.InstantiateInGameManager(_hexaPrefabs[color], position, rotation);
        if(color == "out")
        {
            hexa.SetActive(false);
        }
        hexa.transform.parent = _parentLevel.transform;
        _orignal_size = hexa.transform.localScale;
        _hexaIdByColor.Add(color, _id);
         Hexas.Add(_id, hexa);
        _position.Add(hexa.transform.position);
        _order.Add(_id);
        _id += 1;
    }

    public void ChangeHexaSize(int id, Vector3 size)
    {
        Hexas[id].transform.localScale = new Vector3(size.x, Hexas[id].transform.localScale.y, size.z) ;
    }

    public void ColorSwitch(int difficultyLevel)
    {
        int old_hexa_locked_index = _order.FindIndex(id => id == _hexaLockedId);
        List<int> shuffledOrder = _order.OrderBy(a => Guid.NewGuid()).ToList();
        int new_hexa_locked_index = shuffledOrder.FindIndex(id => id == _hexaLockedId);

        (shuffledOrder[new_hexa_locked_index], shuffledOrder[old_hexa_locked_index]) 
            = (shuffledOrder[old_hexa_locked_index], shuffledOrder[new_hexa_locked_index]);
        int i = 0;
        float hexa_change_proba = difficultyLevel * 4.2f; // 4.2 enuser the proba never exceed 60%
        float hexa_mid_proba = 50.0f;
        foreach (int order in shuffledOrder) {
            if (_rnd.Next(1, 100) < hexa_change_proba)
            {
                if(i != old_hexa_locked_index)
                {
                    Vector3 hexa_size = Hexas[order].transform.localScale;
                    float scale_pourcentage = 0.80f;
                    ChangeHexaSize(order, new Vector3(hexa_size.x * scale_pourcentage, hexa_size.y * scale_pourcentage, hexa_size.z * scale_pourcentage));
                }
            }
                var oldPosition = Hexas[order].transform.position;
            Hexas[order].transform.position = new Vector3(_position[i].x, oldPosition.y, _position[i].z);
            _position[i] = Hexas[order].transform.position;
            i += 1;
        }
        if (_rnd.Next(1, 100) < hexa_mid_proba )
        {
            if (_hexaLockedId != _hexaIdByColor["out"])
            {
               _locked_hexa_mid_out = Hexas[_hexaIdByColor["out"]];
               Hexas[_hexaIdByColor["out"]].SetActive(false);
               Hexas[_hexaIdByColor["red"]].SetActive(true);
            }
            else
            {
                _locked_hexa_mid_out = Hexas[_hexaIdByColor["red"]];
                Hexas[_hexaIdByColor["out"]].SetActive(true);
                Hexas[_hexaIdByColor["red"]].SetActive(false);
            }
        }
        else
        {
            if(_hexaLockedId != _hexaIdByColor["red"])
            {
                _locked_hexa_mid_out = Hexas[_hexaIdByColor["red"]];
                Hexas[_hexaIdByColor["out"]].SetActive(true);
                Hexas[_hexaIdByColor["red"]].SetActive(false);
            } else
            {
                _locked_hexa_mid_out = Hexas[_hexaIdByColor["out"]];
                Hexas[_hexaIdByColor["out"]].SetActive(false);
                Hexas[_hexaIdByColor["red"]].SetActive(true);
            }
        }
        _order = shuffledOrder;
    }

    public void StartRandomHexaMove()
    {
        _randomHexaMove = true;
        _gameManager.StartCoroutine(RandomHexaMoveCoro());
    }


    public void StopRandomHexaMove()
    {
        _randomHexaMove = false;
    }

    IEnumerator RandomHexaMoveCoro()
    {
        while (_randomHexaMove)
        {
 
            if (_rnd.Next(0, 100) < pPatformMoveProba){
                _randomHexaMove = false;
                GameObject current_hexa = Hexas[_rnd.Next(0, Hexas.Count)];
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
        Vector3 topPosition = hexa.transform.position;
        Vector3 downPosition = new Vector3(hexa.transform.position.x, topPosition.y - 0.2f, hexa.transform.position.z);
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(topPosition,downPosition);
        while (downPosition.y < hexa.transform.position.y)
        {
            float distCovered = (Time.time - startTime) * 0.3f;
            float fractionOfJourney = distCovered / journeyLength;
            if (true)
            {
               hexa.transform.position = Vector3.Lerp(
               topPosition,
               downPosition,
               fractionOfJourney);
            }
            yield return 0;
        }
    }

    
// COROUTINE 
    IEnumerator DropHexaCoroutine(int difficultyLevel)
    {
        _startTime = Time.time;
        _journeyLength = Vector3.Distance(new Vector3(0, 0, 0), new Vector3(0, DownPosition, 0));
        while (_yPos > DownPosition)
        {
            float distCovered = (Time.time - _startTime) * Speed * Time.timeScale;
            float fractionOfJourney = distCovered / _journeyLength;

            foreach (GameObject hexa in Hexas.Values)
            {
                if (!System.Object.ReferenceEquals(hexa, _hexaLocked))
                {
                   hexa.transform.position = Vector3.Lerp(
                   new Vector3(hexa.transform.position.x, 0, hexa.transform.position.z),
                   new Vector3(hexa.transform.position.x, DownPosition, hexa.transform.position.z),
                   fractionOfJourney);
                    _yPos = hexa.transform.position.y;
                    if(hexa.transform.localScale.x > 0)
                        hexa.transform.localScale -= Vector3.one * Time.deltaTime * Time.timeScale;
                }
            }
            yield return 0;
        }
        foreach (GameObject hexa in Hexas.Values)
        {
            if (!System.Object.ReferenceEquals(hexa, _hexaLocked))
            {
                hexa.transform.localScale = Vector3.one;
                hexa.transform.position = new Vector3(hexa.transform.position.x, _upYPosition, hexa.transform.position.z);
                hexa.SetActive(false);
            }
        }
        ColorSwitch(difficultyLevel);
        HexaComeBack();
    }

    public void HexaComeBack()
    {
        _gameManager.StartCoroutine(HexaComeBackCoroutine());
    }
    IEnumerator HexaComeBackCoroutine()
    {
        float speedDown = 15;
        foreach (GameObject hexa in Hexas.Values)
        {
            if (!System.Object.ReferenceEquals(hexa, _locked_hexa_mid_out))
            {
                hexa.SetActive(true);
            }
        }
        _startTime = Time.time;
        _journeyLength = Vector3.Distance(new Vector3(0, 0, 0), new Vector3(0, _upYPosition, 0));
        _yPos = _upYPosition;
        while (_yPos > _onStageYPosition)
        {
            float distCovered = (Time.time - _startTime) * speedDown * Time.timeScale;
            float fractionOfJourney = distCovered / _journeyLength;
            foreach (GameObject hexa in Hexas.Values)
            {
                if (!System.Object.ReferenceEquals(hexa,_hexaLocked))
                {
                    hexa.transform.position = Vector3.Lerp(
                   new Vector3(hexa.transform.position.x, _upYPosition, hexa.transform.position.z),
                   new Vector3(hexa.transform.position.x, _onStageYPosition, hexa.transform.position.z),
                   fractionOfJourney);
                   _yPos = hexa.transform.position.y;
                }
            }
            yield return 0;
        }

    }
}
