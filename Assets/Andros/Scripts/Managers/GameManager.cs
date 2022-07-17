using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public Dictionary<string, IEnumerator> coroutines;
    [HideInInspector] public delegate void GameEventManager();
    [HideInInspector] public static event GameEventManager SystemOnInit;

    [HideInInspector] public static event GameEventManager ApplicationQuitHandler;
    [HideInInspector] public static event GameEventManager ApplicationPauseHandler;
    [HideInInspector] public static event GameEventManager ApplicationFocusHandler;

    [HideInInspector] public static event GameEventManager GameUpdateHandler;
    [HideInInspector] public static event GameEventManager GameFixedUpdateHandler;
    private StatesManager statesManager;
    private PrefabsLoaderManager resourcesLoaderManager;
    public bool GameIsStart;

    [Inject]
    private void Init(StatesManager statesManager, PrefabsLoaderManager resourcesLoaderManager)
    {
        this.statesManager = statesManager;
        this.resourcesLoaderManager = resourcesLoaderManager;
        DontDestroyOnLoad(gameObject);
    }
    private void Awake()
    {
        
    }

    private void Update()
    {
        GameUpdateHandler?.Invoke();
    }

    private void OnMouseDown()
    {

    }

    private void FixedUpdate()
    {
        GameFixedUpdateHandler?.Invoke();
    }
    public void StartCouroutineInGameManager(IEnumerator routine, string routineName)
    {
        if (coroutines == null)
        {
            coroutines = new Dictionary<string, IEnumerator>();
        }
        if (coroutines != null && !coroutines.ContainsKey(routineName))
        {
            Coroutine co = StartCoroutine(routine);
            coroutines.Add(routineName, routine);
        }
        else if (coroutines != null && coroutines.ContainsKey(routineName))
        {
            StopCouroutineInGameManager(routineName);
            Coroutine l_co = StartCoroutine(routine);
            coroutines.Add(routineName, routine);
        }
    }
    public void StartCouroutineInGameManager(IEnumerator routine)//Coroutine avec arret automatique du MonoBehavior
    {
        StartCoroutine(routine);
    }
    public void StopCouroutineInGameManager(string coroutineName)
    {
        if (coroutines.ContainsKey(coroutineName))
        {
            StopCoroutine(coroutines[coroutineName]);
            coroutines.Remove(coroutineName);
        }
    }

    void OnApplicationQuit()
    {
        ApplicationQuitHandler?.Invoke();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            ApplicationFocusHandler?.Invoke();
        }
        else
        {
            ApplicationPauseHandler?.Invoke();
        }
    }

    public GameObject InstantiateInGameManager(UnityEngine.Object original, Vector3 position, Quaternion rotation)
    {
        var go = Instantiate(original, position, rotation) as GameObject;
        return go;
    }
    public void DestroyGameObjectInGameManager(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}