using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public Player player;
    public static bool isPaused = false;
    public static bool godSceneActive = false;
    public static bool isGameStarted = false;
    public GameObject DeathManager;
    public GameObject pauseMenuUI;
    public GameObject titleScreenUI;
    public GameObject gameplayUI;
    public GameObject creditsUI;
    public GameObject workbenchUI;
    public GameObject takeDamageUI;
    public GameObject godScene;
    //public static AudioSource backgroundMusic;
    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //backgroundMusic = GetComponent<AudioSource>();
        pauseMenuUI.SetActive(false);
        titleScreenUI.SetActive(true);
        gameplayUI.SetActive(false);
        creditsUI.SetActive(false);
        godScene.SetActive(false);
        takeDamageUI.SetActive(false);
        workbenchUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                UnpauseGame();

            }
            else
            {
                PauseGame();
            }
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        titleScreenUI.SetActive(false);
        gameplayUI.SetActive(true);
    }

    public void UnpauseGame()
    {
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        isPaused = true;
    }

    public void GodCutscene()
    {
        if (godSceneActive) 
        {
            godSceneActive = false;
            godScene.SetActive(false);
            player.GetComponentInChildren<Camera>().enabled = true;
            player.GetComponentInChildren<AudioListener>().enabled = true;
        }
        else
        {
            godSceneActive = true;
            godScene.SetActive(true);
            player.GetComponentInChildren<Camera>().enabled = false;
            player.GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    public void DisplayWorkbenchUI()
    {
        workbenchUI.SetActive(true);
    }

    public void HideWorkbenchUI()
    {
        workbenchUI.SetActive(false);
    }

    public void DisplayCredits()
    {
        creditsUI.SetActive(true);
    }

    public void HideCredits()
    {
        creditsUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
