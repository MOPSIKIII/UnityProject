using UnityEngine;

public class InteractiveRaycast : MonoBehaviour
{
    public GameObject prefab;
    private InteractiveBox selectedBox;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleLeftClick();
        }

        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick();
        }
    }

    void HandleLeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj.CompareTag("InteractivePlane"))
            {
                Vector3 offset = hit.normal * 0.5f;
                Vector3 spawnPos = hit.point + offset;

                spawnPos = new Vector3(
                    Mathf.Round(spawnPos.x),
                    Mathf.Round(spawnPos.y),
                    Mathf.Round(spawnPos.z)
                );

                Instantiate(prefab, spawnPos, Quaternion.identity);
            }

            else if (hitObj.TryGetComponent<InteractiveBox>(out InteractiveBox box))
            {
                if (selectedBox == null)
                {
                    selectedBox = box;
                }
                else if (selectedBox != box)
                {
                    selectedBox.AddNext(box);
                    selectedBox = null;
                }
            }
        }
    }

    void HandleRightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<InteractiveBox>(out InteractiveBox box))
            {
                Destroy(box.gameObject);

                if (selectedBox == box)
                {
                    selectedBox = null;
                }
            }
        }
    }
}

