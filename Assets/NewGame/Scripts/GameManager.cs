using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int stage;

    private float stageTime = 15f;

    private float playTime;

    private float enemyCooldown = 1.2f;

    private float spawnEnemy;

    public Parallax map;

    public Enemy enemy;
    
    public Camera cam;

    private bool tellingStory = false;

    private Animator anim;

    public Transform leftCollider;
    public Transform car;
    private AudioSource source;
    public AudioClip ruidoLlamada;
    public AudioSource motor;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelController.Instance.CreateEnemy();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemy += Time.deltaTime;
        playTime += Time.deltaTime;

        if(spawnEnemy > enemyCooldown && !tellingStory)
        {
            spawnEnemy = 0;
            LevelController.Instance.CreateEnemy();
        }

        if(playTime > stageTime && !tellingStory)
        {
            tellingStory = true;
            map.parallaxVelocity = new Vector2(0, .3f);
            if(stage == 5) SceneLoader.Instance.NextScene();
            source.PlayOneShot(ruidoLlamada);
            anim.SetBool("StartTransition", true);
        }

        if(Input.GetKeyDown(KeyCode.N)) SceneLoader.Instance.NextScene();

    }

    public void TransitionBack()
    {
        anim.SetBool("StartTransition", false);
        anim.SetBool("GoBack", true);
    }

    public void NextStage()
    {
        tellingStory = false;
        anim.SetBool("StartTransition", false);
        anim.SetBool("GoBack", false);
        playTime = 0;
        map.parallaxVelocity += new Vector2(0, 0.5f);
        enemy.speed += 0.1f;
        enemyCooldown -= 0.2f;
        motor.volume += 0.03f;
        motor.pitch += 0.25f;
        stage++;
    }

    public void cutRingtone()
    {
        source.Stop();
    }
}
