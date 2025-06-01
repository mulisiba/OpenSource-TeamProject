using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI HealthText;

    // Start 는 게임 시작 시 한 번 호출됨
    private void Start()
    {
        Debug.Log("GameManager Started!");
    }

    // 버튼 클릭 시 호출되는 함수
    public void StartGame()
    {
        Debug.Log("게임 시작 버튼이 눌렸습니다! GameStageScene으로 이동합니다.");
        // 버튼이 눌리면 페이드 효과가 생기며 GameStageScene으로 화면 전환
        SceneManager.LoadScene("GameStageScene");
    }

    // 각 스테이지 버튼이 함수 호출
    public void StartSpecificStage(int stageNumber)
    {
        Debug.Log("STAGE " + stageNumber + " 버튼이 눌렸습니다! GameScene으로 이동합니다.");
        // 다음 씬으로 넘어갈 때 어떤 스테이지를 로드할지 저장
        PlayerPrefs.SetInt("StartStage", stageNumber);
        if (stageNumber == 1)
            SceneManager.LoadScene("GameScene");
    }
    void Update()
    {
        if (coinText != null)
        {
            coinText.text = Coin.coinCount.ToString(); // Coin 스크립트에 따라 다름
        }
        if (HealthText != null)
        {
            HealthText.text = PlayerHealth.currentHealth.ToString(); // PlayerHealth 스크립트에 따라 다름
        }
    }
}
