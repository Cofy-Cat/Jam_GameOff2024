using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool inRange = false;
    private Collider2D collider;
    public virtual void Interact(Collider2D other) {
        Debug.Log("Interacting with " + other.name);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            collider = other;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            collider = null;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            collider = other;
            Interact(other);
        }
    }

    public void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact(collider);
        }
    }
}
