using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private GameObject connectedDoor;
    [SerializeField] private bool isLocked = false;
    [SerializeField] private float offset = 1.5f;
    private Door connectedDoorScript;

    public void Awake()
    {
        connectedDoorScript = connectedDoor.GetComponent<Door>();
        if (connectedDoorScript != null) connectedDoorScript.connectedDoor = gameObject;
    }
    public override void Interact(Collider2D other)
    {
        if (isLocked) return;
        other.transform.position = connectedDoor.transform.position + connectedDoor.transform.forward * offset;
    }
}
