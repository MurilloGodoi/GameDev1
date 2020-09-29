using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField]
	private GameObject bulletPrefab;
	Rigidbody2D rb2d;
	Vector2 direction;
	float speed;
	float shootingDelay;
	float lastTimeshot = 0f;
	float bulletSpeed;
	public GameObject;
	public SpriteRenderer spriteRenderer;
	public Collider2D collider;
	public GameObject bullet;
	public Transform player;
	public bool disabled;
	public float timeBeforeSpawning;
	public float levelStartTime;
	public Transform startPosition;
	Camera cam;

	void Start()
    {
		rb2d = GetComponent<Rigidbody2D>();
		cam = Camera.main;
		InitCamValues();
		player = GameObject.FindWithTag ("Player").transform;
		levelStartTime = Time.time;
		timeBeforeSpawning = Random.Range(10f, 10f);
		Invoke("Enable", timeBeforeSpawning);
		Disable();
	 }
	
	void Update()
    {
        if (disabled)
        {
			if(Time.time > levelStartTime + timeBeforeSpawning)
            {
				Enable();
            }

        }

		if(Time.time > lastTimeshot + shootingDelay)
        {
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

			GameObject newBullet = Instantiate(bulletPrefab, transform.position, q);
			newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, bulletSpeed));
			lastTimeshot = Time.time;	

        }

    }

	void FixedUpdate()
    {
		if (disabled)
		{
			return;
		}	

		direction = (player.position - transform.position).normalized;
		rb2d.MovePosition(rb2d.position + direction * speed * Time.fixedDeltaTime);
    }

	void InitCamValues()
	{
		rightTopLim = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		leftBottomLim = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
	}


	void Enable()
    {
		transform.position = startPosition.position;
		collider.enabled = true;
		spriteRenderer.enabled = true;
		disabled = false;
	}

	void Disable()
    {
		
		collider.enabled = false;
		spriteRenderer.enabled = false;
		disabled = true;

	}

	void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("bulletGo"))
        {
			Destroy(gameObject, 2f);
			Disable();	
			
		}

	}
}
