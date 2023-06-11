using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOpenWorld : MonoBehaviour
{

    private float positionX;
    private float positionY;

    public GameObject pj;

    public float speed;

    private Rigidbody2D rb;

    private Vector3 movement;

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

        Vector3 director = (pj.transform.position - transform.position).normalized;

        movement = director;
        
        float angle = Mathf.Atan2(director.y, director.x) * Mathf.Rad2Deg;

        rb.rotation = angle;
    }
}
