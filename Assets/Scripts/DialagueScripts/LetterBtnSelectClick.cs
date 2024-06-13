using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LetterBtnSelectClick : MonoBehaviour
{
    public TMP_Text minigameText;
    public Button button;
    public string letter;
    private AudioSource letterAudio;
    // Start is called before the first frame update
    void Start()
    {
        letterAudio = GetComponent<AudioSource>();
        button.onClick.AddListener(() => OnButtonClick(letter));
    }

    void OnButtonClick(string clickedLetter)
    {
        letterAudio.Play();
        minigameText.text += letter;
    }
}
