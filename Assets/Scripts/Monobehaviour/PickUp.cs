using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        player.totalResources++;

        Debug.Log("Recursos: " + player.totalResources);

        Destroy(gameObject);
    }
}