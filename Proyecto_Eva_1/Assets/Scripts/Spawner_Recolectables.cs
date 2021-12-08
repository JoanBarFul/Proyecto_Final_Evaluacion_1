using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Recolectables : MonoBehaviour
{
    private float randX;
    private float randY;
    private float randZ;
    public GameObject medallaPrefab;
    public GameObject bombaPrefab;
    private Vector3 randPos;
    public Player_Controller playerControllerScript;
    private float startSpawn = 2f;
    private float repeatSpawn = 5f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnerR();
        SpawnerR();
        SpawnerR();
        SpawnerR();
        SpawnerR();
        SpawnerR();
        SpawnerR();
        SpawnerR();
        SpawnerR();
        SpawnerR();

        InvokeRepeating("SpawnerObstacles", startSpawn, repeatSpawn);
        playerControllerScript = FindObjectOfType<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnerR()
    {
        randX = Random.Range(150, -151);
        randY = Random.Range(0, 181);
        randZ = Random.Range(-180, 300);
        randPos = new Vector3(randX, randY, randZ);
        Instantiate(medallaPrefab, randPos, medallaPrefab.transform.rotation);
    }

    public void SpawnerObstacles()
    {
        if (!playerControllerScript.gameOver)
        {
            randX = Random.Range(150, -151);
            randY = Random.Range(0, 181);
            randZ = Random.Range(-180, 300);
            randPos = new Vector3(randX, randY, randZ);
            Instantiate(bombaPrefab, randPos, bombaPrefab.transform.rotation);
        }
    }

}
