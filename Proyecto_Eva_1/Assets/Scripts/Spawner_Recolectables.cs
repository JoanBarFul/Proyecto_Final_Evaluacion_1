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
   
    void Start()
    {
        //Hace aparecer al pricipio de la partida 10 medallas aleatoriamente dentro del mapa.
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

        //Cada 5 segundos aparece un obstaculo nuevo
        InvokeRepeating("SpawnerObstacles", startSpawn, repeatSpawn);

        //Busca en el script de "Player_Controller".
        playerControllerScript = FindObjectOfType<Player_Controller>();
    }

    //Instancia una medalla en una posición aleatoria.
    public void SpawnerR()
    {
        randX = Random.Range(150, -151);
        randY = Random.Range(0, 181);
        randZ = Random.Range(-180, 300);
        randPos = new Vector3(randX, randY, randZ);
        Instantiate(medallaPrefab, randPos, medallaPrefab.transform.rotation);
    }

    //Instancia un obstaculo en una posición aleatoria.
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
