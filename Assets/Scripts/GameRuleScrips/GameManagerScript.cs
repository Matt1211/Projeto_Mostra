using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    public GameObject stageNameBar;
    public Button pauseButton;
    public GameObject taskLevel1;
    public GameObject taskLevel2;
    public GameObject taskLevel3;
    private bool paused;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        InitialSetup();
    }

    private void InitialSetup()
    {
        pauseButton.onClick.AddListener(() => PauseUnpause(paused));
        ShowStageName();
        StartCoroutine(HideStageNameBarAfterDelay(5));
    }

    public void PauseUnpause(bool paused)
    {
        if (paused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
    }

    public void ShowStageName()
    {
        stageNameBar.SetActive(true);
    }

    public void HideStageName()
    {
        stageNameBar?.SetActive(false);
    }

    public IEnumerator ResumeGameAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        ResumeGame();
    }

    public void LoadNextLevel()
    {
        if (taskLevel1.activeInHierarchy &&
            taskLevel2.activeInHierarchy &&
            taskLevel3.activeInHierarchy)
        {
            //advance to next level
        }
    }

    public IEnumerator HideStageNameBarAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        HideStageName();
    }
}
