/*First Villager

Villager: Your Majesty, you've returned! But... you look so different. What happened to you in the dungeons?
Me: Dungeons? I remember nothing but shadows and cold stone walls. Tell me, what has happened?
Villager: It's been miserable since you were taken. The usurper, Loris, he struck you down and claimed your crown.
Me: Loris? My own advisor turned against me?
Villager: Yes, and under his rule, the kingdom has suffered greatly. We've awaited your return, our true king.
Me: My crown... I had forgotten. The weight of it, the responsibility.
Villager: You were more than just a ruler; you were a beacon of hope. We need you to reclaim the throne and free us from Loris's tyranny
Me: But how do i reclaim my throne
Villager: Do not worry, let me teach you some skills...
*/

/* VillagerTP

Villager: Ah, brave hero! You've done well to reach this point. But the true challenge lies ahead.
Me: What must I do next?
Villager: You must journey to the castle. Within its walls, enemies and the false king await.
Me: The king... He betrayed us all. I will face him."
Villager: Be cautious. The castle is heavily guarded, and the king is a formidable foe."
Me: I am ready. How do I reach the castle?"
Villager: I possess an ancient spell of teleportation. When you are ready step on to the white square. Once there, fight bravely and reclaim your rightful place as our ruler.
Me: I am prepared. Let's do this.
Villager: Very well. May the light guide you. *chants an ancient spell* Farewell, hero, until you return as our king!
*/


using UnityEngine;
using TMPro;

public class VillagerSystem : MonoBehaviour
{
    public GameObject textBox;
    public TextMeshProUGUI textComponent;
    public string[] dialogueLines; // Make this public so you can set it in the editor

    private int currentLineIndex = 0;
    private bool playerInRange = false;

    void Start()
    {
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
