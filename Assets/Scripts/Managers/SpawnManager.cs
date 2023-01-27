using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab, _enemyContainer;
    [SerializeField] List<GameObject> _powerups;

    bool _isPlayerDead;

    void Start()
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
        while (!_isPlayerDead)
        { 
            var enemy = Instantiate(_enemyPrefab, SpawnPosition(), Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator SpawnRandomPowerupRoutine()
    {
        while (!_isPlayerDead)
        {
            var powerup = _powerups[Random.Range(0, _powerups.Count)];
            yield return new WaitForSeconds(Random.Range(5, 20));
            Instantiate(powerup, SpawnPosition(), Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _isPlayerDead = true;
    }
}
