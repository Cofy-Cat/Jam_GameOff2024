using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject connectedDoor;
    [SerializeField] private bool isLocked = false;
    [SerializeField] private bool isDisabled = false;
    [SerializeField] private float offset = 1.5f;
    private Door connectedDoorScript;

    public void Awake()
    {
        connectedDoorScript = connectedDoor.GetComponent<Door>();
        if (connectedDoorScript != null) connectedDoorScript.connectedDoor = gameObject;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLocked) return;
        if (other.CompareTag("Player"))
        {
            connectedDoorScript.isDisabled = true;
            if (!isDisabled)
            {
                other.transform.position = connectedDoor.transform.position + connectedDoor.transform.forward * offset;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isLocked) return;
        if (other.CompareTag("Player"))
        {
            connectedDoorScript.isDisabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isLocked) return;
        if (other.CompareTag("Player"))
        {
            isDisabled = false;
        }
    }
}
