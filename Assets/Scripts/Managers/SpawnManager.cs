using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab, _enemyContainer,
        _trippleShotPowerupPrefab;
    bool _isPlayerDead;

    void Start()
    {
        StartCoroutine(SpawnEmemyRoutine());
        StartCoroutine(SpawnTrippleShotPowerupRoutine());
    }

    IEnumerator SpawnEmemyRoutine()
    {
        while (!_isPlayerDead)
        { 
            var pos = new Vector3(Random.Range(-11, 11), 7f);
            var enemy = Instantiate(_enemyPrefab, pos, Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator SpawnTrippleShotPowerupRoutine()
    {
        while (!_isPlayerDead)
        {
            var pos = new Vector3(Random.Range(-11, 11), 7f);
            yield return new WaitForSeconds(Random.Range(5, 15));
            Instantiate(_trippleShotPowerupPrefab, pos, Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _isPlayerDead = true;
    }
}
