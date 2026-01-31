using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Transform> _enemySpawner;
    public GameObject _enemy;
    public float _SpawnSpeed;
    public int _enemyMaxSpawn;
    public int _enemyCount;
    public bool _isSpawning;

    public AudioSource _spawnSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isSpawning = true;
        StartCoroutine(Spawner());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   IEnumerator Spawner()
    {
        while (_isSpawning==true)
        {
            _spawnSound.Play();
            Debug.Log("Spawnoldu");
            var RandomPointSpawner = Random.Range(0, _enemySpawner.Count);
            Transform RandomPointTranform = _enemySpawner[RandomPointSpawner];

            Instantiate(_enemy, RandomPointTranform.position, Quaternion.identity);

            _enemyCount++;

            if (_enemyCount>= _enemyMaxSpawn)
            {
                _isSpawning = false;
            }
            yield return new WaitForSeconds(_SpawnSpeed);
        }

    }
}
