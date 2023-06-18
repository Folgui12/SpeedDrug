using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int stage = 3;

    private float stageTime = 20f;

    private float playTime;

    private float enemyCooldown = 4f;

    private float spawnEnemy;

    public Parallax map;

    public Enemy enemy;

    private float increaseSpeed = .1f;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        DeployEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemy += Time.deltaTime;
        playTime += Time.deltaTime;

        if(spawnEnemy > enemyCooldown)
        {
            spawnEnemy = 0;
            DeployEnemies();
        }

        if(playTime > stageTime)
        {
            playTime = 0;
            if(stage < 8) 
            {
                map.parallaxVelocity += new Vector2(0, 0.2f);
                enemy.speed += 0.2f;
                stage++;
            }
            else SceneLoader.Instance.NextScene();
        }

        if(Input.GetKeyDown(KeyCode.N)) SceneLoader.Instance.NextScene();

    }

    private void DeployEnemies()
    {
        for(int i = 0; i < stage; i++)
        {
            LevelController.Instance.CreateEnemy();
        }
    }
}
