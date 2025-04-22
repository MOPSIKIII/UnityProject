using UnityEngine;

public class InteractiveBox : MonoBehaviour
{
    private InteractiveBox next;

    public void AddNext(InteractiveBox box)
    {
        next = box;
    }

    private void Update()
    {
        if (next != null)
        {
            Vector3 direction = next.transform.position - transform.position;

            // Рисуем луч, видимый и в Scene, и в Game
            Debug.DrawLine(transform.position, next.transform.position, Color.green);

            // Проверяем, попадает ли луч в ObstacleItem
            Ray ray = new Ray(transform.position, direction.normalized);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, direction.magnitude))
            {
                ObstacleItem obstacle = hit.collider.GetComponent<ObstacleItem>();
                if (obstacle != null)
                {
                    obstacle.GetDamage(Time.deltaTime); // наносим урон плавно
                }
            }
        }
    }
}
