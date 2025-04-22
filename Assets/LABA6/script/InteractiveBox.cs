using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class InteractiveBox : MonoBehaviour
{
    private InteractiveBox next;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // ��������� LineRenderer
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        // ������ ����� �� ��������� next
        lineRenderer.enabled = false;
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

            // ��������� �����
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

            // �������� ��������� � obstacle
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
            // �������� �����, ���� ����� ������ ���
            lineRenderer.enabled = false;
        }
    }
}
