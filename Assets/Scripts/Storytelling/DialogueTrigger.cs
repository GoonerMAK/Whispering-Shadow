using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Button ButtonDisappear;
    public Button ButtonAppear;
    private void Start()
    {
        // Hide the button at the start
        ButtonAppear.gameObject.SetActive(false);
    }

    public void TriggerDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }

    public void ContinueButtonAppear()
    {
        ButtonAppear.gameObject.SetActive(true);
    }

    public void StartButtonDisappear()
    {
        ButtonDisappear.gameObject.SetActive(false);
    }
}
