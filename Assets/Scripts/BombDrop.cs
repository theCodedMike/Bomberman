using UnityEngine;

public class BombDrop : MonoBehaviour
{
    [Header("炸弹")]
    public GameObject bombPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 获取炸弹人的位置，并在此生成炸弹
            GameObject bombObj = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            bombObj.transform.SetParent(transform.parent);
        }
    }
}
