using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private float _frstverticalOffset;
    [SerializeField] private float _scndtverticalOffset;

    [SerializeField] private TextMeshProUGUI count;

    private float? _lastPositionY = null;
    private float spawnPointPositionY;    

    public static int money;
    private void Awake()
    {
        money = PlayerPrefs.GetInt("Money");
    }
    
    void Start()
    {
        SpawnCoin();
    }

    public void UpdateCount()
    {
        count.text = PlayerPrefs.GetInt("Money").ToString();
    }

    public static void AddCoin()
    {
        money += 1;
        PlayerPrefs.SetInt("Money", money);     
        Debug.Log(money);
    }

    public void AddadMoney()
    {
        money = PlayerPrefs.GetInt("Money");
        money += 5;
        PlayerPrefs.SetInt("Money", money);
        //Debug.Log("money: " + money);
        //Debug.Log("money prefs: " + PlayerPrefs.GetInt("Money"));
    }

    public void SpawnCoin()
    {
        if (_spawnPoint != null)
        {
            spawnPointPositionY = _lastPositionY == null ? _spawnPoint.position.y :
            (float)_lastPositionY + Random.Range(_frstverticalOffset, _scndtverticalOffset);

            _spawnPoint.position = new Vector3(_spawnPoint.position.x, spawnPointPositionY, _spawnPoint.position.z);
            _lastPositionY = spawnPointPositionY;

            Instantiate(_coinPrefab, _spawnPoint.position, Quaternion.identity);
        }        
    }
}
