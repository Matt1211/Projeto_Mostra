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
    public GameObject taskScoreboad;
    public GameObject taskScorebox;
    public GameObject npcTooltip;

    private Dictionary<string, string> npcDialogues = new Dictionary<string, string>()
    {
        {"Coelho" , "Olá amigo! Posso te ensinar a técnica do pulo! Mas apenas se você usar as letras para formar meu nome! Me chamo Coelho!"},
        {"Sapo", "Oi amigo! Eu sou o Sapo! Vi que você está ajudando a floresta! Você gosta de pular? Repita comigo: PULAR! Se você vencer esse desafio, vamos viajar juntos!" },
        {"Esquilo", "Ei amigo! Cuidado! Você verá que existem cogumelos que estão tentando acabar com a nossa graminha verde! Se você puder usar seu pulo nesses cogumelos, poderá deixar a floresta ainda mais VERDE! Repita: VERDE!" },
        {"Coelho2", "Amigo! O Sapo tinha uma mensagem para mim, certo? Deve estar com frio! Precisamos de um CASACO!" },
        {"Sapo2", "Chegamos na Nevasca! Diga ao meu amigo Coelho no final do percurso que sinto muito frio aqui! Repito: FRIIIOOO!" },
        {"Esquilo2", "Amigo me ajude! As raposas são velozes na neve! Escutou? NE-VE!" },
        {"Coelho3", "Eu acho essa paisagem linda! E sei que você acha o mesmo! Repete comigo!" },
        {"Esquilo3", "Vamos para o mar! Você entende o que digo? Se sim, repita e vamos comigo!" },
        {"Sapo3", "Oi amigo! Eu digo o que vejo: A ilha tem muitas pedras! ... Poderia me lembrar o que eu disse?" }
    };

    private Dictionary<string, NPC_Task> npcTasks = new Dictionary<string, NPC_Task>()
    {
        {"Coelho", new NPC_Task("Coelho") },
        {"Sapo", new NPC_Task("Pular") },
        {"Esquilo", new NPC_Task("Verde") },
        {"Coelho2", new NPC_Task("Casaco") },
        {"Sapo2", new NPC_Task("Frio") },
        {"Esquilo2", new NPC_Task("Neve") },
        {"Coelho3", new NPC_Task("Eu acho essa paisagem linda!") },
        {"Esquilo3", new NPC_Task("Vamos para o mar!") },
        {"Sapo3", new NPC_Task("A ilha tem muitas pedras") }
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

        if (Time.timeScale != 0)
        {
            if (playerIsClose && !npcTask.completed)
            {
                npcTooltip.SetActive(true);
            }
            else
            {
                npcTooltip.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Return) && playerIsClose
            && dialoguePanel.activeInHierarchy
            && !npcTask.completed)
            {
                ZeroText();
                TaskStart(npcTask);
            }

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
            TaskSuccess();
        }
        else
        {
            TaskFailed();
        }
    }

    private void TaskSuccess()
    {
        npcTask.completed = true;
        npcAudio.clip = successClip;
        npcAudio.Play();
        taskScoreboad.SetActive(true);
        taskScorebox.SetActive(true);
        TaskSuccessSfx.instance.PlayTaskSuccessSfx();
        StartCoroutine(HideTaskScoreBoxAfterDelay(1.8f));
        StartCoroutine(GameManagerScript.instance.ResumeGameAfterDelay(2));
        taskPanel.SetActive(false);
        StartCoroutine(GameManagerScript.instance.LoadNextLevel(5));
    }

    private void TaskFailed()
    {
        npcTask.completed = false;
        npcAudio.clip = failureClip;
        npcAudio.Play();
        taskResultText.text = "";
    }

    private IEnumerator HideTaskScoreBoxAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        taskScorebox.SetActive(false);
    }
}
