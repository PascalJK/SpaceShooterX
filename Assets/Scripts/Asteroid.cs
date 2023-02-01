using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 3f;
    [SerializeField] GameObject _explosion;
    [SerializeField] SpawnManager _spawnManager;
    bool isDestroying;

    void Update()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestroying) return;

        if(other.CompareTag("Laser"))
        {
            isDestroying = true;
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(gameObject, .25f);
        }
    }
}
