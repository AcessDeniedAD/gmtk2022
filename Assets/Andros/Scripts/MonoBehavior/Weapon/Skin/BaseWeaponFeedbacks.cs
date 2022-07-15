using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponFeedbacks : MonoBehaviour
{
    public MMFeedbacks ShootingFeedbacks;
    public MMFeedbacks NoAmmoFeedbacks;
    public MMFeedbacks ReloadFeedbacks;
    public virtual void PlayFBShoot()
    {
        ShootingFeedbacks?.PlayFeedbacks();
    }
    public virtual void PlayFBAnime()
    {
        NoAmmoFeedbacks?.PlayFeedbacks();
    }
    public virtual void PlayFBReload()
    {
        ReloadFeedbacks?.PlayFeedbacks();
    }
}
