/** 
 *Copyright(C) by  http://github.com/igeeknerd 
 *All rights reserved. 
 *FileName:     F:/UnityProjs/PCGDemo/Assets/PCG/Scripts/MapGenObject.cs 
 *Author:       igeeknerd 
 *UnityVersion：2017.1.1f1 
 *Date:         2/23/2018  
*/  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenObject : MonoBehaviour {
    [Header("地图宽度和长度")]
    public int mapWidth = 1;
    public int mapHeight = 1;
    [Space]
    [Header("地图可视化")]
    public GameObject mapContainer;
    public GameObject tilePrefab;
    public Vector2 tileSize = new Vector2(16,16);
    [Space]
    [Header("地图材质")]
    public Texture2D tex2d;
    [Space]
    [Header("边界变化")]
    [Range(0, 0.9f)]
    public float erodePercent = 0.5f;
    public int coastLoopTime = 5;

    [Header("大树")]
    public float treePercent = 0.3f;
    [Header("山丘")]
    public float hillPercent = 0.2f;
    [Header("山脉")]
    public float mountainPercent = 0.1f;
    [Header("湖")]
    public float lakePercent = 0.05f;

    public MapVo map;
    public Sprite[] sps;

	// Use this for initialization
	void Start () {
        map = new MapVo();
        sps = Resources.LoadAll<Sprite>(tex2d.name);
	}
	
    public void MakeMap()
    {
        map.NewMap(mapWidth,mapHeight);
        map.ChangeEdge(erodePercent,this.coastLoopTime,treePercent,hillPercent,mountainPercent,lakePercent);
        Debug.Log("生成了地图 " + mapWidth +"x"+mapHeight);
        clearGrid();
        CreateGrid();
        CenterCam(map.castleTile.id);
        Debug.Log("地图可视化完成");
    }

    private void CreateGrid()
    {
        int total = map.cols * map.rows;
        for (int i=0;i <total;i++)
        {
            float posX = i % map.cols * tileSize.x;
            float posY = -(i / map.cols) * tileSize.y;
            var tile = Instantiate(tilePrefab);
            tile.transform.SetParent(mapContainer.transform);
            tile.name = "tile" + i;
            tile.transform.position = new Vector3(posX,posY,0);

            TileVo vo = map.tilevos[i];
            int spriteId = vo.autoTileId;
            if (spriteId > 0)
            {
                SpriteRenderer tempsp = tile.GetComponent<SpriteRenderer>();
                tempsp.sprite = sps[spriteId];
            }
            
        }
    }

    private void clearGrid()
    {
        Transform[] tileAll = mapContainer.GetComponentsInChildren<Transform>();
        for (int i = tileAll.Length -1;i > 0;i--)
        {
            Destroy(tileAll[i].gameObject);
        }
    }

    private void CenterCam(int p_index)
    {
        
        int width = map.cols;
        float newx = p_index % width * tileSize.x;
        float newy = -p_index / width * tileSize.y;
        Camera.main.transform.position = new Vector3(newx,newy, Camera.main.transform.position.z);
    }

}
