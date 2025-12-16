using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public float interactionRange;
    public bool isInRange;
    public GameObject interactableText;
    public Player player;
    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = gameManager.player;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceVector = player.transform.position - transform.position;
        if (distanceVector.magnitude < interactionRange)
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }

        if (isInRange)
        {
            interactableText.SetActive(true);
        }
        else
        {
            interactableText.SetActive(false);
        }
    }

    public bool CanInteract()
    {
        if (isInRange)
            return true;
        return false;
    }

    public void Interact(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }
}
