using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool inRange = false;
    private Collider2D collider;
    public virtual void Interact(Collider2D other)
    {
        Debug.Log("Interacting with " + other.name);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            collider = other;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            collider = null;
        }
    }

    public virtual void OnTriggerStay2D(Collider2D other)
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            collider = other;
            Interact(other);
        }
    }

    public virtual void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact(collider);
        }
    }
}
