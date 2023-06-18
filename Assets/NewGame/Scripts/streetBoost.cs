using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class streetBoost : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        
    }*/

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 7)
        {
            Walter_Open_World pj = other.gameObject.GetComponent<Walter_Open_World>();

            pj.speed = 10;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.layer == 7)
        {
            Walter_Open_World pj = other.gameObject.GetComponent<Walter_Open_World>();

            pj.speed = 13;
        }
    }
}
