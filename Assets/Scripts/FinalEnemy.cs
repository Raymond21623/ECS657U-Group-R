using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class FinalEnemy : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    Animator animator;

    private Enemy enemyscript;

    public float moveDistance = 5f;
    public float floatHeight = 10f;
    public float floatDownTime = 5f;

    private bool hasTriggered = false;

    public GameObject textBox;
    public TextMeshProUGUI textComponent;

    void Start()
    {
        enemyscript = GetComponent<Enemy>();
        if (enemyscript == null)
        {
            Debug.LogError("Enemy script not found on the final boss!");
        }

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Set the initial floating position
        Vector3 floatPosition = new Vector3(transform.position.x, floatHeight, transform.position.z);
        transform.position = floatPosition;

        agent.enabled = false;
    }

    IEnumerator FloatToGround()
    {
        // Raycast downwards from the floating position to find the ground
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, 100f))
        {
            // Gradually move the enemy down to the ground
            float elapsedTime = 0;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = hitInfo.point;
            endPosition.y += 0.5f;

            while (elapsedTime < floatDownTime)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / floatDownTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Enable the NavMeshAgent once on the ground
            agent.enabled = true;
            agent.Warp(transform.position);
        }
        else
        {
            Debug.LogWarning("Ground not found below enemy's floating position!");
        }
    }

    private void ShowMessage()
    {
        textBox.SetActive(true);
        textComponent.text = "HAHA WELCOME TO MY CASTLE";
        StartCoroutine(HideMessageAfterDelay(4));

    }

    IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textBox.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowMessage();
            StartCoroutine(FloatToGround());
            Debug.Log("Player entered trigger - Enemy will float down");
            hasTriggered = true;
            Debug.Log("Player entered trigger - Enemy will move");
            Vector3 newPosition = GetNewPosition();

            if (NavMesh.SamplePosition(newPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
            }
            else
            {
                Debug.LogWarning("New position for enemy is not on NavMesh!");
            }
        }
    }

    private Vector3 GetNewPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0;
        randomDirection.z *= moveDistance;

        return new Vector3(transform.position.x + 10, transform.position.y, transform.position.z + randomDirection.z);
    }

    public float GetBossHealth
    {
        get { return enemyscript.Health; }
    }
}