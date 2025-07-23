using System;
using UnityEngine;

public class Player : MonoBehaviour
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
}
