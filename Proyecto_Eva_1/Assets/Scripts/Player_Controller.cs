using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Player_Controller : MonoBehaviour
{

    private float verticalInput;
    private float horizontalInput;
    public GameObject proyectilPrefab;
    public ParticleSystem explosionParticles;
    public ParticleSystem deathParticles;
    public bool gameOver;
    public bool win;
    public TextMeshProUGUI CounterText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI gameOverText;
    public int medals = 0;
    private float speed = 13f;
    private float turnSpeed = 30f;
    public AudioSource audioPlayer;
    public AudioClip medalClip;
    public AudioClip explosionClip;
    public AudioSource audioCamera;
    private Vector3 offsetscope = new Vector3(0.01f, -0.1f, 0.3f);




    void Start()
    {
        // Posición inicial.
        transform.position = new Vector3(0, 100, 0);

        //Medallas iniciales.
        medals = 0;

        //Oculto el texto de final de partida para mostrarlo solo si gano o pierdo.
        winText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);

        //Componentes del audio.
        audioPlayer = GetComponent<AudioSource>();
        audioCamera = GameObject.Find("Main Camera").GetComponent<AudioSource>();


    }


    void Update()
    {
        if (!gameOver && !win)
        {
            //Movimiento constante hacia delante.
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            //Asignación de los controles.
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            //Rotación del "Player" mediante los controles (flechas o A,D,W,S).
            transform.Rotate(Vector3.right * Time.deltaTime * turnSpeed * verticalInput);
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

            //Límites del mapa por donde se podrá mover el "Player".
            if (transform.position.x >= 200)
            { transform.position = new Vector3(200, transform.position.y, transform.position.z); }
            if (transform.position.x <= -200)
            { transform.position = new Vector3(-200, transform.position.y, transform.position.z); }

            if (transform.position.y >= 200)
            { transform.position = new Vector3(transform.position.x, 200, transform.position.z); }
            if (transform.position.y <= 0)
            { transform.position = new Vector3(transform.position.x, 0, transform.position.z); }

            if (transform.position.z >= 350)
            { transform.position = new Vector3(transform.position.x, transform.position.y, 350); }
            if (transform.position.z <= -75)
            { transform.position = new Vector3(transform.position.x, transform.position.y, -100); }

            //Tecla para instanciar el proyectil.
            if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl))
            {
                //Instanciación del proyectil.
                Instantiate(proyectilPrefab, gameObject.transform.position + offsetscope, gameObject.transform.rotation);
            }

            //Texto del marcador.
            CounterText.text = $"Medallas: {medals} / 10";

            //Cuando recoges todas las medallas (10) termina el juego, sale en la pantalla el texto de victoria, termina la música y suena el clip de audio de victoria.
            if (medals == 10)
            {
                winText.gameObject.SetActive(true);
                audioCamera.Stop();
                win = true;
            }

            //Rotación eje Z.
            Estabilizar(KeyCode.Q, Vector3.forward);
            Estabilizar(KeyCode.E, Vector3.back);
        }
    }

    //Comprobar colisiones y sus comportamientos al respecto.
    public void OnCollisionEnter(Collision otherCollider)
    {
        if (!gameOver && !win) 
        {
            //Si coges una medalla.
            if (otherCollider.gameObject.CompareTag("Recolectable"))
            {
                //Se destuye la medalla.
                Destroy(otherCollider.gameObject);

                //Suma 1 en el marcador de medallas recogidas.
                medals = medals + 1;

                //Se reproduce un sonido
                audioPlayer.PlayOneShot(medalClip, 1);
            }

            //Si colisionas contra un obstaculo.
            else if (otherCollider.gameObject.CompareTag("Obstacle"))
            {
                //Explosion.
                Instantiate(deathParticles, transform.position, transform.rotation);
                deathParticles.Play();

                //Se destruyen tanto el obstaculo como el "Player".
                Destroy(otherCollider.gameObject);
                Destroy(gameObject);
                
                //Se para la música.
                audioCamera.Stop();

                //Texto gameOver.
                gameOverText.gameObject.SetActive(true);

                //Se termina el juego.
                gameOver = true;
            }
        }
        }

    //Giro sobre el eje Z para estabilizar el player.
    public void Estabilizar(KeyCode key, Vector3 lado)
    {
        if (Input.GetKey(key))
        {transform.Rotate( lado * Time.deltaTime * turnSpeed);}
    }
}
