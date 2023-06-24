using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walter_Open_World : MonoBehaviour
{
    public float speed = 20;

    private bool canBeHit = true;

    public int life = 5;

    public float angle = 1.5f;

    private Rigidbody2D rb;
    private Vector3 carMovement;
    private int boostTime = 3;
    private bool imBoosted = false;
    private float boostCounter = 0f;
    private float boostSpeed = 30;

    public bool onWater = false;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D)) transform.eulerAngles -= new Vector3(0, 0, 5) * angle;
        if(Input.GetKey(KeyCode.A)) transform.eulerAngles += new Vector3(0, 0, 5) * angle;

        carMovement = new Vector3(0, speed, 0) * Time.deltaTime;

        if(imBoosted)
        {
            boostCounter += Time.deltaTime;

            speed = boostSpeed;

            if(boostTime < boostCounter)
            {
                imBoosted = false;
                boostCounter = 0f;
                speed = 18; 
            }
        }

        if(life <= 0) Debug.Log("Perdiste");

        transform.Translate(carMovement);

        Debug.Log(life);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 8 && canBeHit)
        {
            life--;
            anim.SetTrigger("GotHit");
        }
    }

    public void ApplyBoost()
    {
        imBoosted = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.layer == 4) 
        {
            speed = 5;
            onWater = true;
        }



    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 4) speed = 10;
    }

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.layer == 11 || collisionInfo.gameObject.layer == 12) 
        {
            Destroy(collisionInfo.gameObject);
            life--;
            anim.SetTrigger("GotHit");
        }
        if(collisionInfo.gameObject.layer == 8)
        {
            life--;
            anim.SetTrigger("GotHit");
        }
    }

    public void CanBeHit()
    {
        canBeHit = true;
    }

    public void CantBeHit()
    {
        canBeHit = false;
    }
}
