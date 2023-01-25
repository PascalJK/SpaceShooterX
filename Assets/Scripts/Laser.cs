using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    float _speed = 8.0f;

    void Update()
    {
        CalculateLaserMovement();
    }

    private void CalculateLaserMovement()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up);

        if (transform.position.y > 8)
        {
            if (gameObject.transform.parent != null)
                Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
