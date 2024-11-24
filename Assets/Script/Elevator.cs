using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    // Create serialized field for the connected scene, load the scene when interacted
    [SerializeField] private int connectedSceneIndex;

    [SerializeField] private bool isLocked = false;
    [SerializeField] private Interactable interactable;
    public void Awake()
    {
        if (interactable != null)
        {
            Debug.Log("Elevator Interactable found");
            interactable.OnInteract += Interact;
        }
    }
    public void Interact(Collider2D other)
    {
        if (isLocked) return;
        // Go to the next scene
        SceneManager.LoadScene(connectedSceneIndex);
    }
}