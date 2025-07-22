using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炸弹消失脚本
/// </summary>
public class Done_DestroyAfter : MonoBehaviour {


    //设置炸弹存在时间
    public float time = 3f;
	// Use this for initialization
	void Start () {
        //销毁炸弹
        Destroy(gameObject,time);
	}
	
	
}
