using UnityEngine;

public class Leaser : MonoBehaviour
{
    [SerializeField]
    float _speed = 8.0f;

    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up);

        if(transform.position.y > 8)
            Destroy(gameObject);
    }
}
