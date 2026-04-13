using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _increaseBoost;
    [SerializeField] private float _maxSpeed;
    

    private static float _boost = 1;
    private static float _boostdif = 1;
    private int _moveDirection = 1;
    private bool _hasToMove = true;
    private Vector3 _startPos;
    private Vector3 _finishPos;
    private float _trackPercent = 0;
    
    private void Start()
    {        
        _startPos = transform.position;
        _finishPos = _startPos;
        _finishPos.x = -_startPos.x;
    }

    void Update()
    {
        if (_hasToMove == true)        
        {            
            _trackPercent += _moveDirection * _speed * _boostdif * Time.deltaTime;
            float x = (_finishPos.x - _startPos.x) * _trackPercent + _startPos.x;       
            
            transform.position = new Vector3(x, _startPos.y, _startPos.z);
            
            if ((_moveDirection == 1 && _trackPercent > 0.9f) || (_moveDirection == -1 && _trackPercent < .1f))
            {
                _moveDirection *= -1;
            }
        }
    }

    public void StopMovement() => _hasToMove = false;    

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void IncreaseSpeedPlatform()
    {       
        if (_boost <= _maxSpeed)
        {
            _boost += _increaseBoost;
        }
        else if(_boost <= _maxSpeed + 0.5)
        {
            _boost += _increaseBoost / 10;
        }
        
        if (_boost <= 1f)
        {
            _boostdif = Random.Range(_boost, _boost + 0.25f);
        }
        else
        {
            _boostdif = Random.Range(_boost - 0.35f, _boost + 0.1f);
        }       
       
        //Debug.Log("_boost:" + _boost);
        //Debug.Log("_boostdif:" + _boostdif);
    }

    public void SetToDefault()
    {
        _speed = 0.3f;
        _boost = 1f;
        _boostdif = 1f;
    }
}
