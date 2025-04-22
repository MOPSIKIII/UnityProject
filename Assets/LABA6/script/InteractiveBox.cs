using UnityEngine;

public class InteractiveBox : MonoBehaviour
{
    private InteractiveBox next;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Настройка по умолчанию
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
    }

    public void AddNext(InteractiveBox box)
    {
        next = box;
    }

    private void Update()
    {
        if (next != null)
        {
            Vector3 start = transform.position;
            Vector3 end = next.transform.position;

            // Обновляем LineRenderer
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

            // Проверка попадания в obstacle
            Vector3 direction = end - start;
            Ray ray = new Ray(start, direction.normalized);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, direction.magnitude))
            {
                ObstacleItem obstacle = hit.collider.GetComponent<ObstacleItem>();
                if (obstacle != null)
                {
                    obstacle.GetDamage(Time.deltaTime);
                }
            }
        }
        else
        {
            // Скрываем линию, если нет next
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}
