using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalterDialog : MonoBehaviour
{

    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    public float wordSpeed;
    private int index = 0;
    private bool canType = false;

    // Update is called once per frame
    void Update()
    {
        if(canType)
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
            canType = false;
        }
        if(dialogueText.text == dialogue[index])
        {
            index++;
            zeroText();
            GameManager.Instance.TransitionBack();
        }
        
    }

    private void zeroText()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        canType = false;
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void NextLine()
    {
        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    public void ActivePanel()
    {
        dialoguePanel.SetActive(true);
    }

    public void StartTyping()
    {
        canType = true;
    }
}
