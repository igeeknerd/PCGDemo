/** 
 *Copyright(C) by  http://github.com/igeeknerd 
 *All rights reserved. 
 *FileName:     F:/UnityProjs/PCGDemo/Assets/PCG/Editor/MapGenEditor.cs 
 *Author:       igeeknerd 
 *UnityVersion：2017.1.1f1 
 *Date:         2/23/2018  
*/  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MapGenObject))]
public class MapGenEditor:Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var script = (MapGenObject)target;
        if (GUILayout.Button("生成地图"))
        {
            if(Application.isPlaying)
            script.MakeMap();
        }
    }

}
