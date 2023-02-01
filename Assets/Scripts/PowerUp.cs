using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float _speed = 3;
    [SerializeField] string _powerupName;

    Player _player;
    SpawnManager _spawnManager;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    void Update()
    {
        CalculatePowerUpMovment();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (_powerupName)
            {
                case "trippleShot":
                    _player.ActivateTrippleShotPowerup();
                    break;
                case "speedBoost":
                    _player.ActivateSpeedPowerup();
                    break;
                case "shieldPowerup":
                    _player.ActivateShieldPowerup();
                    break;
                default:
                    return;
            }
            _spawnManager.PowerupAudio();
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
