using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    private float thrust = 5f; // intensidade da força de propulsão
    [SerializeField]
    private float torque = 5f; // intensidade do torque
    [SerializeField]
    private GameObject bulletPrefab; // prefab da bullet
    [SerializeField]
    private Transform fireSpotTrans; // transform de referência (posição e orientação) para criação de uma nova bullet
    int ammunitioncount = 10;
    int life = 3; // qde de vida
    Rigidbody2D rb2d; // referencia da componente Rigidbody2D
    public GameObject player;
    public Text lifeText;
    public Text ammunitioncountText

    Camera cam; // Camera principal do jogo
    Vector2 rightTopLim; // limite superior direito da camera
    Vector2 leftBottomLim; // limite inforior esquerdo da camera

    // Executa uma unica vez no inicio (anterior a primeira atualização do quadro)
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        InitCamValues();
        lifeText.text = "Vidas" + life;
        ammunitioncountText.text = "Balas" + ammunitioncount;
    }

    // Executa uma unica vez em todos os quadros. Sincronizado com a atualização de quadros.
    void Update()
    {
        CheckCamLimits();
        player.SendMessage(ammunitioncount);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ammunitioncount > 0)
            {   // instanciar nova bala
                GameObject bulletGo = Instantiate(bulletPrefab, fireSpotTrans.position, fireSpotTrans.rotation);
                bulletGo.tag = gameObject.tag;
                ammunitioncount--;
                ammunitioncountText.text = "Balas" + ammunitioncount;
            }
        }
    }

    // Chamada sincrona com o motor de física
    // Intervalo de tempo fixo entre chamadas
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // -1 a 1 (esquerda (-1) , nada (0), direita (1))
        float vertical = Input.GetAxisRaw("Vertical"); // -1 a 1 (baixo (-1) , nada (0), cima (1))

        // Adição de uma força baseada na entrada do jogador. Direção "pra cima" local
        rb2d.AddRelativeForce(vertical * thrust * Vector2.up);
        // Adição de torque (rotação) baseada na entrada do jogador. 
        // Ajuste de sinal para que a relação seta clicada e rotação gerada estejam coerentes
        rb2d.AddTorque(-horizontal * torque);
    }
    
    //Respawn com velocidade zerada
    void Respawn()
    {
        life = 3;
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        GetComponent <SpritRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        
    }
    //Verifica se  a vida é igual a zero e chama o respawn com velocidade zerada
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            life--;
            lifeText.text = "Vidas" + life;
        }
        if (collision.CompareTag("Ammunition"))
        {
            Destroy(collision.gameObject);    
            while (ammunitioncount <= 10)
            {
                for (int i = 0; i <= 5; i++)
                    ammunitioncount++;
                    ammunitioncountText.text = "Balas" + ammunitioncount;
            }
        }

        if (life == 0) { 
            
            GetComponent<SpritRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Invoke("Respawn", 2f);
    }   
    }

    // Inicializo limites definidos pela visão da camera no universo.
    // Camera converte pontos de referência no dispositivo grafico de saida (monitor/aba game), que estão em coordenadas de screen (pixels).
    // em pontos "equivalentes" no universo do jogo.
    void InitCamValues()
    {
        rightTopLim = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        leftBottomLim = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
    }

    // Verifica a posição de ship em relação aos limites definidos pela visão da camera no universo
    // teletransporta para lado oposto
    void CheckCamLimits()
    {
        Vector3 pos = transform.position;

        if(transform.position.x > rightTopLim.x)
        {
            pos.x = leftBottomLim.x;
        }
        else if(transform.position.x < leftBottomLim.x)
        {
            pos.x = rightTopLim.x;
        }
        else if(transform.position.y > rightTopLim.y)
        {
            pos.y = leftBottomLim.y;
        }
        else if(transform.position.y < leftBottomLim.y)
        {
            pos.y = rightTopLim.y;
        }

        transform.position = pos;
    }

}
