using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walter_Open_World : MonoBehaviour
{
    public float speed = 20;

    private bool canBeHit = true;

    public int life = 5;

    private float recoveryTime;

    private float timeToBeHit = 2f;


    public float angle = 1.5f;

    private Rigidbody2D rb;
    private Vector3 carMovement;
    private int boostTime = 3;
    private bool imBoosted = false;
    private float boostCounter = 0f;
    private float boostSpeed = 30;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        recoveryTime += Time.deltaTime;

        if(Input.GetKey(KeyCode.D)) transform.eulerAngles -= new Vector3(0, 0, 1) * angle;
        if(Input.GetKey(KeyCode.A)) transform.eulerAngles += new Vector3(0, 0, 1) * angle;

        carMovement = new Vector3(0, speed, 0) * Time.deltaTime;

        if(recoveryTime > timeToBeHit && !canBeHit)
        {
            canBeHit = true;
            recoveryTime = 0;
        }

        if(imBoosted)
        {
            boostCounter += Time.deltaTime;

            speed = boostSpeed;

            if(boostTime < boostCounter)
            {
                imBoosted = false;
                boostCounter = 0f;
                speed = 20; 
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
            canBeHit = false;
        }
        if(other.gameObject.layer == 4) speed = 10;
    }

    public void ApplyBoost()
    {
        imBoosted = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 4) speed = 20;
    }

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.layer == 11) 
        {
            Destroy(collisionInfo.gameObject);
            life--;
        }
    }
}
