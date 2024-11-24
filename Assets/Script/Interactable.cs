using UnityEngine;

public class Interactable : MonoBehaviour
{
    public delegate void InteractAction(Collider2D other);
    public event InteractAction OnInteract;

    public void Interact(Collider2D other)
    {
        if (OnInteract != null)
        {
            Debug.Log("Interacting with " + gameObject.name);
            OnInteract(other);
        }
        else
        {
            Debug.Log("No interact event added.");
        }
    }
}
