using UnityEngine;
using UnityEngine.InputSystem;

public class Locker : MonoBehaviour
{
    private GameObject player = null;
    private bool isInLocker = false;
    [SerializeField] private Interactable interactable;
    public void Awake()
    {
        interactable = GetComponent<Interactable>();
        if (interactable != null)
        {
            Debug.Log("Locker Interactable found");
            interactable.OnInteract += Interact;
        }
    }
    public void Interact(Collider2D other)
    {
        Debug.Log("Interacting with locker");
        player = other.gameObject;
        // Disable player
        player.transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
        // Disable player sprite and collider
        player.GetComponentInChildren<SpriteRenderer>().enabled = false;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<PlayerInput>().enabled = false;
        isInLocker = true;
    }

    public void Update()
    {
        if (isInLocker && Input.GetKeyDown(KeyCode.E))
        {
            player.GetComponentInChildren<SpriteRenderer>().enabled = true;
            player.GetComponent<BoxCollider2D>().enabled = true;
            player.GetComponent<PlayerInput>().enabled = true;
            isInLocker = false;
        }
    }
}