using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Object : MonoBehaviour
{
    private float rotateSpeed = 600f;
    private float recolectableSpeed = 50f;
    public Player_Controller playerControllerScript;
   
    void Start()
    {
        //Busca en el script de "Player_Controller".
        playerControllerScript = FindObjectOfType<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            //Las Aspas giran constantemente si no ha terminado la partida.
            if (gameObject.CompareTag("Aspas"))
            { transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed); }

            //Las hélices de la cola giran constantemente si no ha terminado la partida.
            if (gameObject.CompareTag("Cola"))
            { transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed); }

            //Los objetos de la partida giran contantemente si no ha terminado la partida.
            if (gameObject.CompareTag("Recolectable") || gameObject.CompareTag("Obstacle"))
            { transform.Rotate(Vector3.up * Time.deltaTime * recolectableSpeed); }
        }



    }
}
