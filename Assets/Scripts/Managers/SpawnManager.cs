using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab, _enemyContainer;
    bool _isPlayerDead;

    void Start()
    {
        StartCoroutine(SpawnEmemy());
    }

    IEnumerator SpawnEmemy()
    {
        while (!_isPlayerDead)
        { 
            var pos = new Vector3(Random.Range(-11, 11), 7f);
            var enemy = Instantiate(_enemyPrefab, pos, Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(.5f);
        }
    }

    public void OnPlayerDeath()
    {
        _isPlayerDead = true;
    }
}
