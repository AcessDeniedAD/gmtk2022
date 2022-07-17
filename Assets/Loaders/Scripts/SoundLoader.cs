using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SoundLoader", menuName = "ScriptableObjects/SoundLoader", order = 3)]
public class SoundLoader : ScriptableObject
{
    public AudioClip clip1;

    //Quand la couleur s'affiche
    public AudioClip _diceStopping;

    public AudioClip _ambiance;

    // Quand les plateformes tombent
    public AudioClip _platformFall;

    public AudioClip _catchCoin;

    public AudioClip _music;

    public AudioClip _playerFell;

    public AudioClip _playerStep1;

    public AudioClip _playerStep2;

    public AudioClip _playerStep3;

    public AudioClip _playerStep4;

    public AudioClip _playerJump;

    public AudioClip _playerLand;

    public AudioClip _crowdCheer;

    public AudioClip _coinFalling;

    public AudioClip _jackPot;
}

