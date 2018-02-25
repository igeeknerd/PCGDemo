/** 
 *Copyright(C) by  http://github.com/igeeknerd 
 *All rights reserved. 
 *FileName:     F:/UnityProjs/PCGDemo/Assets/PCG/Scripts/MoveCamera.cs 
 *Author:       igeeknerd 
 *UnityVersion：2017.1.1f1 
 *Date:         2/23/2018  
*/  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    private bool isMove = false;
    private Vector3 starPos = Vector2.zero;
    public float speed = 4.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1)) {
            isMove = true;
            starPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isMove = false;
        }

        if (isMove)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - starPos);
            Debug.Log("screen 差距为 " + pos + "\n" + "viewport差距为 " + (Input.mousePosition - starPos));
            Vector3 move = new Vector3(-pos.x * speed, -pos.y * speed, 0);
            transform.Translate(move,Space.Self);
        }
	}
}
