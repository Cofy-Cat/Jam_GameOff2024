using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : Interactable
{
    // Create serialized field for the connected scene, load the scene when interacted
    [SerializeField] private string connectedScene;

    [SerializeField] private bool isLocked = false;
    public override void Interact(Collider2D other)
    {
        if (isLocked) return;
        // Go to the next scene
        SceneManager.LoadScene(connectedScene);
    }
}
