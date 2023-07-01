using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int stage;

    private float stageTime = 15f;

    private float playTime;

    private float enemyCooldown = 1f;

    private float spawnEnemy;

    public Parallax map;

    public Enemy enemy;
    
    public Camera cam;

    private bool tellingStory = false;

    private Animator anim;

    public Transform leftCollider;
    public Transform car;

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
            anim.SetBool("StartTransition", true);
            map.parallaxVelocity = new Vector2(0, .3f);
            if(stage == 5) SceneLoader.Instance.NextScene();
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
        enemyCooldown -= 0.1f;
        stage++;
    }
}
