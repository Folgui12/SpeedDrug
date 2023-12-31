using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] int seg, min;
    [SerializeField] Text reloj;

    private float restante;

    public Walter_Open_World walter;
    public GameObject avisoWalter;

    void Awake()
    {
        restante = (min * 60) + seg;
    }

    // Update is called once per frame
    void Update()
    {
        if(restante > 0) restante -= Time.deltaTime; 
        if(restante < 1) avisoWalter.SetActive(true);
        if(restante < 1 && walter.tries > 0 && walter.onSafeArea)
        {
            SceneLoader.Instance.Win();
        }

        int tempMin = Mathf.FloorToInt(restante / 60);
        int tempSeg = Mathf.FloorToInt(restante % 60);

        reloj.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg);
    }
}
