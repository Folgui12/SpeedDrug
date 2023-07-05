using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Walter_Open_World : MonoBehaviour
{
    public ParticleSystem smoke1;
    public ParticleSystem smoke2;
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

    public Transform spawnPoint;

    private float noDamageTime = 2f;

    private float timer;

    public int tries = 3;

    public Image bar;

    private float maxLife = 5;

    public bool onSafeArea = false;

    public Text textTries;
    public GameObject pu1;
    public GameObject pu2;
    public bool canUseBoost = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        smoke1.Stop();
        smoke2.Stop();
        textTries.text = tries.ToString();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    

    // Update is called once per frame
    void Update()
    {

        soundManager.Instance.Play("Motor");
        if(Input.GetKey(KeyCode.D)) transform.eulerAngles -= new Vector3(0, 0, 4) * angle;
        if(Input.GetKey(KeyCode.A)) transform.eulerAngles += new Vector3(0, 0, 4) * angle;

        if(imBoosted)
        {
            boostCounter += Time.deltaTime;

            speed = boostSpeed;

            if(boostTime < boostCounter)
            {
                imBoosted = false;
                boostCounter = 0f;
                speed = 17; 
            }
        }

        if(!canBeHit)
        {
            timer += Time.deltaTime;

            if(timer > noDamageTime) {
                timer = 0;
                canBeHit = true;
            }

        }

        if(canUseBoost && (pu1.activeInHierarchy || pu2.activeInHierarchy) && Input.GetKey(KeyCode.W) && !imBoosted) {
            ApplyBoost();
            soundManager.Instance.Play("Turbo");

            if(pu1.activeInHierarchy && !pu2.activeInHierarchy) pu1.SetActive(false);
            else if(pu2.activeInHierarchy) pu2.SetActive(false);
        }

        LifeBarManager();
    
    }

    public void ApplyBoost()
    {
        imBoosted = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.layer == 4) 
        {
            speed = 10;
            onWater = true;
            
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 4) 
        {
            soundManager.Instance.Play("Water");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 4) speed = 15;
    }

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.layer == 11 || collisionInfo.gameObject.layer == 12) 
        {
            Destroy(collisionInfo.gameObject);
            if(canBeHit) lifeController();
            canBeHit = false;
            anim.SetTrigger("GotHit");
        }
        
        if(collisionInfo.gameObject.layer == 8 && canBeHit) //&& canBeHit
        {
            lifeController();
            anim.SetTrigger("GotHit");
            var enemy = collisionInfo.gameObject.GetComponent<EnemyOpenWorld>();
            enemy.reachWalter();
            canBeHit = false;
        }

        if(collisionInfo.gameObject.layer == 13 && canBeHit) //&& canBeHit
        {
            lifeController();
            canBeHit = false;
            anim.SetTrigger("GotHit");
            var enemy = collisionInfo.gameObject.GetComponent<PoliceMovement>();
            enemy.reachWalter();
        }

        if(collisionInfo.gameObject.layer == 14) onSafeArea = true;
    }

    public void lifeController()
    {
        soundManager.Instance.Play("Crash");
        life--;
        if(life < 5 && life > 2)
        {
            smoke1.Play();
        }
        else if (life < 2)
        {
            smoke1.Stop();
            smoke2.Play();
        }
        if(life <= 0) 
        {
            life = 5;
            transform.position = spawnPoint.position;
            transform.eulerAngles = new Vector3(0, 0, 0);
            speed = 15;
            smoke2.Stop();
            tries--;
            textTries.text = tries.ToString();
            if(tries == 0) SceneLoader.Instance.Lose();
        }
    }

    private void LifeBarManager()
    {
        bar.fillAmount = life / maxLife;
    }
}
