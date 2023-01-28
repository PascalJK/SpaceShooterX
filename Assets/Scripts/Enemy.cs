using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 4f;

    Player _player;
    Animator _animator;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();
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

            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0.1f;
            Destroy(gameObject, 2.8f);
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            if (_player != null)
                _player.AddScore();

            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0.1f;
            Destroy(gameObject, 2.8f);
        }
    }
}
