using UnityEngine;


public class CloneScript : SampleScript
{
    [SerializeField] private GameObject prefab;
    [SerializeField, Min(1)] private int count = 5;
    [SerializeField, Min(0.1f)] private float step = 1.5f;
    [SerializeField] private Vector3 direction = Vector3.right;
    [SerializeField] private Transform parent;

    public override void Use()
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab íå íàçíà÷åí!", this);
            return;
        }

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = transform.position + direction.normalized * i * step;
            GameObject clone = Instantiate(prefab, spawnPosition, Quaternion.identity, parent);
        }
    }
}