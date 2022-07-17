using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Random = System.Random;

public class SoundManager : MonoBehaviour
{
    public SoundLoader soundLoader;

    AudioSource audioSource;

    PlayerManager _playerManager;

    Vector3 playerDir;

    [Inject]
    void Inject(PlayerManager playerManager, StatesManager statesManager)
    {
        _playerManager = playerManager;
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //EventsManager.StartListening(nameof(StatesEvents.OnRollDiceIn), RotateAround);
        EventsManager.StartListening(nameof(StatesEvents.OnDiceIsShowedIn), PlayDiceShownSound);
        EventsManager.StartListening("PlatformFalling", PlayPlatformSound);
        EventsManager.StartListening("PlayerFell", PlayFallSound);
        EventsManager.StartListening("PlayerJump", PlayJumpSound);
        EventsManager.StartListening("PlayerLand", PlayLandSound);
        EventsManager.StartListening("PlayerCatchCoin", PlayCoinSound);
        EventsManager.StartListening("CoinFalling", PlayCoinFallingSound);
        EventsManager.StartListening("CrowdCheer", PlayCrowdCheer);
        EventsManager.StartListening(nameof(StatesEvents.OnLooseIn), PlayFallSound);

        audioSource = GetComponent<AudioSource>();
    }

    //private void Update()
    //{
    //    playerDir = _playerManager.Player.GetComponent<PlayerController>().PlayerDirection;
    //    if(playerDir.magnitude>0.1)
    //    {
    //        StartCoroutine(PlayStepSoundCoroutine());
    //    }
    //}

    //private IEnumerator PlayStepSoundCoroutine()
    //{
    //    AudioClip[] stepSounds = new AudioClip[4] { soundLoader._playerStep3, soundLoader._playerStep1, soundLoader._playerStep2, soundLoader._playerStep4 }
    //    while (true)
    //    {
    //        audioSource.PlayOneShot(Random.Next(0, stepSounds.Length));
    //        yield return new WaitForSecondsRealtime(1f * playerDir.magnitude);
            
    //    }
    //}

    public void PlayDiceShownSound(Args args)
    {
        audioSource.PlayOneShot(soundLoader._diceStopping);
    }

    public void PlayPlatformSound(Args args)
    {
        audioSource.PlayOneShot(soundLoader._platformFall);
    }

    public void PlayFallSound(Args args)
    {
        audioSource.PlayOneShot(soundLoader._playerFell);
    }

    public void PlayJumpSound(Args args)
    {
        audioSource.PlayOneShot(soundLoader._playerJump);
    }

    public void PlayLandSound(Args args)
    {
        audioSource.PlayOneShot(soundLoader._playerLand);
    }

    public void PlayCoinSound(Args args)
    {
        audioSource.PlayOneShot(soundLoader._catchCoin);
    }

    public void PlayCrowdCheer(Args args)
    {
        audioSource.PlayOneShot(soundLoader._crowdCheer);
    }

    public void PlayCoinFallingSound(Args args)
    {
        audioSource.PlayOneShot(soundLoader._coinFalling);
    }

    public void PlayJackPotSound(Args args)
    {
        audioSource.PlayOneShot(soundLoader._jackPot);
    }
}
