using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject connectedDoor;
    [SerializeField] private bool isLocked = false;
    [SerializeField] private float offset = 1.5f;
    [SerializeField] private Interactable interactable;
    private Door connectedDoorScript;

    public void Awake()
    {
        connectedDoorScript = connectedDoor.GetComponent<Door>();
        if (connectedDoorScript != null) connectedDoorScript.connectedDoor = gameObject;
        if (interactable != null)
        {
            Debug.Log("Door Interactable found");
            interactable.OnInteract += Interact;
        }
    }
    private void Interact(Collider2D other)
    {
        Debug.Log("Interacting with door");
        if (isLocked) return;
        other.transform.position = connectedDoor.transform.position + connectedDoor.transform.forward * offset;
    }
}
