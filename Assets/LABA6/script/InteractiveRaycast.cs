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
                BoxCollider collider = prefab.GetComponent<BoxCollider>();
                float objectHeight = (collider != null) ? collider.size.y : 1.0f; // Если нет коллайдера, устанавливаем высоту по умолчанию

                // Находим позицию для спавна с учетом высоты объекта
                Vector3 spawnPos = new Vector3(
                    Mathf.Round(hit.point.x),
                    hit.point.y + objectHeight / 2, // Смещаем вверх на половину высоты
                    Mathf.Round(hit.point.z)
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

