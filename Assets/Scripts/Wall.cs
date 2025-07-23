using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 遇炸弹爆炸死亡
        if (other.CompareTag("Fire"))
        {
            Destroy(gameObject);
        }
    }
}
