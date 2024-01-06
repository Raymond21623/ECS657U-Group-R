using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform player, destination;
    public Character character; // Reference to the Character script

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Instead of disabling the player game object, you might disable specific components if needed.
            // For example, disable the controller during teleportation to avoid conflicts:
            character.controller.enabled = false;

            // Teleport the player
            player.position = new Vector3(destination.position.x, player.position.y, destination.position.z);

            // If any components were disabled, re-enable them here:
            character.controller.enabled = true;
        }
    }
}
