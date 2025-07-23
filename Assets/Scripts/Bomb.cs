using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("炸弹存活时间")]
    public float lifeTime;
    [Header("爆炸火花")]
    public GameObject explosionPrefab;
    
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnDestroy()
    {
        // 在炸弹的位置上生成火花
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
