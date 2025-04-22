using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class InteractiveBox : MonoBehaviour
{
    private InteractiveBox next;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 0;
    }

    public void AddNext(InteractiveBox box)
    {
        next = box;
        lineRenderer.enabled = true;
    }

    private void Update()
    {
        if (next != null)
        {
            Vector3 start = transform.position;
            Vector3 end = next.transform.position;
            Vector3 direction = (end - start).normalized;
            float distance = Vector3.Distance(start, end);

            Ray ray = new Ray(start, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distance))
            {
                ObstacleItem obstacle = hit.collider.GetComponent<ObstacleItem>();
                if (obstacle != null && !obstacle.isDestroying)
                {
                    // Наносим урон
                    obstacle.GetDamage(Time.deltaTime);

                    // Рисуем луч до препятствия
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, start);
                    lineRenderer.SetPosition(1, hit.point);
                    return;
                }
            }

            // Если нет препятствий — рисуем до next
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }
}