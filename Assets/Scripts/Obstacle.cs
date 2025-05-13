using UnityEngine;

public class RotatingBlock : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private int damage = 1;

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
