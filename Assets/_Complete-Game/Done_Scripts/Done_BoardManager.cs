using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地图管理器，负责墙体的生成和销毁，道具的放置等
/// </summary>
public class Done_BoardManager : MonoBehaviour
{

    /// <summary>
    /// 用于管理可炸毁墙体数量
    /// </summary>
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    //声明各种地图内的预制体                               
    public GameObject wallTile;
    public GameObject metalTile;
    //敌人预制体
    public GameObject wormPrefab;
    //public GameObject doorTile;
    //public GameObject badgeTile;

    // 数量参数，地图的行列数以及墙体的数量范围，敌人的数量
    public int columns { get; private set; }
    public int rows { get; private set; }
    public Count wallCount = new Count(30, 40);
    public Count wormCount = new Count(3, 5);

    // 管理用参数
    public List<Done_Wall> wallList = new List<Done_Wall>();              // Wall类集合
    public List<Done_Metal> metalList = new List<Done_Metal>();           // Metal类集合
    private List<Vector3> gridPositions = new List<Vector3>();  // 可用的格子的集合
    //public Door door;                                           // 门
    //public Badge badge;                                         // 徽章

    private Transform boardHolder;                              // 墙体的父Transform

    void InitialiseList()
    {
        gridPositions.Clear();
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                // 为主角留一个出生位置
                // □□■■■■
                // □■■■■■
                // ■■■■■■
                if ((x == 0 & y == rows - 1) | (x == 0 & y == rows - 2) | (x == 1 & y == rows - 1))
                {
                    continue;
                }
                // 找出所有的可用的格子存入gridPositions
                //在炸弹人游戏中，偶数行和偶数列的格子是可以用于可炸毁墙体的生成和怪物以及人的行走的。其与墙体均不可走。
                if (x % 2 == 0 || y % 2 == 0)
                {
                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }
    }

    /// <summary>
    /// 给游戏创建Metal外墙（边界）和内墙
    /// 在炸弹人游戏中，除整个地图被外墙包围以外，地图中x,y均为奇数的格子也会生成不可炸毁的墙体
    /// </summary>
    void BoardSetup()
    {
        columns = 18;
        rows = 7;
        metalList.Clear();
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = null;
                if ((x % 2 == 1 & y % 2 == 1) | x == -1 | x == columns | y == -1 | y == rows)
                {
                    toInstantiate = metalTile;
                }
                if (toInstantiate)
                {
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                    instance.transform.SetParent(boardHolder);
                    metalList.Add(instance.GetComponent<Done_Metal>());
                }
            }
        }
    }

    /// <summary>
    /// 从我们的gridPositions集合中返回一个随机位置
    /// </summary>
    /// <returns></returns>
    public Vector3 RandomPosition()
    {
        if (gridPositions.Count == 0)
        {
            Debug.Log("可用的格子不够了");
        }
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }


    /// <summary>
    ///  创建minimum到maximum数之间个数的可炸毁墙体wall
    ///  wall的生成范围在GridPositions列表中
    /// </summary>
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    void LayoutWallAtRandom(int minimum, int maximum)
    {

       int objectCount = Random.Range(minimum, maximum + 1);
        Debug.Log(objectCount);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();

            GameObject obj = Instantiate(wallTile, randomPosition, Quaternion.identity);

            obj.transform.SetParent(boardHolder);

            wallList.Add(obj.GetComponent<Done_Wall>());
        }
    }

    /// <summary>
    ///  创建minimum到maximum数之间个数的敌人
    ///  敌人的生成范围在GridPositions列表中
    /// </summary>
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    void LayoutWormAtRandom(int minimum, int maximum)
    {

        wallList.Clear();

        int objectCount = Random.Range(minimum, maximum + 1);
        Debug.Log(objectCount);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();

            GameObject obj = Instantiate(wormPrefab, randomPosition, Quaternion.identity);
            
        }
    }

    // Use this for initialization
    /// <summary>
    /// 建立场景
    /// </summary>
    void Start()
    {
        BoardSetup();

        InitialiseList();

        LayoutWallAtRandom(wallCount.minimum, wallCount.maximum);
        
        //敌人生成
        LayoutWormAtRandom(wormCount.minimum, wormCount.maximum);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
