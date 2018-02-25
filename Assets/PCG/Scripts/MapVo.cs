/** 
 *Copyright(C) by  http://github.com/igeeknerd 
 *All rights reserved. 
 *FileName:     F:/UnityProjs/PCGDemo/Assets/PCG/Scripts/MapVo.cs 
 *Author:       igeeknerd 
 *UnityVersion：2017.1.1f1 
 *Date:         2/23/2018  
*/
using UnityEngine;
using System.Linq;
/**
 * 地图数据
 */ 
public class MapVo{
    /**
     * 环境类型
     */ 
    public enum EnvType
    {
        Empty = -1,
        Grass = 15,
        Tree = 16,
        Hill = 17,
        Mountains = 18,
        Castle = 20
    }
    public TileVo[] tilevos;
    public int cols;
    public int rows;
    /**
     * 海岸环境
     */ 
    public TileVo[] edges
    {
        get
        {
            return tilevos.Where(t => t.autoTileId < (int)EnvType.Grass).ToArray();
        }
    }

    public TileVo[] landTiles
    {
        get
        {
            return tilevos.Where(t => t.autoTileId == (int)EnvType.Grass).ToArray();
        }

    }

    public TileVo castleTile
    {
        get
        {
            return tilevos.FirstOrDefault(t => t.autoTileId == (int)EnvType.Castle);
        }
    }
    /**
     * 新地图
     */ 
    public void NewMap(int p_width,int p_height)
    {
        cols = p_width;
        rows = p_height;
        tilevos = new TileVo[cols * rows];
        this.FillTileVo();
        this.FindNeighbours();
    }
    /**
     * 填充网格数据
     */ 
    private void FillTileVo()
    {
        int totalNum = cols * rows;
        for (int i=0;i < totalNum;i++)
        {
            TileVo vo = new TileVo();
            vo.id = i;
            //Debug.Log("填充数据为 " + vo.id);
            tilevos[i] = vo;
        }
    }
    /**
     * 寻找临近格子
     */ 
    private void FindNeighbours()
    {
        for (int i=0;i < rows;i++)
        {
            for (int j=0;j < cols;j++)
            {
                //得到当前的tile
                TileVo vo = tilevos[cols * i + j];
                //计算左边的
                if (j > 0)
                {
                    vo.AddNeighbours(TileType.Left, tilevos[cols * i + j - 1]);
                }
                //计算右边的
                if(j < cols - 1)
                {
                    vo.AddNeighbours(TileType.Right, tilevos[cols * i + j + 1]);
                }
                //计算上边的
                if (i > 0)
                {
                    vo.AddNeighbours(TileType.Top, tilevos[(i - 1) * rows + j]);
                }
                //计算下边的
                if (i < rows -1)
                {
                    vo.AddNeighbours(TileType.Bottom, tilevos[(i+1)*rows + j]);
                }
            }
        }
    }
    public void ChangeEdge(float erode,int p_loop,float p_tree,float p_hill,float p_mountain,float p_lake)
    {
        DecTiles(landTiles, p_lake, EnvType.Empty);
        for (int i = 0; i < p_loop; i++)
        {
            DecTiles(edges, erode, EnvType.Empty);
        }

        TileVo[] openTile = landTiles;
        Debug.Log("开始的长度是 " + openTile.Length);
        FisherShuffle(openTile);
        Debug.Log("最后的长度是 " + openTile.Length);
        TileVo castle = openTile[0];
        castle.autoTileId = (int)EnvType.Castle;

        DecTiles(landTiles, p_tree, EnvType.Tree);
        DecTiles(landTiles,p_hill,EnvType.Hill);
        DecTiles(landTiles, p_mountain, EnvType.Mountains);

    }
    /**
     * 装饰网格
     */ 
    public void DecTiles(TileVo[] p_vos,float p_percent,EnvType p_type)
    {
        int total = Mathf.FloorToInt(p_vos.Length * p_percent);
        FisherShuffle(p_vos);
        for (int i=0;i < total;i++){
            TileVo p_vo = p_vos[i];
            if(p_type == EnvType.Empty)
            {
                p_vo.ClearNeighbours();
            }
            p_vo.autoTileId = (int)p_type;
        }
    }

    /**
     * Fisher-Yates Shuffle
     */ 
    public void FisherShuffle(TileVo[] p_vos)
    {
        TileVo tmpvo = null;
        for (int i=0;i < p_vos.Length;i++)
        {
            tmpvo = p_vos[i];
            int index = Random.Range(i, p_vos.Length);
            Debug.Log("随机到的数值是" + index + " 长度是 " + p_vos.Length);
            p_vos[i] = p_vos[index];
            p_vos[index] = tmpvo;
        }
    }
}
