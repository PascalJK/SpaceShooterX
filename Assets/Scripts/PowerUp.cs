using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float _speed = 3;
    Player _player;

    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    void Update()
    {
        CalculatePowerUpMovment();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.ActivateTrippleShot();
            Destroy(gameObject);
        }
    }

    void CalculatePowerUpMovment()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        if (transform.position.y < -6)
            Destroy(gameObject);
    }
}
