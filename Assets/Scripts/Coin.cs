using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int coinCount = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            coinCount++;

            // SoundManager의 싱글톤 인스턴스를 통해 동전 먹는 소리 재생
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayCoinCollectSound();
            }
            else
            {
                Debug.LogWarning("Coin: SoundManager 인스턴스를 찾을 수 없습니다.");
            }

            Destroy(gameObject);
        }
    }
}
