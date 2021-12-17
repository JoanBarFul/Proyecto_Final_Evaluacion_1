using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    private float speed = 28f;
    private float destroyTime = 5f;
    public Player_Controller playerControllerScript;


    void Start()
    {
        //Los proyectiles se destruyen al cabo de un tiempo para no tenerlos infinitamente en la escena.
        Destroy(gameObject, destroyTime);

        playerControllerScript = FindObjectOfType<Player_Controller>();
    }

   
    void Update()
    {
        //Una vez aparece el proyectil este avanza hacia delante.
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void OnCollisionEnter(Collision otherCollider)
    {
        //Cuando colisiona con un obstaculo destruye a este y a si mismo, también crea una explosión. 
        if (otherCollider.gameObject.CompareTag("Obstacle"))
        {
            Instantiate(playerControllerScript.explosionParticles, otherCollider.transform.position, transform.rotation);
            Destroy(otherCollider.gameObject);
            Destroy(gameObject);
            playerControllerScript.explosionParticles.Play();
            playerControllerScript.audioPlayer.PlayOneShot(playerControllerScript.explosionClip, 1);

        }
    }
}
