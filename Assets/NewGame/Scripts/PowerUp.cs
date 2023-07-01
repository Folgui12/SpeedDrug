using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private bool isBoost = false;
    [SerializeField] private bool isHeal = false;
    [SerializeField] private bool is4x4 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 7)
        {
            if(is4x4) 
            {

            }
            else if(isHeal)
            {
                Walter_Open_World pj = other.gameObject.GetComponent<Walter_Open_World>();

                if(pj.life < 5) pj.life++;
            }
            else if(isBoost)
            {
                Walter_Open_World pj = other.gameObject.GetComponent<Walter_Open_World>();

                pj.ApplyBoost();

                Destroy(this.gameObject);
            }
        }
    }
}
