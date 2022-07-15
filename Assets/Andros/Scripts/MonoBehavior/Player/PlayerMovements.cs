using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMovements : BaseLivingObject
{
    [TagSelector]
    public List<string> TagsWhoPlayerCanBounce = new List<string> { };

    [HideInInspector]
    public Vector3 Direction;
    [HideInInspector]
    public float CurrentSpeed;
    [HideInInspector]
    public float NormalSpeed;
    public delegate void PlayerDash(InputAction.CallbackContext context, Vector3 dir, float speed);
    public event PlayerDash PlayerDashHandler;
    public MMFeedbacks DashFeedBacks;
    private bool canDash = true;
    
    void Start()
    {
        CurrentSpeed = GetStats().Speed;
        NormalSpeed = CurrentSpeed;
        PlayerDashHandler = NormalDash;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Direction * Time.deltaTime * CurrentSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TagsWhoPlayerCanBounce.Contains(collision.transform.tag))
        {
            Direction = Vector3.Reflect(Direction, collision.contacts[0].normal).normalized;
        }
    }
    public void Dash(InputAction.CallbackContext context, Vector3 direction,float speed)
    {
        PlayerDashHandler?.Invoke(context, direction, speed);
    }
    public void NormalDash(InputAction.CallbackContext context, Vector3 dir, float speed)
    {
        if (context.started && canDash)
        {
            Direction = dir.normalized;
            CurrentSpeed = speed;
            DashFeedBacks?.PlayFeedbacks();
            StartCoroutine(ProgressiveReturnToNormalSPeed(9f,false));
        }
    }
    public void SetRecoil(Vector3 dir, float velocity, float normalSpeedTimeRatio)
    {
        Direction = dir;
        CurrentSpeed += velocity;
        StartCoroutine(ProgressiveReturnToNormalSPeed(normalSpeedTimeRatio, false));
    }
    IEnumerator ProgressiveReturnToNormalSPeed(float timeFrame, bool canDash)
    {
        this.canDash = canDash;
        float x = timeFrame;  // time frame
        float f = 0;
        Func<bool> diff;

        while (Math.Abs( CurrentSpeed - NormalSpeed )>1)
        {

            f += Time.deltaTime;
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, NormalSpeed,  f / x);    
            yield return null;
        }
        CurrentSpeed = NormalSpeed;
        this.canDash = true;
        Debug.Log("sortie");

    }
}
