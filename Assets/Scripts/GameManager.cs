using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI HealthText;

    // Start �� ���� ���� �� �� �� ȣ���
    private void Start()
    {
        Debug.Log("GameManager Started!");
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void StartGame()
    {
        Debug.Log("���� ���� ��ư�� ���Ƚ��ϴ�! GameStageScene���� �̵��մϴ�.");
        // ��ư�� ������ GameStageScene���� ȭ�� ��ȯ
        SceneManager.LoadScene("GameStageScene");
    }

    // �� �������� ��ư�� �Լ� ȣ��
    public void StartSpecificStage(int stageNumber)
    {
        Debug.Log("STAGE " + stageNumber + " ��ư�� ���Ƚ��ϴ�! GameScene���� �̵��մϴ�.");
        // ���� ������ �Ѿ �� � ���������� �ε����� ����
        PlayerPrefs.SetInt("StartStage", stageNumber);
        if (stageNumber == 1)
            SceneManager.LoadScene("GameScene");
    }
    void Update()
    {
        if (coinText != null)
        {
            coinText.text = Coin.coinCount.ToString(); // Coin ��ũ��Ʈ�� ���� �ٸ�
        }
        if (HealthText != null)
        {
            HealthText.text = PlayerHealth.currentHealth.ToString(); // PlayerHealth ��ũ��Ʈ�� ���� �ٸ�
        }
    }
}
