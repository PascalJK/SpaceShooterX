using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 3.5f;
    [SerializeField] GameObject _leaser, _multiLeaser;
    [SerializeField] float _fireRate = .15f;
    [SerializeField] float _canFire = -1f;
    [SerializeField] int _health = 100;
    bool _isTrippleShotEnabled;
    private SpawnManager _spawnManager;

    void Start()
    {
        transform.position = Vector3.zero;
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    void Update()
    {
        CalculateMovment();

        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
            FireLeaser();
    }

    void CalculateMovment()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var direction = new Vector3(horizontalInput, verticalInput);
        transform.Translate(_speed * Time.deltaTime * direction);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 0));

        if (transform.position.x > 11)
            transform.position = new Vector3(-11, transform.position.y);
        else if (transform.position.x < -11)
            transform.position = new Vector3(11, transform.position.y);
    }

    void FireLeaser()
    {
        _canFire = Time.time + _fireRate;

        var pos = new Vector3(transform.position.x, transform.position.y + 1.2f);
        if (_isTrippleShotEnabled)
        {
            Instantiate(_multiLeaser, transform.position, Quaternion.identity);
            return;
        }
        Instantiate(_leaser, pos, Quaternion.identity); 
    }

    public void DamageTaken()
    {
        _health -= 20;
        if (_health <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void ActivateTrippleShot()
    {
        StartCoroutine(TrippleShotCoroutine());
    }

    IEnumerator TrippleShotCoroutine()
    {
        _isTrippleShotEnabled = true;
        yield return new WaitForSeconds(5);
        _isTrippleShotEnabled = false;
    }
}
