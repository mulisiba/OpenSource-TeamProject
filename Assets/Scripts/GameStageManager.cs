using UnityEngine;
using TMPro; 

public class GameStageManager : MonoBehaviour
{
    public GameObject stage1Content;
    public GameObject stage2Content;
    public GameObject stage3Content;

    public TextMeshProUGUI stageNumberText; // TextMeshProUGUI ��� ��

    private int currentStage = 0;

    void Start()
    {
        InitializeStages();

        // PlayerPrefs���� ������ �������� ��ȣ�� ������
        // �⺻���� 1 (���� PlayerPrefs�� ���� ���ų� StartSpecificStage�� ȣ����� �ʾҴٸ�)
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
        // ���� Ȱ��ȭ�� �������� �������� �ִٸ� ��Ȱ��ȭ
        if (currentStage == 1 && stage1Content != null) stage1Content.SetActive(false);
        else if (currentStage == 2 && stage2Content != null) stage2Content.SetActive(false);
        else if (currentStage == 3 && stage3Content != null) stage3Content.SetActive(false);

        // �� �������� Ȱ��ȭ
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
            Debug.Log("��� ���������� Ŭ�����߽��ϴ�! ���� ���� �Ǵ� ���� ȭ������.");
        }
    }
}