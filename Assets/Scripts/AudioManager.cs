using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;

    [Header("SFX Clips")]
    public AudioClip buttonClickSFX; // ��ư Ŭ�� �Ҹ�
    public AudioClip coinCollectSFX; // ���� �Դ� �Ҹ�
    public AudioClip bulletHitSFX; // �Ѿ� �´� �Ҹ�
    public AudioClip DeadSFX; // ���� �״� �Ҹ�


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
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource�� ���ٸ� �߰�
            Debug.LogWarning("SoundManager: AudioSource ������Ʈ�� ���� ���� �߰��߽��ϴ�.");
        }
    }

    // ��ư Ŭ�� �� ȣ��� �Լ�
    public void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSFX != null)
        {
            audioSource.PlayOneShot(buttonClickSFX); // AudioSource.PlayOneShot()���� Ŭ�� ���
        }
        else
        {
            Debug.LogWarning("SoundManager: AudioSource �Ǵ� buttonClickSFX�� �������� �ʾҽ��ϴ�.");
        }
    }

    // ���� �Դ� �Ҹ� ��� �Լ�
    public void PlayCoinCollectSound()
    {
        if (audioSource != null && coinCollectSFX != null)
        {
            audioSource.PlayOneShot(coinCollectSFX);
            Debug.Log("Coin collect sound played.");
        }
        else
        {
            Debug.LogWarning("SoundManager: AudioSource �Ǵ� coinCollectSFX�� �������� �ʾҽ��ϴ�.");
        }
    }

    // �Ѿ� �´� �Ҹ� ��� �Լ�
    public void PlayBulletHitSound()
    {
        if (audioSource != null && bulletHitSFX != null)
        {
            audioSource.PlayOneShot(bulletHitSFX);
            Debug.Log("Bullet hit sound played.");
        }
        else
        {
            Debug.LogWarning("SoundManager: AudioSource �Ǵ� bulletHitSFX�� �������� �ʾҽ��ϴ�.");
        }
    }

    // �״� �Ҹ� ��� �Լ�
    public void PlayDeadSound()
    {
        // DeadSFX�� null�� �ƴ��� Ȯ��
        if (audioSource != null && DeadSFX != null)
        {
            audioSource.PlayOneShot(DeadSFX);
            Debug.Log("Dead sound played.");
        }
        else
        {
            // ��� �޽����� DeadSFX�� �°� ����
            Debug.LogWarning("AudioManager: AudioSource �Ǵ� DeadSFX�� �������� �ʾҽ��ϴ�. �״� �Ҹ��� ����� �� �����ϴ�.");
        }
    }
}