using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject metalPrefab;
    public int column { get; private set; }
    public int row { get; private set; }
    [HideInInspector]
    public List<Metal> metalList = new();

    private Transform _boardHolder;


    private void Start()
    {
        BoardSetup();
    }

    private void BoardSetup()
    {
        column = 19;
        row = 7;
        _boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < column + 1; x++)
        {
            for (int y = -1; y < row + 1; y++)
            {
                if ((x % 2 == 1 && y % 2 == 1) || x == -1 || x == column || y == -1 || y == row)
                {
                    GameObject obj = Instantiate(metalPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    obj.transform.parent = _boardHolder;
                    metalList.Add(obj.GetComponent<Metal>());
                }
            }
        }
    }
}
