using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 火花生成
/// </summary>
public class Done_Bomb : MonoBehaviour {

    //火花承载变量
    public GameObject ExplosionRrefab;

    
    void OnDestroy()
    {
        //在炸弹的位置上生成火花
        Instantiate(ExplosionRrefab,transform.position,Quaternion.identity);
    }
}
