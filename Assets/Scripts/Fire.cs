using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("火花存活时间")]
    public float lifeTime;
    
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
