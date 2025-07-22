using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炸弹生成脚本
/// </summary>
public class Done_BombDrop : MonoBehaviour {

    //申明炸弹
    public GameObject BombPrefab;

    void Update()
    {
        //检测空格键，生成炸弹
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //获取炸弹人当前坐标，将坐标整数化，在此位置上生成炸弹
            Vector2 pos = transform.position;
            //Vector2 pos = new Vector2(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y));
            //Mathf.Round(pos.x);
            //Mathf.Round(pos.y);
            Instantiate(BombPrefab,pos, Quaternion.identity);
        }
    }
}
