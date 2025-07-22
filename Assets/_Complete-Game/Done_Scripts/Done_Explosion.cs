using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 火花管理
/// </summary>
public class Done_Explosion : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D coll)
    {
        //如果碰到的物体不是静态属性，则被消除
        if (!coll.gameObject.isStatic) {
            Destroy(coll.gameObject);
        }
    }
}
