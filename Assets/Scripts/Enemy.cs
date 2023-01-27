using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 4f;

    Player _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
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
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            if (_player != null)
                _player.AddScore();

            Destroy(gameObject);
        }
    }
}
