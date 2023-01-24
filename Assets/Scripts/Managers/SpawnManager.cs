using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] int _enemyCount;
    [SerializeField] GameObject _enemyPrefab, _enemyContainer;

    void Start()
    {
        StartCoroutine(SpawnEmemy());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnEmemy()
    {
        while (_enemyCount < 5)
        { 
            var pos = new Vector3(Random.Range(-11, 11), 4.5f);
            var enemy = Instantiate(_enemyPrefab, pos, Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            _enemyCount++;
            yield return new WaitForSeconds(.5f);
        }
    }

    public void ReduceEnemyCount()
    {
        _enemyCount--;
    }
}
