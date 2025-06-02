using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int coinCount = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            coinCount++;

            // SoundManager�� �̱��� �ν��Ͻ��� ���� ���� �Դ� �Ҹ� ���
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayCoinCollectSound();
            }
            else
            {
                Debug.LogWarning("Coin: SoundManager �ν��Ͻ��� ã�� �� �����ϴ�.");
            }

            Destroy(gameObject);
        }
    }
}
