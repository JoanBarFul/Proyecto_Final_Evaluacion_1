using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Object : MonoBehaviour
{
    private float rotateSpeed = 600f;
    private float recolectableSpeed = 50f;
    public Player_Controller playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = FindObjectOfType<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            if (gameObject.CompareTag("Aspas"))
            { transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed); }

            if (gameObject.CompareTag("Cola"))
            { transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed); }

            if (gameObject.CompareTag("Recolectable"))
            { transform.Rotate(Vector3.up * Time.deltaTime * recolectableSpeed); }

            if (gameObject.CompareTag("Obstacle"))
            { transform.Rotate(Vector3.up * Time.deltaTime * recolectableSpeed); }
        }



    }
}
