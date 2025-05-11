using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int coinCount = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            coinCount++;
            Destroy(gameObject);
        }
    }
}
