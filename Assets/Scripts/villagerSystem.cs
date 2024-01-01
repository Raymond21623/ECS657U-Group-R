using UnityEngine;
using TMPro;

public class VillagerSystem : MonoBehaviour
{
    public GameObject textBox;
    public TextMeshProUGUI textComponent;


    private string[] dialogueLines; 
    private int currentLineIndex = 0; 
    private bool playerInRange = false; 

    void Start()
    {
        dialogueLines = new string[] {
            "Villager: Your Majesty, you've returned! But... you look so different. What happened to you in the dungeons?",
            "Me: Dungeons? I remember nothing but shadows and cold stone walls. Tell me, what has happened?",
            "Villager: It's been miserable since you were taken. The usurper, Loris, he struck you down and claimed your crown.",
            "Me: Loris? My own advisor turned against me?",
            "Villager: Yes, and under his rule, the kingdom has suffered greatly. We've awaited your return, our true king.",
            "Me: My crown... I had forgotten. The weight of it, the responsibility.",
            "Villager: You were more than just a ruler; you were a beacon of hope. We need you to reclaim the throne and free us from Loris's tyranny",
            "Me: But how do i reclaim my throne",
            "Villager: Do not worry, let me teach you some skills...",
        };

        textBox.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (!textBox.activeInHierarchy)
            {
                ShowDialogue();
            }
            else
            {
                AdvanceDialogue();
            }
        }
    }

    void ShowDialogue()
    {
        textBox.SetActive(true);
        textComponent.text = dialogueLines[currentLineIndex];
    }

    void AdvanceDialogue()
    {
        currentLineIndex++;

        if (currentLineIndex < dialogueLines.Length)
        {
            textComponent.text = dialogueLines[currentLineIndex];
        }
        else
        {
            textBox.SetActive(false);
            currentLineIndex = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
            textBox.SetActive(false);
            currentLineIndex = 0;
        }
    }
}
