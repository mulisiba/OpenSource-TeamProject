using UnityEngine;
using Yarn.Unity;

public class NPCInteraction : MonoBehaviour
{
    public string yarnNode = "OldMan"; // Yarn 노드 이름
    private bool isPlayerInRange = false;
    private DialogueRunner dialogueRunner;

    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler("mark_done", () => {
            enabled = false;
        });

    }

    void Update()
    {
        if (isPlayerInRange )
        {
            if (!dialogueRunner.IsDialogueRunning)
            {
                dialogueRunner.StartDialogue(yarnNode);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }


}
