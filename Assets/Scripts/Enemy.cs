using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 4f;
    readonly float _destroyEnemyWaitTime = 2.8f;

    Player _player;
    Animator _animator;
    SpawnManager _spawnManager;

    void Start()
    {
        _player = GameObject.Find(nameof(Player)).GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();
        _spawnManager = GameObject.Find(nameof(SpawnManager)).GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
            /*var randX_Pos = Random.Range(-11, 11);
            transform.position = new Vector3(randX_Pos, 7);*/
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.TryGetComponent<Player>(out var player))
                player.DamageTaken();
            DestroyEnemyShip();
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            if (_player != null)
                _player.AddScore();
            DestroyEnemyShip();
        }
    }

    void DestroyEnemyShip()
    {
        _animator.SetTrigger("OnEnemyDeath");
        _spawnManager.ExplosionAudio();
        _speed = 0;
        Destroy(gameObject, _destroyEnemyWaitTime);
    }
}
