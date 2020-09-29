using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] asteroids;
    GameObject[] ammution;

    float leftLimit = -6.5f;
    float bottomLimit = -5f;
    float topLimit = 5f;
    float rightLimit = 6.5f;
    


    void Start()
    {
        CreateNewAmmunition();
        CreateNewAsteroid();
    }
 
    void Update()
    {
        float countdown = 8.0f;

        countdown -= Time.deltaTime;
        if (countdown <= 0.0f)
        {
            CreateNewAmmunition();
            CreateNewAsteroid();
           
        }
    }
    

    void CreateNewAsteroid()
    {
        // Sorteio um indice (array de prefabs de asteroides) aleatorio
        int randomIndex = Random.Range(0, asteroids.Length);

        // posição (Vector3 (x, y, z)) aleatória
        Vector3 newPos = new Vector3(Random.Range(leftLimit, rightLimit), Random.Range(bottomLimit, topLimit), 0f);
        GameObject asteroid = Instantiate(asteroids[randomIndex], newPos, transform.rotation);

        // Asteroid instanciado (clone de prefab) coloco velocidade em direção aleatoria
        Vector2 direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        // direção aleatoria é normalizada, ou seja, fica com tamanho 1
        direction.Normalize();
        asteroid.GetComponent<Rigidbody2D>().velocity = direction * 2f;

    }
    
    void CreateNewAmmunition()
    {
    // posição (Vector3 (x, y, z)) aleatória
        Vector3 newPos = new Vector3(Random.Range(leftLimit, rightLimit), Random.Range(bottomLimit, topLimit), 0f);
        GameObject ammunition = Instantiate(ammunition[randomIndex], newPos, transform.rotation);
    }
    
}
