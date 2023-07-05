using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance; 

    public Animator transition;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }

    void Update()
    {
        
    }

    public void NextScene()
    {
        if(SceneManager.GetActiveScene().buildIndex < 3) StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Win()
    {
        SceneManager.LoadScene(3);
    }

    public void Lose()
    {
        SceneManager.LoadScene(4);
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("FadeIn");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(levelIndex);
    }

    public void BackToMenu()
    {
        StartCoroutine(LoadLevel(0));
    }
}
