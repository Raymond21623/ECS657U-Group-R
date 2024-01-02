/* First Villager

Villager: Your Majesty, you've returned! But... you look so different. What happened to you in the dungeons?
Me: Dungeons? I remember nothing but shadows and cold stone walls. Tell me, what has happened?
Villager: It's been miserable since you were taken. The usurper, Loris, he struck you down and claimed your crown.
Me: Loris? My own advisor turned against me?
Villager: Yes, and under his rule, the kingdom has suffered greatly. We've awaited your return, our true king.
Me: My crown... I had forgotten. The weight of it, the responsibility.
Villager: You were more than just a ruler; you were a beacon of hope. We need you to reclaim the throne and free us from Loris's tyranny
Me: But how do I reclaim my throne?
Villager: Do not worry, let  Eric The Wise teach you some skills...
*/

/* Eric The Wise

Eric The Wise: Ah, you have arrived! Before you embark on your quest, you must master the basics of movement and combat.
Me: I'm ready to learn. What should I do first?
Eric The Wise: First, let's start with movement. Use 'W' to move forward, 'A' to move left, 'S' to move back, and 'D' to move right. Try moving around to get a feel for it.
Me: (moves around) Alright, that feels natural.
Eric The Wise: Good. Now, press 'Space' to jump. This can help you overcome obstacles or avoid attacks.
Me: (jumps) Got it. What's next?
Eric The Wise: To keep a low profile, use 'C' to crouch.
Me: (crouches) This will be useful.
Eric The Wise: Indeed. Now, for combat. Press 'R' to equip your sword.
Me: (equips sword) The sword feels right in my hands.
Eric The Wise: With your sword equipped, use the left mouse button to strike. For a combo, click it rapidly in succession. Try it out.
Me: (performs a few strikes and combos) Like this?
Eric The Wise: Excellent! Remember, timing and rhythm are key to effective combos. Finally, press 'F' to interact with doors and villagers like myself.
Me: (presses F to interact) Interaction seems simple enough.
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
    public string[] dialogueLines;

    public Animator villagerAnimator;
    public bool isVillagerTP = false;

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
            PlayGreetingAnimation(); 
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

    private void PlayGreetingAnimation()
    {
        if (villagerAnimator != null)
        {
            string animationName = isVillagerTP ? "villagerWave2" : "villagerWave";
            
            villagerAnimator.SetTrigger(animationName);
        }
    }
}

