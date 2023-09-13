using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOpenWorld : MonoBehaviour
{
    public GameObject pj;

    public float speed;

    private Rigidbody2D rb;

    private Vector3 movement;

    private float timerSlow = 0;
    private bool touchWalter = false;
    private float slowCountDown = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x, movement.y) * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(touchWalter)
        {
            timerSlow += Time.deltaTime;
        }

        if(timerSlow > slowCountDown)
        {
            speed = 15;
        }

        Vector3 director = (pj.transform.position - transform.position).normalized;

        movement = director;
        
        float angle = Mathf.Atan2(director.y, director.x) * Mathf.Rad2Deg;

        rb.rotation = angle - 90f;
    }

    public void reachWalter()
    {
        touchWalter = true;
        speed = 10;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 4) speed = 10;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 4) speed = 15;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.layer == 7) reachWalter();
    }
}
