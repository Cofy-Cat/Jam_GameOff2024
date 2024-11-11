using UnityEngine;

public class Locker : Interactable
{
    private GameObject player = null;
    private bool isInLocker = false;
    public override void Interact(Collider2D other)
    {
        Debug.Log("Interacting with locker");
        player = other.gameObject;
        // Disable player
        player.transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
        player.SetActive(false);
        isInLocker = true;
    }

    public override void Update()
    {
        if (isInLocker && Input.GetKeyDown(KeyCode.E))
        {
            player.SetActive(true);
            isInLocker = false;
        }
        else
        {
            base.Update();
        }
    }
}