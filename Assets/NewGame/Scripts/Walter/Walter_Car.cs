using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walter_Car : MonoBehaviour
{

    private float speed = 10;
    private float movementX;

    private Vector3 carMovement;

    private bool canBeHit = true;

    public int life = 5;

    private float recoveryTime;

    private float timeToBeHit = 2f;

    private bool move = false; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        movementX = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void Update()
    {

        recoveryTime += Time.deltaTime;

        carMovement = new Vector3(speed*movementX, 0, 0) * Time.deltaTime;

        if(recoveryTime > timeToBeHit && !canBeHit)
        {
            canBeHit = true;
            recoveryTime = 0;
        }

        if(life <= 0) Debug.Log("Perdiste");

        transform.Translate(carMovement);

        if(transform.position.y < -10f && move) 
        {
            transform.position += new Vector3(0, .02f, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 8 && canBeHit)
        {
            life--;
            canBeHit = false;
        }
        if(other.gameObject.layer == 4) speed = 7;
        if(other.gameObject.tag == "Lose") SceneLoader.Instance.Lose();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 4) speed = 20;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.layer == 8) 
        {
            move = false;
        }
    }

    void OnCollisionExit2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.layer == 8) move = true;
    }
}
