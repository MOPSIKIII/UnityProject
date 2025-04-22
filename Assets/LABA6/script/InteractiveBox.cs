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
            Vector3 direction = end - start;

            Ray ray = new Ray(start, direction.normalized);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, direction.magnitude))
            {
                Debug.Log("hit");
                ObstacleItem obstacle = hit.collider.GetComponent<ObstacleItem>();
                if (obstacle != null)
                {
                    obstacle.GetDamage(Time.deltaTime);

                    // Вычисляем вход и выход для линии
                    Vector3 hitPoint = hit.point;
                    Vector3 obstacleSize = hit.collider.bounds.size;
                    Vector3 exitPoint = hit.point + direction.normalized * obstacleSize.z;

                    lineRenderer.positionCount = 3;
                    lineRenderer.SetPosition(0, start);
                    lineRenderer.SetPosition(1, hitPoint);
                    lineRenderer.SetPosition(2, exitPoint);
                    return;
                }
            }

            // Если нет препятствий — обычная линия
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
