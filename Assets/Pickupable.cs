using UnityEngine;

public class Pickupable : Interactable
{
    [SerializeField] private int itemId = 0;
    [SerializeField] private string itemName = "Default Item";
    public override void Interact(Collider2D other)
    {
        // Add the item to the player's inventory (inventory system not implemented)
        Debug.Log("Picked up " + itemName);
        Destroy(gameObject);
    }
}
