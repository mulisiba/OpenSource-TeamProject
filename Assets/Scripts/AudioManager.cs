using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;

    [Header("SFX Clips")]
    public AudioClip buttonClickSFX; // 버튼 클릭 소리
    public AudioClip coinCollectSFX; // 동전 먹는 소리
    public AudioClip bulletHitSFX; // 총알 맞는 소리
    public AudioClip DeadSFX; // 적이 죽는 소리


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource가 없다면 추가
            Debug.LogWarning("SoundManager: AudioSource 컴포넌트가 없어 새로 추가했습니다.");
        }
    }

    // 버튼 클릭 시 호출될 함수
    public void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSFX != null)
        {
            audioSource.PlayOneShot(buttonClickSFX); // AudioSource.PlayOneShot()으로 클립 재생
        }
        else
        {
            Debug.LogWarning("SoundManager: AudioSource 또는 buttonClickSFX가 설정되지 않았습니다.");
        }
    }

    // 동전 먹는 소리 재생 함수
    public void PlayCoinCollectSound()
    {
        if (audioSource != null && coinCollectSFX != null)
        {
            audioSource.PlayOneShot(coinCollectSFX);
            Debug.Log("Coin collect sound played.");
        }
        else
        {
            Debug.LogWarning("SoundManager: AudioSource 또는 coinCollectSFX가 설정되지 않았습니다.");
        }
    }

    // 총알 맞는 소리 재생 함수
    public void PlayBulletHitSound()
    {
        if (audioSource != null && bulletHitSFX != null)
        {
            audioSource.PlayOneShot(bulletHitSFX);
            Debug.Log("Bullet hit sound played.");
        }
        else
        {
            Debug.LogWarning("SoundManager: AudioSource 또는 bulletHitSFX가 설정되지 않았습니다.");
        }
    }

    // 죽는 소리 재생 함수
    public void PlayDeadSound()
    {
        // DeadSFX가 null이 아닌지 확인
        if (audioSource != null && DeadSFX != null)
        {
            audioSource.PlayOneShot(DeadSFX);
            Debug.Log("Dead sound played.");
        }
        else
        {
            // 경고 메시지도 DeadSFX에 맞게 수정
            Debug.LogWarning("AudioManager: AudioSource 또는 DeadSFX가 설정되지 않았습니다. 죽는 소리를 재생할 수 없습니다.");
        }
    }
}