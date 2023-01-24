using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 4f;

    void Start()
    {
        transform.position = new Vector3(Random.Range(-11, 11), 0);
    }

    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        if (transform.position.y < -4.5f)
        {
            var randX_Pos = Random.Range(-11, 11);
            transform.position = new Vector3(randX_Pos, 7);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.TryGetComponent<Player>(out var player))
                player.DamageTaken();
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("leaser"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
