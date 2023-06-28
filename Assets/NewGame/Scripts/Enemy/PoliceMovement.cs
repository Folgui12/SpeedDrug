using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMovement : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;

    public float speed;

    [SerializeField] private Transform characterPos;

    [SerializeField] private Walter_Open_World prueba;

    private int waypointIndex = 0;

    private bool movingTo = false;
    public walterDetected walterOnTrigger;
    private Rigidbody2D rb; 

    private float timerSlow = 0;
    private bool touchWalter = false;
    private float slowCountDown = 3f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, characterPos.position);

        if(touchWalter)
        {
            timerSlow += Time.deltaTime;
        }

        if(timerSlow > slowCountDown)
        {
            speed = 15; 
        }

        if(!movingTo)
        {
            Move();
        }
        else
        {
            moveToCharacter();
        }

        if(walterOnTrigger.enemyDetected)
        {
            movingTo = true;
        }
        else if(!walterOnTrigger.enemyDetected)
        {
            movingTo = false;
        }

        aimingWalter();
    }

    private void Move()
    {
        if(waypointIndex <= waypoints.Length - 1 && !movingTo)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, speed*Time.deltaTime);

            if(transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex++;
                if(waypointIndex >= waypoints.Length)
                {
                    waypointIndex = 0;
                }
            }
        }
    }

    private void moveToCharacter()
    {
        transform.position = Vector3.MoveTowards(transform.position, characterPos.position, speed*Time.deltaTime*1.1f);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.layer == 8)
        {
            /*MUERTE: Hacer un cambio de escena donde se muestre al enemigo muy 
            cerca de la cámara a modo de screamer, con gritos de fondo*/
        }
    }

    private void aimingWalter()
    {
        Vector3 director = (prueba.transform.position - transform.position).normalized;
        
        float angle = Mathf.Atan2(director.y, director.x) * Mathf.Rad2Deg;

        rb.rotation = angle - 90f;
    }

    public void reachWalter()
    {
        touchWalter = true;
        speed = 10;
    }
}
