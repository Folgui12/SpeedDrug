using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float roadEnemyLifeTime = 3f;

    private float timerRoad; 

    public bool canSpawn = true;

    public Sprite[] spritesToPick;

    private int rand;

    public float speed = .8f;

    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0, spritesToPick.Length);
        GetComponent<SpriteRenderer>().sprite = spritesToPick[rand];
    }

    // Update is called once per frame
    void Update()
    {
        timerRoad += Time.deltaTime;

        transform.position -= new Vector3(0, speed, 0) * Time.deltaTime * 1.5f;

        if(timerRoad > roadEnemyLifeTime) {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 8)
        {
            canSpawn = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 8)
        {
            canSpawn = true;
        }
    }
}
