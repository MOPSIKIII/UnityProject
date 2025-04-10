using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChildrenScript : SampleScript
{
    public Transform target;
    public float shrinkDuration = 1f;

    public override void Use()
    {
        StartCoroutine(DestroyChildrenWithShrink());
    }

    private IEnumerator DestroyChildrenWithShrink()
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in target)
        {
            children.Add(child);
        }

        float elapsedTime = 0;
        while (elapsedTime < shrinkDuration)
        {
            foreach (Transform child in children)
            {
                if (child != null)
                {
                    child.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, elapsedTime / shrinkDuration);
                }
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (Transform child in children)
        {
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
