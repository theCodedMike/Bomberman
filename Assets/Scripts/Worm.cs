using UnityEngine;
using Random = UnityEngine.Random;

public class Worm : MonoBehaviour
{
    private static readonly int X = Animator.StringToHash("x");
    private static readonly int Y = Animator.StringToHash("y");

    [Header("移动速度")]
    public float speed;

    private Rigidbody2D _rig;
    private Animator _animator;

    

    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        InvokeRepeating(nameof(ChangeDir), 0, 0.5f);
    }

    // 移动
    private void ChangeDir()
    {
        Vector2 dir = RandomDir();
        if (IsValidDir(dir))
        {
            _rig.linearVelocity = dir * speed;
            _animator.SetInteger(X, (int)dir.x);
            _animator.SetInteger(Y, (int)dir.y);
        }
    }

    // 检测运动方向
    // "position + dir, position", 从下一步位置射向目前位置，可以判断下一步位置是否为空
    private bool IsValidDir(Vector2 dir)
    {
        Vector2 position = transform.position;
        // 从怪物当前位置发射一条射线，如果碰到物体则怪物无法移动
        RaycastHit2D hit = Physics2D.Linecast(position + dir, position);
        
        return hit.collider.gameObject == gameObject;
    }
    
    // 生成随机运动方向
    Vector2 RandomDir()
    {
        int r = Random.Range(-1, 2);
        return Random.value < 0.5 ? new Vector2(r, 0) : new Vector2(0, r);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 遇炸弹爆炸死亡
        if (other.CompareTag("Fire"))
        {
            Destroy(gameObject);
        }
    }
}
