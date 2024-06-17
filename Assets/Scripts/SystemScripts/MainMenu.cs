using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button muteButton;
    public Button tutorialButton;
    public Button quitButton;
    public Button continueTutorialButton;
    public Button closeTutorialButton;
    public GameObject tutorialPage;
    public GameObject tutorialLastPage;
    private bool isMuted = false;


    // Start is called before the first frame update
    void Start()
    {
        quitButton.onClick.AddListener(() => Application.Quit());
        muteButton.onClick.AddListener(() => MuteApplication());
        tutorialButton.onClick.AddListener(() => ShowTutorial());
        continueTutorialButton.onClick.AddListener(() => ShowTutorialLastPage());
        closeTutorialButton.onClick.AddListener(() => CloseTutorial());
        playButton.onClick.AddListener(() => PlayGame());
    }

    private void MuteApplication()
    {
        AudioListener.volume = isMuted ? 1 : 0;
        isMuted = !isMuted;
    }

    private void ShowTutorial()
    {
        tutorialPage.SetActive(true);
    }

    private void ShowTutorialLastPage()
    {
        tutorialPage.SetActive(false);
        tutorialLastPage.SetActive(true);
    }

    private void CloseTutorial()
    {
        tutorialLastPage.SetActive(false);
    }

    private void PlayGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
