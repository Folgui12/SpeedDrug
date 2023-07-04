using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalterDialog : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private string[] dialogue2 = {"Declan:- Detente ya.",
"Walter:- Te dire que ocurrirá si no lo hago: a 2 km se encuentra un pueblo, en el cual entraré y continuaré alejándome de ti. Vos tenes dos opciones: detenerte y evitar desastres, o perseguirme y poner en riesgo la vida de muchos ciudadanos. Tu eliges Declan.",
"Declan:-  ¿Intentas convencerme de que si alguien muere será mi culpa? ¿Qué clase de manipulación es esa Heisenberg? Necesitarás mucho más que eso para hacer que me detenga.",
"Walter:- Mirenlo, el pequeño Declan no tiene miedo de mancharse las manos. Estoy ansioso por verlo.",
"Declan:- No me das miedo.",
"Walter:- Entonces sígueme que nuestro destino está adelante."};
    public float wordSpeed;
    private int index = 0;
    private bool canTypeFirst = false;
    private float timer = 0f;
    private float pauseTime = 3f;
    private bool startCountDown = false;
    private bool secondRound = false;
    private bool walterSpeaking = true;
    
    private AudioSource source;
    public AudioClip hablaWalter;
    public AudioClip hablaDeclan;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canTypeFirst)
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
            canTypeFirst = false;
        }
        if(startCountDown)
        {
            timer += Time.deltaTime;
        }
        if(timer > pauseTime)
        {
            zeroText();
            GameManager.Instance.TransitionBack();
            startCountDown = false;
            timer = 0;
        }
        
    }

    private void zeroText()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        source.volume = 0.2f;
    }

    IEnumerator Typing()
    {
        source.volume = 0.4f;
        if(walterSpeaking) source.PlayOneShot(hablaWalter);
        else source.PlayOneShot(hablaDeclan);
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        NextLine();
    }

    private void NextLine()
    {
        walterSpeaking = !walterSpeaking;
        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text += "\n \n";
            StartCoroutine(Typing());
        }
        else 
        {
            startCountDown = true;
            secondRound = true;
        }
    }

    public void ActivePanel()
    {
        dialoguePanel.SetActive(true);
    }

    public void StartTyping()
    {
        canTypeFirst = true;
        if(secondRound) dialogue = dialogue2;
        index = 0;
    }
}
