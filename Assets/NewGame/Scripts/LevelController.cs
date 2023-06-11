using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public static LevelController Instance;
    public Enemy test;

    private float random;

    private int spawnPosition;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateEnemy()
    {
        spawnPosition = GetSpawnPoint();

        test.transform.position = new Vector3(spawnPosition, 11, 0);

        Instantiate(test, test.transform.position, test.transform.rotation);

        while(!test.canSpawn)
        {
            spawnPosition = GetSpawnPoint();

            test.transform.position = new Vector3(spawnPosition, 11, 0);
        } 
    }

    private int GetSpawnPoint()
    {
        int aux = 0;

        random = Random.Range(0, 8);

        switch(random)
        {
            case 0:
                aux = -8;
                break;
            case 1:
                aux = -6;
                break;
            case 2:
                aux = -4;
                break;
            case 3:
                aux = -2;
                break;
            case 4:
                aux = 2;
                break;
            case 5:
                aux = 4;
                break;
            case 6:
                aux = 6;
                break;
            case 7:
                aux = 8;
                break;
            default:
                break;
        }

        return aux;
    }
}
