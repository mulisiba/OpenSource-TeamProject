using UnityEngine;
using TMPro; // TextMeshProUGUI를 사용한다면 추가
// using UnityEngine.SceneManagement; // 여기서는 직접 씬 전환 안 하므로 없어도 됨

public class GameStageManager : MonoBehaviour
{
    public GameObject stage1Content;
    public GameObject stage2Content;
    public GameObject stage3Content;

    public TextMeshProUGUI stageNumberText; // TextMeshProUGUI 사용 시

    private int currentStage = 0;

    void Start()
    {
        InitializeStages();

        // PlayerPrefs에서 시작할 스테이지 번호를 가져옵니다.
        // 기본값은 1 (만약 PlayerPrefs에 값이 없거나 StartSpecificStage가 호출되지 않았다면)
        int initialStage = PlayerPrefs.GetInt("StartStage", 1);
        LoadStage(initialStage);
    }

    void InitializeStages()
    {
        if (stage1Content != null) stage1Content.SetActive(false);
        if (stage2Content != null) stage2Content.SetActive(false);
        if (stage3Content != null) stage3Content.SetActive(false);
    }

    public void LoadStage(int stageToLoad)
    {
        // 현재 활성화된 스테이지 컨텐츠가 있다면 비활성화
        if (currentStage == 1 && stage1Content != null) stage1Content.SetActive(false);
        else if (currentStage == 2 && stage2Content != null) stage2Content.SetActive(false);
        else if (currentStage == 3 && stage3Content != null) stage3Content.SetActive(false);

        // 새 스테이지 활성화
        switch (stageToLoad)
        {
            case 1:
                if (stage1Content != null) stage1Content.SetActive(true);
                break;
            case 2:
                if (stage2Content != null) stage2Content.SetActive(true);
                break;
            case 3:
                if (stage3Content != null) stage3Content.SetActive(true);
                break;
            default:
                Debug.LogWarning("Invalid stage number: " + stageToLoad);
                return;
        }

        currentStage = stageToLoad;
        Debug.Log("Loaded Stage: " + currentStage);

        if (stageNumberText != null)
        {
            stageNumberText.text = "STAGE " + currentStage;
        }
    }

    public void NextStage()
    {
        if (currentStage < 3)
        {
            LoadStage(currentStage + 1);
        }
        else
        {
            Debug.Log("모든 스테이지를 클리어했습니다! 게임 종료 또는 엔딩 화면으로.");
            // 모든 스테이지 클리어 시 GameStartScene으로 돌아가거나 엔딩 씬으로 이동
            // SceneManager.LoadScene("GameStartScene"); // GameStartScene으로 돌아가기
            // SceneManager.LoadScene("EndingScene"); // 엔딩 씬 (필요하다면 SceneManagement 추가)
        }
    }
}