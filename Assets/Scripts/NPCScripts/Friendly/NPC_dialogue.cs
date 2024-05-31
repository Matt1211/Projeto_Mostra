using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC_dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public string dialogue;
    private AudioSource dialogueAudio;
    public bool playerIsClose;

    private string sunnyBunnyDialog1 = "Olá amigo! Posso te ensinar a técnica do pulo! Mas apenas se você usar as letras para formar meu nome! Me chamo Coelho!";

    private void Start()
    {
        dialogueAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                ZeroText();
            }
            else
            {
                PlayDialogue();
                dialoguePanel.SetActive(true);
                dialogueAudio.Play();
            }
        }
    }

    public void ZeroText()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        dialogueAudio.Stop();
    }

    public void PlayDialogue()
    {
        dialogueText.text = sunnyBunnyDialog1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            ZeroText();
        }
    }
}
