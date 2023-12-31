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

    public void CreateEnemy()
    {
        spawnPosition = GetSpawnPoint();

        test.transform.position = new Vector3(spawnPosition, 20, 0);

        Instantiate(test, test.transform.position, test.transform.rotation);
    }

    private int GetSpawnPoint()
    {
        int aux = 0;

        random = Random.Range(0, 7);

        switch(random)
        {
            case 0:
                aux = -5;
                break;
            case 1:
                aux = -3;
                break;
            case 2:
                aux = -1;
                break;
            case 3:
                aux = 1;
                break;
            case 4:
                aux = 3;
                break;
            case 5:
                aux = 5;
                break;
            case 6:
                aux = 0;
                break;
            default:
                break;
        }

        return aux;
    }
}
