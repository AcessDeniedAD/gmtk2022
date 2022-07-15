using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class HudManager
{
    private HudLoader hudLoader;
    private Panels panels;
    private GameObject currentCanvas;

    public HudManager(PrefabsLoaderManager prefabsLoaderManager)
    {
        //hudLoader = prefabsLoaderManager.HudLoader;
        //EventsManager.StartListening(nameof(LivingObjectEvents.DamageHandler), DisplayDamage);
        //currentCanvas = GameObject.Instantiate(hudLoader.Canvas);
        //panels = currentCanvas.GetComponent<Panels>();
      
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
