using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private bool inRange = false;
    public abstract void Interact();

    public void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    public void OnCollisionStay2D(Collision2D other)
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }
}
