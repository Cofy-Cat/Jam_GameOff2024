using UnityEngine;

public class Pickupable : MonoBehaviour
{
    [SerializeField] private int itemId = 0;
    [SerializeField] private string itemName = "Default Item";
    [SerializeField] private Interactable interactable;
    public void Awake()
    {
        if (interactable != null)
        {
            Debug.Log("Pickupable Interactable found");
            interactable.OnInteract += Interact;
        }
    }
    public void Interact(Collider2D other)
    {
        // Add the item to the player's inventory (inventory system not implemented)
        Debug.Log("Picked up " + itemName);
        Destroy(gameObject);
    }
}
