using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Header("不可毁砖块")]
    public GameObject metalPrefab;
    [Header("可被毁砖块")]
    public GameObject wallPrefab;
    [Header("主角")]
    public GameObject playerPrefab;
    [Header("敌人")]
    public GameObject wormPrefab;

    [Header("列数")]
    public int column;
    [Header("行数")]
    public int row;
    [Header("可被毁砖块的个数")]
    public Count wallCount;
    [Header("怪物数量范围")]
    public Count wormCount;
    
    [HideInInspector]
    public List<Vector3> metalPositions = new();
    [HideInInspector]
    public List<Vector3> wallPositions = new();
    [HideInInspector]
    public List<Transform> wormList = new();
    [HideInInspector]
    public List<Vector3> gridPositions = new();

    private Transform _boardHolder;


    private void Start()
    {
        BoardSetup();
        InitList();
        LayoutWallAtRandom(wallCount.min, wallCount.max);
        GeneratePlayer();
        LayoutWormAtRandom(wormCount.min, wormCount.max);
    }

    // 生成不可毁砖块
    private void BoardSetup()
    {
        _boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < column + 1; x++)
        {
            for (int y = -1; y < row + 1; y++)
            {
                if ((x % 2 == 1 && y % 2 == 1) || x == -1 || x == column || y == -1 || y == row)
                {
                    GameObject obj = Instantiate(metalPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    obj.transform.parent = _boardHolder;
                    metalPositions.Add(obj.transform.position);
                }
            }
        }
    }

    // 收集可被毁砖块的生成位置
    private void InitList()
    {
        gridPositions.Clear();
        for (int x = 0; x < column; x++)
        {
            for (int y = 0; y < row; y++)
            {
                // 空出左上角用于生成角色
                if((x == 0 && y == row - 1) | (x == 0 && y == row - 2) | (x == 1 && y == row - 1)) 
                    continue;

                // 偶数行、偶数列可以生成可被毁墙体和怪物且角色是可以行走的
                if (x % 2 == 0 || y % 2 == 0)
                {
                    gridPositions.Add(new Vector3(x, y, 0));
                }
            }
        }
        
        print($"gridPositions.Count = {gridPositions.Count}");
    }

    // 生成随机位置
    private Vector3 RandomPosition()
    {
        if (gridPositions.Count == 0)
        {
            print("格子不够用了...");
            throw new ArgumentException("gridPositions.Count == 0");
        }

        int idx = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[idx];
        gridPositions.RemoveAt(idx);
        
        return randomPosition;
    }

    // 生成[min, max]个可被毁墙体
    private void LayoutWallAtRandom(int min, int max)
    {
        wallPositions.Clear();
        int count = Random.Range(min, max + 1);
        print($"LayoutWallAtRandom: {count}");

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(wallPrefab, RandomPosition(), Quaternion.identity);
            obj.transform.SetParent(_boardHolder);
            wallPositions.Add(obj.transform.position);
        }
    }
    
    // 生成玩家
    private void GeneratePlayer()
    {
        GameObject playerObj = Instantiate(playerPrefab, new Vector3(0, row - 1, 0), Quaternion.identity);
        playerObj.transform.SetParent(_boardHolder);
    }

    // 生成怪物
    private void LayoutWormAtRandom(int min, int max)
    {
        wormList.Clear();
        int count = Random.Range(min, max + 1);
        for (int i = 0; i < count; i++)
        {
            GameObject wormObj = Instantiate(wormPrefab, RandomPosition(), Quaternion.identity);
            wormObj.transform.SetParent(_boardHolder);
            wormList.Add(wormObj.transform);
        }
    }
}

[Serializable]
public class Count
{
    public int min;
    public int max;
}
