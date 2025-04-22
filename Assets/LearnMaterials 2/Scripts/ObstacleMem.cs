using UnityEngine;
using UnityEngine.Events;

public class ObstacleMem : MonoBehaviour
{
    [Header("Настройки")]
    [Range(0, 1)] public float health = 1f;
    public float colorChangeSpeed = 3f;
    public float shrinkSpeed = 1f;

    [Header("События")]
    public UnityEvent onDestroyObstacles;

    private Material objectMaterial;
    private bool isDestroying = false;
    private float displayedHealth;

    void Start()
    {
        objectMaterial = GetComponent<Renderer>().material;
        displayedHealth = health;
        UpdateColor();
    }

    void Update()
    {
        if (isDestroying)
        {
            // Анимация уменьшения
            transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
            if (transform.localScale.x <= 0.05f)
            {
                Destroy(gameObject);
            }
            return;
        }

        // Плавное изменение цвета
        displayedHealth = Mathf.MoveTowards(displayedHealth, health, colorChangeSpeed * Time.deltaTime);
        UpdateColor();
    }

    public void GetDamage(float damage)
    {
        if (isDestroying) return;

        health = Mathf.Clamp(health - damage, 0, 1);

        if (health <= 0)
        {
            // Мгновенно устанавливаем красный цвет
            displayedHealth = 0f;
            UpdateColor();

            // Запускаем уничтожение
            isDestroying = true;
            onDestroyObstacles.Invoke();
        }
    }

    void UpdateColor()
    {
        objectMaterial.color = Color.Lerp(Color.red, Color.white, displayedHealth);
    }

    void OnDestroy()
    {
        if (objectMaterial != null)
        {
            Destroy(objectMaterial);
        }
    }
}