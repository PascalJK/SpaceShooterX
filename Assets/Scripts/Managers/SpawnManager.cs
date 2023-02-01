using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab, _enemyContainer;
    [SerializeField] GameObject _powerupAudio, _explosionAudio;
    [SerializeField] List<GameObject> _powerups;
    [SerializeField] float _enemySpawnWaitTime = .5f;

    bool _isPlayerDead;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEmemyRoutine());
        StartCoroutine(SpawnRandomPowerupRoutine());
    }

    Vector2 SpawnPosition()
    {
        // Was used to spawn powerups..
        //var pos = new Vector3(Random.Range(-9f, 9f), 7f);
        return new Vector3(Random.Range(-9.30f, 9.30f), 7f);
    }

    IEnumerator SpawnEmemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (!_isPlayerDead)
        { 
            var enemy = Instantiate(_enemyPrefab, SpawnPosition(), Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnWaitTime);
        }
    }

    IEnumerator SpawnRandomPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (!_isPlayerDead)
        {
            var powerup = _powerups[Random.Range(0, _powerups.Count)];
            yield return new WaitForSeconds(Random.Range(5, 6));
            Instantiate(powerup, SpawnPosition(), Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _isPlayerDead = true;
        ExplosionAudio();
        StopAllCoroutines();
    }

    public void ExplosionAudio()
    {
        var obj = Instantiate(_explosionAudio);
        Destroy(obj, 2f);
    }

    public void PowerupAudio()
    {
        var obj = Instantiate(_powerupAudio);
        Destroy(obj, 2f);
    }
}
