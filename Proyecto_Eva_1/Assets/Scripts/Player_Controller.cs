using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Controller : MonoBehaviour
{

    private float verticalInput;
    private float horizontalInput;
    public GameObject proyectilPrefab;
    public bool gameOver;
    public TextMeshProUGUI CounterText;
    public TextMeshProUGUI winText;
    public int medals = 0;
    private float speed = 10f;
    private float turnSpeed = 30f;
    private AudioSource playerAudio;
    public AudioClip winClip;
    public AudioClip gameOverClip;




    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 100, 0);
        medals = 0;
        winText.gameObject.SetActive(false);
        playerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.right * Time.deltaTime * turnSpeed * verticalInput);
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

            if (transform.position.x >= 200)
            { transform.position = new Vector3(200, transform.position.y, transform.position.z); }
            if (transform.position.x <= -200)
            { transform.position = new Vector3(-200, transform.position.y, transform.position.z); }

            if (transform.position.y >= 200)
            { transform.position = new Vector3(transform.position.x, 200, transform.position.z); }
            if (transform.position.y <= 0)
            { transform.position = new Vector3(transform.position.x, 0, transform.position.z); }

            //He ajustado el límite con el borde del mapa comprobando que no se salga de este en las equinas.
            if (transform.position.z >= 350)
            { transform.position = new Vector3(transform.position.x, transform.position.y, 350); }
            if (transform.position.z <= -100)
            { transform.position = new Vector3(transform.position.x, transform.position.y, -100); }

            if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl))
            { Instantiate(proyectilPrefab, transform.position, gameObject.transform.rotation);
               
            }


            CounterText.text = $"Medallas: {medals} / 10";
            if (medals == 10)
            {
                gameOver = true;
                winText.text = $"WIN";
                winText.gameObject.SetActive(true);
                playerAudio.PlayOneShot(winClip, 1);
            }

        }
    }

    public void OnCollisionEnter(Collision otherCollider)
    {
        if (!gameOver) 
        {
            if (otherCollider.gameObject.CompareTag("Recolectable"))
            {
               Destroy(otherCollider.gameObject);
                medals = medals + 1;
            }

            else if (otherCollider.gameObject.CompareTag("Obstacle"))
            {
                Destroy(otherCollider.gameObject);
                Destroy(gameObject);
                winText.text = $"GAME OVER";
                winText.gameObject.SetActive(true);
                gameOver = true;
                playerAudio.PlayOneShot(gameOverClip, 1);
            }
        }
        }
}
