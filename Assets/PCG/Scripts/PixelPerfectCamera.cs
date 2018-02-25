/** 
 *Copyright(C) by  http://github.com/igeeknerd 
 *All rights reserved. 
 *FileName:     F:/UnityProjs/PCGDemo/Assets/PCG/Scripts/PixelPerfectCamera.cs 
 *Author:       igeeknerd 
 *UnityVersion：2017.1.1f1 
 *Date:         2/23/2018  
*/  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour {

    public static float PixelToUnits = 1.0f;
    public static float Scale = 1.0f;
    /**
     * 160x144是GameBoy的屏幕分辨率，这个scale可以得到GameBoy的游戏体验
     */
    public Vector2 NativeRes = new Vector2(160,144);
	// Use this for initialization
	void Start () {
        Camera cam = this.GetComponent<Camera>();
        if (cam.orthographic)
        {
            float dir = Screen.height;
            float res = NativeRes.y;
            Scale = dir / res;
            PixelToUnits *= Scale;
            cam.orthographicSize = (dir / 2.0f) / PixelToUnits;
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
