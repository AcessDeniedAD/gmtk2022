using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class HudManager
{
    private Panels panels;
    private PrefabsLoaderManager _prefabsLoaderManager;
    private GameObject currentCanvas;
    private GameManager _gamerManager;

    public HudManager(PrefabsLoaderManager prefabsLoaderManager, GameManager gameManager)
    {
        _gamerManager = gameManager;
        _prefabsLoaderManager = prefabsLoaderManager;
        GenerateTitleScreen();
        StartGame();
    }   

    private void GenerateTitleScreen()
    {
        GameObject canvas_prefab = _prefabsLoaderManager.HudLoader.Canvas;
        currentCanvas = _gamerManager.InstantiateInGameManager(canvas_prefab, canvas_prefab.transform.position, canvas_prefab.transform.rotation);
    }

    private void StartGame()
    {
        _gamerManager.StartCoroutine(WaitStartGame());
    }

    public void ShowTitleScreen()
    {
        currentCanvas.SetActive(true);
        StartGame();
    }

    IEnumerator WaitStartGame()
    {   
        while (!_gamerManager.GameIsStart)
        {
             yield return new WaitForSecondsRealtime(5);
            currentCanvas.SetActive(false);
        }
    }
    
    //private void DisplayDamage(Args args)
    //{
    //    //if (args.GetType() != typeof(LivingObjectEvents.LivingObjectDamageArgs))
    //    //    throw new Exception("argument must be a LivingObjectDamageArgs");
    //    //LivingObjectEvents.LivingObjectDamageArgs _args = ((LivingObjectEvents.LivingObjectDamageArgs)args);

    //    //Vector3 DamagePos = Camera.main.WorldToScreenPoint(_args.Go.transform.position);
    //    //var go = GameObject.Instantiate(hudLoader.DamageText, DamagePos + Vector3.up *20, Quaternion.identity,panels.DamageDisplayer.transform);
    //    //go.GetComponent<TextMeshProUGUI>().text =  _args.Damage.ToString();
    //    //var mmFb = go.GetComponent<MMFeedbacks>();
    //    //mmFb.PlayFeedbacks();

    //}
}
