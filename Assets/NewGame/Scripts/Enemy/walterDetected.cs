using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walterDetected : MonoBehaviour
{
    public bool enemyDetected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 7)
        {
            enemyDetected = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 7)
        {
            enemyDetected = false;
        }
    }
}
