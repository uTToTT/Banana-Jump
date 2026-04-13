using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private float _verticalOffset;

    private float? _lastPositionY = null;
    private float spawnPointPositionY;
    GameObject clonePlatform;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Transform randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        spawnPointPositionY = _lastPositionY == null ? randomSpawnPoint.position.y : (float)_lastPositionY + _verticalOffset;

        randomSpawnPoint.position = new Vector3(randomSpawnPoint.position.x, spawnPointPositionY, randomSpawnPoint.position.z);
        _lastPositionY = spawnPointPositionY;

        clonePlatform = Instantiate(_platformPrefab, randomSpawnPoint.position, Quaternion.identity);
    }

    public void DestroyClone()
    {
        Destroy(clonePlatform);
    }

    public void SetBecamePos()
    {
        _lastPositionY -= _verticalOffset; 
    }
}
