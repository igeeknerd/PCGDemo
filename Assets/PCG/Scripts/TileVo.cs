/** 
 *Copyright(C) by  http://github.com/igeeknerd 
 *All rights reserved. 
 *FileName:     F:/UnityProjs/PCGDemo/Assets/PCG/Scripts/TileVo.cs 
 *Author:       igeeknerd 
 *UnityVersion：2017.1.1f1 
 *Date:         2/23/2018  
*/
using UnityEngine;
using System;
using System.Text;
/**
 * 4方向网格类型
 */
public enum TileType
{
    Bottom,
    Right,
    Left,
    Top
}
/**
 *  网格单位数据
 */
public class TileVo {
    public int id = 0;
    public TileVo[] Neighbours = new TileVo[4];
    public int autoTileId = 0;
    /**
     * 添加临近网格数据
     */ 
    public void AddNeighbours( TileType p_type,TileVo p_vo)
    {
        Neighbours[(int)p_type] = p_vo;
        //Debug.Log("p_id" + this.id + "添加了p_type  " + (int)p_type + "void " + p_vo.id);
        calculateTileId();
    }
    /**
     * 删除特定的网格
     */
    public void RemoveNeighbour(TileVo p_vo)
    {
        for(int i=0;i <Neighbours.Length;i++)
        {
            if(Neighbours[i] != null)
            {
                if(Neighbours[i].id == p_vo.id)
                {
                    Neighbours[i] = null;
                }
            }
        }

        calculateTileId();
    }

    /**
     * 删除临近的链接
     */ 
    public void ClearNeighbours()
    {
        for (int i=0;i < Neighbours.Length;i++)
        {
            if (Neighbours[i] != null)
            {
                Neighbours[i].RemoveNeighbour(this);
                //Debug.Log("tile id " + this.id + " 邻居id " + Neighbours[i].id);
                Neighbours[i] = null;
            }
        }
        calculateTileId();
    }
    /**
     * 计算网格id
     */  
    private void calculateTileId()
    {
        StringBuilder sb = new StringBuilder();
        foreach (TileVo vo in Neighbours)
        {
            sb.Append(vo == null ? "0" : "1");
        }
        autoTileId = Convert.ToInt32(sb.ToString(), 2);
        //Debug.Log("tileid " + id + " " + "autoid " + autoTileId + " str= " + sb.ToString());
    }
}
