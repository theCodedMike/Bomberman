using UnityEngine;

public class Wall : MonoBehaviour
{
    private BoardManager _boardManager;

    private void Start()
    {
        _boardManager = FindFirstObjectByType<BoardManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 遇炸弹爆炸死亡
        if (other.CompareTag("Fire"))
        {
            _boardManager.wallPositions.Remove(transform.position);
            Destroy(gameObject);
        }
    }
}
