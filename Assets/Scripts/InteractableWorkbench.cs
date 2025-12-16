using UnityEngine;

public class InteractableWorkbench : MonoBehaviour, IInteractable
{
    public float interactionRange;
    public bool isInRange;
    public bool isUsingWorkbench;
    public GameObject marker;
    public AudioSource openWorkbenchAudio;
    public GameObject interactableText;
    public Player player;
    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.gameManager;
        player = gameManager.player;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceVector = player.transform.position - marker.transform.position;
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
            if (isUsingWorkbench)
            {
                isUsingWorkbench = false;
                gameManager.HideWorkbenchUI();
            }
        }
    }

    public bool CanInteract()
    {
        if (isInRange && !isUsingWorkbench)
            return true;
        return false;
    }

    public void Interact(Interactor interactor)
    {
        if (openWorkbenchAudio != null)
        {
            AudioSource.PlayClipAtPoint(openWorkbenchAudio.clip, transform.position);
        }
        isUsingWorkbench = true;
        gameManager.DisplayWorkbenchUI();
    }
}
