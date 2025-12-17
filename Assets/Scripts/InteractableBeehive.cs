using UnityEngine;

public class InteractableBeehive : MonoBehaviour, IInteractable
{
    public float interactionRange;
    public bool isInRange;
    public GameObject marker;
    public AudioSource interactBeehiveAudio;
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
            interactableText.SetActive(true);
            Vector3 targetPosition = new Vector3(player.transform.position.x, interactableText.transform.position.y, player.transform.position.z);
            interactableText.transform.LookAt(targetPosition);
            interactableText.transform.Rotate(0, 180, 0);
        }
        else
        {
            isInRange = false;
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
        if (interactBeehiveAudio != null)
        {
            AudioSource.PlayClipAtPoint(interactBeehiveAudio.clip, transform.position);
        }
        player.health.TakeDamage(4, "Beehive");
        // Release the BEEEES
    }
}
