using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    // 3 prefabs distintos
    [SerializeField]
    GameObject[] asteroids;

    bool division = true; // pode se dividir? não
    Rigidbody2D rb2d;
    Camera cam;
    Vector2 rightTopLim; 
    Vector2 leftBottomLim;
    public float maxThrust;
    public float maxTorque;


    void Start()
    { 
        rb2d = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        InitCamValues();
      
    }

    void InitCamValues()
    {
        rightTopLim = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        leftBottomLim = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
    }

    void Update()
    {
        CheckCamLimits();
    }

    void CheckCamLimits()
    {
        Vector3 pos = transform.position;

        if (transform.position.x > rightTopLim.x)
        {
            pos.x = leftBottomLim.x;
        }
        else if (transform.position.x < leftBottomLim.x)
        {
            pos.x = rightTopLim.x;
        }
        else if (transform.position.y > rightTopLim.y)
        {
            pos.y = leftBottomLim.y;
        }
        else if (transform.position.y < leftBottomLim.y)
        {
            pos.y = rightTopLim.y;
        }

        transform.position = pos;
    }

    // Chamado quando um "outro collider" faz contato com o collider deste objeto de jogo. Porém um dos dois (ou os dois) 
    // deverão estar no modo Trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // flitragem por TAG
        if(other.CompareTag("Player"))
        {
            // Se pode dividir => divido em 3 astroides 70% do tamanho original
            // a direção de cada velocidade dos asteróides: direção do movimento da bullet, direção perpendicular a direita do movimento da bullet
            // e direção perpendicular a esquerda do movimento da bullet
            // Destruo o asteroide
            //
            // Caso não pode dividir
            // Apenas Destruo o asteroide

            if (division)
            {
                Vector3[] directions = new Vector3[3];
                directions[0] = other.transform.up;
                directions[1] = other.transform.right;
                directions[2] = -other.transform.right;

                for (int i = 0; i < 3; i++)
                {
                    // solução para ship não colidir com asteroid original e com seus fragmentos logo em seguida
                    // posição inicial dos asteroides é oriunda do vetor posição do asteroide original + directions
                    // outra maneira simplesmente desabilitar division (division = false)

                    GameObject asteroidClone = Instantiate(asteroids[i], transform.position + directions[i], transform.rotation);
                    asteroidClone.GetComponent<Rigidbody2D>().velocity = directions[i] * 1f;
                    asteroidClone.transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
                    asteroidClone.GetComponent<AsteroidController>().SetDivision(false);
                }
            }
            
            Destroy(gameObject);
        }
    }

    public void SetDivision(bool value)
    {
        division = value;
    }
}
