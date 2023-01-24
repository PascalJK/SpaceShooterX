using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 3.5f;
    [SerializeField] GameObject _leaser;
    [SerializeField] float _fireRate = .15f;
    [SerializeField] float _canFire = -1f;
    [SerializeField] int _health = 100;

    void Start()
    {
        transform.position = Vector3.zero;
    }

    void Update()
    {
        CalculateMovment();

        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
            FireLeaser();
    }

    private void CalculateMovment()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var direction = new Vector3(horizontalInput, verticalInput);
        transform.Translate(_speed * Time.deltaTime * direction);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 0));

        if (transform.position.x > 11)
            transform.position = new Vector3(-11, transform.position.y);
        else if (transform.position.x < -11)
            transform.position = new Vector3(11, transform.position.y);
    }

    private void FireLeaser()
    {
        _canFire = Time.time + _fireRate;

        var pos = new Vector3(transform.position.x, transform.position.y + .8f);
        Instantiate(_leaser, pos, Quaternion.identity);
    }

    public void DamageTaken()
    {
        _health -= 20;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
