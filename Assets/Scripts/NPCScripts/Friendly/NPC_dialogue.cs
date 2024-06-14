using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Task
{
    public string validWord;
    public bool completed;

    public NPC_Task(string validWord)
    {
        this.validWord = validWord;
        this.completed = false;
    }
}

public class NPC_dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject taskPanel;
    public Button returnButton;
    public TMP_Text dialogueText;
    public TMP_Text taskResultText;
    private AudioSource npcAudio;
    private bool playerIsClose;
    public string npcName;
    public NPC_Task npcTask;
    public AudioClip npcTaskClip;
    public AudioClip successClip;
    public AudioClip failureClip;

    private Dictionary<string, string> npcDialogues = new Dictionary<string, string>()
    {
        {"Coelho" , "Olá amigo! Posso te ensinar a técnica do pulo! Mas apenas se você usar as letras para formar meu nome! Me chamo Coelho!"},
    };

    private Dictionary<string, NPC_Task> npcTasks = new Dictionary<string, NPC_Task>()
    {
        {"Coelho", new NPC_Task("Coelho") },
    };

    private void Start()
    {
        returnButton.onClick.AddListener(() => ValidateTask(taskResultText.text));
        npcAudio = GetComponent<AudioSource>();
        npcAudio.clip = npcTaskClip;
        npcTask = npcTasks[npcName];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (!npcTask.completed)
            {
                if (dialoguePanel.activeInHierarchy)
                {
                    ZeroText();
                }
                else
                {
                    PlayDialogue();
                    dialoguePanel.SetActive(true);
                    npcAudio.Play();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && playerIsClose
            && dialoguePanel.activeInHierarchy
            && !npcTask.completed)
        {
            ZeroText();
            TaskStart(npcTask);
        }
    }

    public void ZeroText()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        npcAudio.Stop();
    }

    public void PlayDialogue()
    {
        dialogueText.text = npcDialogues[npcName];
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

    private void TaskStart(NPC_Task task)
    {
        GameManagerScript.instance.PauseGame();
        taskPanel.SetActive(true);
    }

    private void ValidateTask(string resultText)
    {
        if (resultText.Trim().ToLower() == npcTask.validWord.ToLower())
        {
            npcTask.completed = true;
            npcAudio.clip = successClip;
            npcAudio.Play();
            StartCoroutine(GameManagerScript.instance.ResumeGameAfterDelay(2));
            taskPanel.SetActive(false);
        }
        else
        {
            npcTask.completed = false;
            npcAudio.clip = failureClip;
            npcAudio.Play();
            taskResultText.text = "";
        }
    }
}
