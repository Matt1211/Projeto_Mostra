using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmDialog : MonoBehaviour
{
    public Button confirmButton;
    public GameObject dialoguePanel;

    // Start is called before the first frame update
    void Start()
    {
        confirmButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            confirmButton.onClick.Invoke();
        }
    }

    void TaskOnClick()
    {
        if (dialoguePanel.activeInHierarchy)
        {
            dialoguePanel.SetActive(false);
        }
        else
        {
            dialoguePanel.SetActive(true);
        }
    }
}
