using UnityEngine;

public class InteractableTutorialRock : MonoBehaviour, IInteractable
{
    public float interactionRange;
    public bool isInRange;
    public GameObject marker;
    public AudioSource useRockAudio;
    public GameObject interactableText;
    public Player player;
    public GameManager gameManager;
    public DeathManager deathManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathManager = DeathManager.deathManager;
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
        if (useRockAudio != null)
        {
            AudioSource.PlayClipAtPoint(useRockAudio.clip, transform.position);
        }
        StartCoroutine(deathManager.HandleDeathCoroutine("Tutorial"));
    }
}
