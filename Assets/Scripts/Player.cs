using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly int X = Animator.StringToHash("x");
    private static readonly int Y = Animator.StringToHash("y");
    
    public static int Life; // 生命值
    
    [Header("移动速度")]
    public float speed;

    private Rigidbody2D _rig;
    private Animator _animator;
    private BoardManager _boardManager;
    

    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boardManager = FindFirstObjectByType<BoardManager>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        Move(h, v);
        
        PlayAnimation(h, v);
    }

    private void Move(float h, float v)
    {
        _rig.linearVelocity = new Vector2(h, v) * speed;
    }

    private void PlayAnimation(float h, float v)
    {
        _animator.SetInteger(X, (int)h);
        _animator.SetInteger(Y, (int)v);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 遇炸弹爆炸死亡
        if (other.CompareTag("Fire"))
            PlayerDead();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 遇怪物死亡
        if (other.collider.CompareTag("Worm"))
            PlayerDead();
    }

    private void PlayerDead()
    {
        Life--;
        _boardManager.UpdateLife(Life);
        if(Life == 0)
            Destroy(gameObject);
    }
}
