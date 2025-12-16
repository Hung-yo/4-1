using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public static DeathManager deathManager;
    public GameManager gameManager;
    public float deathFadeOutTime;
    public float deathFadeInTime;
    public int totalUniqueDeaths;
    public AudioSource deathDieAudio;
    public AudioSource deathReviveAudio;
    public GodTextManager godTextManager;
    public CanvasGroup deathFadeoutCanvas;
    public GameObject deathFadeoutUI;
    void Awake()
    {
        if (deathManager == null)
        {
            deathManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Dictionary of ways to die
    public Dictionary<string, int> WaysToDie = new Dictionary<string, int>()
    {
        {"Cactus", 0},
        {"Fall_Damage", 0},
        {"Lava", 0},
        {"Workbench_Suicide", 0}
    };

    void Start()
    {
        gameManager = GameManager.gameManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DeathFadeOutCoroutine(CanvasGroup deathUI, float start, float end, float duration)
    {
        deathFadeoutUI.SetActive(true);
        float elapsed = 0f;
        deathUI.alpha = start;
        deathUI.blocksRaycasts = true;
        deathUI.interactable = true;
        while (elapsed < duration)
        {
            deathUI.alpha = Mathf.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        deathUI.alpha = end;
        yield return new WaitForSeconds(.2f);
    }

    private IEnumerator DeathFadeInCoroutine(CanvasGroup deathUI, float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            deathUI.alpha = Mathf.Lerp(end, start, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        deathUI.alpha = start;
        deathUI.blocksRaycasts = false;
        deathUI.interactable = false;
        deathFadeoutUI.SetActive(false);
    }

    private bool DeathWasRepeat(string recentDeathType)
    {
        if (WaysToDie[recentDeathType] == 0)
        {
            print("Have not died this way before");
            WaysToDie[recentDeathType] += 1;
            return false;
        }
        print("Already died this way before");
        WaysToDie[recentDeathType] += 1;
        return true;
    }

    public IEnumerator HandleDeathCoroutine(string recentDeathType)
    {
        deathDieAudio.PlayOneShot(deathDieAudio.clip, 1.0f);
        yield return StartCoroutine(DeathFadeOutCoroutine(deathFadeoutCanvas, 0f, 1f, deathFadeOutTime));
        StartCoroutine(DeathFadeInCoroutine(deathFadeoutCanvas, 0f, 1f, deathFadeInTime));
        if (DeathWasRepeat(recentDeathType))
        {
            WaysToDie[recentDeathType] += 1;
            gameManager.GodCutscene();
            yield return StartCoroutine(godTextManager.ShowGodDialogue("Repeat", godTextManager.delay));
        }
        else
        {
            WaysToDie[recentDeathType] += 1;
            totalUniqueDeaths++;
            gameManager.GodCutscene();
            yield return StartCoroutine(godTextManager.ShowGodDialogue(recentDeathType, godTextManager.delay));
            if (totalUniqueDeaths == 5)
            {
                yield return StartCoroutine(godTextManager.ShowGodDialogue("Milestone_5", godTextManager.delay));
            }
        }
        yield return StartCoroutine(DeathFadeOutCoroutine(deathFadeoutCanvas, 0f, 1f, deathFadeOutTime));
        gameManager.GodCutscene();
        deathReviveAudio.PlayOneShot(deathReviveAudio.clip, 1.0f);
        StartCoroutine(DeathFadeInCoroutine(deathFadeoutCanvas, 0f, 1f, deathFadeInTime));
    }
}
