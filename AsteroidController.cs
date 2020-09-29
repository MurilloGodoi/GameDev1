using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    // 3 prefabs distintos
    [SerializeField]
    GameObject[] asteroids;

    bool division = false; // pode se dividir? não
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
        if(other.CompareTag("bulletGo"))
        {
            Destroy(gameObject);
        }
    }

    
}
