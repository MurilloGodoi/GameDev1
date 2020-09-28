using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	// Componente RigidBody2D contida em bullet
    Rigidbody2D rb2d;

    void Start()
    {
        // Pegamos a referencia da compeonente RigidBody2D contida em bullet
        // Adicionamos uma força com alta intensidade (pontual). "Empurrão inicial"
        // Agendamos a destruição do objeto de jogo (assim como suas componentes) para 5 segundos após sua criação

        rb2d = GetComponent<Rigidbody2D>();

        rb2d.AddRelativeForce(Vector2.up * 800);

        Destroy(gameObject, 5.0f);
    }

    // Chamado quando um "outro collider" faz contato com o collider deste objeto de jogo. Porém um dos dois (ou os dois) 
    // deverão estar no modo Trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
