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
    private AudioSource audioPlayer;
    public AudioClip medalClip;
    public AudioSource audioCamera;




    
    void Start()
    {   
        // Posición inicial.
        transform.position = new Vector3(0, 100, 0);

        //Medallas iniciales.
        medals = 0;

        //Oculto el texto de final de partida para mostrarlo solo si gano o pierdo.
        winText.gameObject.SetActive(false);

        //Componentes del audio.
        audioPlayer = GetComponent<AudioSource>();
        audioCamera = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (!gameOver)
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

            // Por el tamaño de mi "Player" he ajustado el límite con el borde del mapa, comprobando que no se salga de este en las equinas, porque 200 se me quedaba pequeño.
            if (transform.position.z >= 350)
            { transform.position = new Vector3(transform.position.x, transform.position.y, 350); }
            if (transform.position.z <= -100)
            { transform.position = new Vector3(transform.position.x, transform.position.y, -100); }

            //Tecla para instanciar el proyectil.
            if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl))
            { 
                //Instanciación del proyectil.
                Instantiate(proyectilPrefab, transform.position, gameObject.transform.rotation);
            }

            //Texto del marcador.
            CounterText.text = $"Medallas: {medals} / 10";

            //Cuando recoges todas las medallas (10) termina el juego, sale en la pantalla el texto de victoria, termina la música y suena el clip de audio de victoria.
            if (medals == 10)
            {
                winText.text = $"WIN";
                winText.gameObject.SetActive(true);
                audioCamera.Stop();
                //Insertar clip victoria.
                gameOver = true;
            }

        }
    }

    //Comprobar colisiones y sus comportamientos al respecto.
    public void OnCollisionEnter(Collision otherCollider)
    {
        if (!gameOver) 
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
                //Se destruyen tanto el obstaculo como el "Player".
                Destroy(otherCollider.gameObject);
                Destroy(gameObject);

                //Sale el texto de "GAME OVER".
                winText.text = $"GAME OVER";
                winText.gameObject.SetActive(true);
                
                //Se para la música.
                
                audioCamera.Stop();

                //Aparece una explosion y hace el ruido es esta.
                //Insertar Explosion

                //Se termina el juego.
                gameOver = true;
            }
        }
        }
}
