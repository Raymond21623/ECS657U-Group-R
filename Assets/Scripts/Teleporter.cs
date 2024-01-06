using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform player, destination;
    public Character character;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            character.controller.enabled = false;

            player.position = new Vector3(destination.position.x, player.position.y, destination.position.z);

            character.controller.enabled = true;
        }
    }
}
