using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmutionController : MonoBehaviour
{
	Rigidbody2D rb2d;
    Camera cam;
    Vector2 rightTopLim;
    Vector2 leftBottomLim;

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
}
