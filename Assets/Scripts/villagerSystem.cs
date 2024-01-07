/* First Villager

Villager: Your Majesty, you've returned! But... you look so different. What happened to you in the dungeons?
Me: Dungeons? I remember nothing but shadows and cold stone walls. Tell me, what has happened?
Villager: It's been miserable since you were taken. The usurper, Morena, she struck you down and claimed your crown.
Me: Morena? My own advisor turned against me?
Villager: Yes, and under her rule, the kingdom has suffered greatly. We've awaited your return, our true king.
Me: My crown... I had forgotten. The weight of it, the responsibility.
Villager: You were more than just a ruler; you were a beacon of hope. We need you to reclaim the throne and free us from Morena's tyranny
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
Eric The Wise: You may need to pickup items such as key, use 'E' to pickup. Also, you will not be able to jump or crouch when you have your sword to make sure she don't hear us.
Me: I see, thanks for the information.
Eric The Wise: Behind me, I gift you some armour to help you fight these villians, don't tell anyone, I will lose(die) my job.
Me: I will use this armour, thank you very much for this awesome gift.
Eric The Wise: Yes, it is. With these skills, you are ready to face the challenges ahead. Remember if you need pause, just press ESC. 
w*/

/* VillagerTP

Villager: Ah, brave hero! You've done well to reach this point. But the true challenge lies ahead.
Me: What must I do next?
Villager: You must journey to the castle. Within its walls, enemies and the false leader await.
Me: Morena... She betrayed us all. I will face her."
Villager: Be cautious. The castle is heavily guarded, and Morena is a formidable foe."
Me: I am ready. How do I reach the castle?"
Villager: I possess an ancient spell of teleportation. When you are ready step on to the white square. Once there, fight bravely and reclaim your rightful place as our ruler.
Me: I am prepared. Let's do this.
Villager: Very well. May the light guide you. *chants an ancient spell* Farewell, hero, until you return as our king! Also, eat some food on the table.
*/

/* VillagerFM

Villager: Wow, you defeated against all these villian and here at her lair.
Me: What can I say, I am the rightful King.
Villager: That was right, I remember all you fights, oh well, across this corridor is her lair. Open the doors and defeat her and become our King.
Me: Morena.. She will get what she deserves, but anything I should know before I go there.
Villager: Be cautious. The lair is heavily guarded with 5 guards, and Morena herself."
Me: I am ready.
Villager: Great, check your health and armour everytime, near me there is food and a new set of armour.
Me: I am prepared. Let's do this. Thanks for the food and armour.
*/


using UnityEngine;
using TMPro;

public class VillagerSystem : MonoBehaviour
{
    public GameObject textBox1;
    public GameObject textBox2;
    public TextMeshProUGUI textComponent1;
    public TextMeshProUGUI textComponent_2;
    public string[] dialogueLines;

    public bool isVillagerTP = false;
    public Animator villagerAnimator;

    private int currentLineIndex = 0;
    private bool playerInRange = false;

    void Start()
    {
        textBox1.SetActive(false);
        textBox2.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (!textBox1.activeInHierarchy)
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
        textBox1.SetActive(true);
        textComponent1.text = dialogueLines[currentLineIndex];
        UpdateTextComponent2();
    }

    void AdvanceDialogue()
    {
        currentLineIndex++;

        if (currentLineIndex < dialogueLines.Length)
        {
            textComponent1.text = dialogueLines[currentLineIndex];
            UpdateTextComponent2();
        }
        else
        {
            textBox1.SetActive(false);
            textBox2.SetActive(false);
            currentLineIndex = 0;
        }
    }

    void UpdateTextComponent2()
    {
        textBox2.SetActive(true);

        if (currentLineIndex >= dialogueLines.Length - 1)
        {
            textComponent_2.text = "End Of Conversation";
        }
        else
        {
            textComponent_2.text = "Press [F] For Next Line";
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
            textBox1.SetActive(false);
            textBox2.SetActive(false);
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

