using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家移动脚本
/// </summary>
public class Done_Move : MonoBehaviour {


    //设置运动速度
    public float speed = 16f;
    /// <summary>
    /// 实时获取键盘输入
    /// </summary>
    private void FixedUpdate()
    {
        //getAxisRaw只能返回-1，0，1三个值
        //得到水平或垂直命令
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //访问炸弹人2D刚体，通过获取的值进行运动
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(h, v) * speed;

        //播放对应动画
        GetComponent<Animator>().SetInteger("x", (int)h);
        GetComponent<Animator>().SetInteger("y", (int)v);
    }

    
    /// <summary>
    /// 玩家与敌人相撞消失
    /// </summary>
    /// <param name="co"></param>
    private void OnCollisionEnter2D(Collision2D co)
    {
        
        if (co.gameObject.name == "Done_worm(Clone)")
        {
            Destroy(gameObject);

        }
    }
}
