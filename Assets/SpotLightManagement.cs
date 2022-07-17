using UnityEngine;
using System;
using System.Collections;
using Zenject;

public class SpotLightManagement : MonoBehaviour
{
    PlayerManager _playerManager;

    StatesManager _statesManager;

    Transform _playerTr;

    public Transform _centerTr;

    public float rotationDirection = 1;

    public Transform _TargetTr;

    private Coroutine _RotationCoroutine = null;

    [SerializeField]
    private float _interpSpeed = 5f;

    // Start is called before the first frame update
    [Inject]
    void Inject(PlayerManager playerManager, StatesManager statesManager)
    {
        _playerManager = playerManager;
        _statesManager = statesManager;
    }

    private void Awake()
    {
        _playerTr = _playerManager.Player.transform;
        EventsManager.StartListening(nameof(StatesEvents.OnRollDiceIn), RotateAround);
        EventsManager.StartListening(nameof(StatesEvents.OnDiceIsShowedIn), LookAtPlayer);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RotateAround(Args args)
    {
        if(_RotationCoroutine!=null)
        {
            StopCoroutine(_RotationCoroutine);
        }
        _RotationCoroutine = StartCoroutine(RotateAround_Coroutine());
    }

    public void LookAtPlayer(Args args)
    {
        if (_RotationCoroutine != null)
        {
            StopCoroutine(_RotationCoroutine);
        }
        _RotationCoroutine = StartCoroutine(LookAtPlayer_Coroutine());
    }

    private IEnumerator LookAtPlayer_Coroutine()
    {
        Quaternion qto = new Quaternion();
        Quaternion initialRotation = transform.rotation;
        float interpolator = 0f;
        qto.SetFromToRotation(transform.forward,  _playerTr.position - transform.position);

        while (Vector3.Angle(transform.forward, _playerTr.position - transform.position)>5)
        {
            //Debug.Log("lerping");
            transform.rotation = Quaternion.Slerp(initialRotation, qto*initialRotation, interpolator * _interpSpeed);
            interpolator += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        while (true)
        {
            transform.LookAt(_playerTr);
            yield return new WaitForEndOfFrame();
        }
        
    }

    private IEnumerator RotateAround_Coroutine()
    {
        Quaternion qto = new Quaternion();
        Quaternion initialRotation = transform.rotation;
        float interpolator = 0f;
        qto.SetFromToRotation(transform.forward, _TargetTr.position - transform.position);

        while (Vector3.Angle(transform.forward, _TargetTr.position - transform.position) > 5)
        {
            //Debug.Log("lerping");
            transform.rotation = Quaternion.Slerp(initialRotation, qto * initialRotation, interpolator * _interpSpeed);
            interpolator += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //transform.LookAt(_TargetTr);
        while (true)
        {
            //Debug.Log("Rotating");
            transform.RotateAround(transform.position, _centerTr.position - transform.position , rotationDirection * Time.deltaTime * 300f); ;
            yield return new WaitForEndOfFrame();
        }
    }
}
