using UnityEngine;
using UnityEngine.Events;

public class ObstacleMem : MonoBehaviour
{
    [Header("Настройки")]
<<<<<<< Updated upstream
    [Range(0, 1)] public float health = 1f;
    public float colorChangeSpeed = 3f;
    public float shrinkSpeed = 1f;
=======
    [Range(0, 1)] public float health = 1f; // переименовал currentVolume в health для ясности
    public float colorChangeSpeed = 3f; // отдельная скорость для изменения цвета
    public float shrinkSpeed = 1f; // скорость уменьшения объекта
>>>>>>> Stashed changes

    [Header("События")]
    public UnityEvent onDestroyObstacles;

    private Material objectMaterial;
    private bool isDestroying = false;
<<<<<<< Updated upstream
    private float displayedHealth;
=======
    private float displayedHealth; // текущее отображаемое значение здоровья
>>>>>>> Stashed changes

    void Start()
    {
        objectMaterial = GetComponent<Renderer>().material;
        displayedHealth = health;
        UpdateColor();
    }

    void Update()
    {
<<<<<<< Updated upstream
=======
        // Плавная корректировка отображаемого здоровья
        displayedHealth = Mathf.MoveTowards(
            displayedHealth,
            health,
            colorChangeSpeed * Time.deltaTime
        );

        UpdateColor();

>>>>>>> Stashed changes
        if (isDestroying)
        {
            // Анимация уменьшения
            transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
            if (transform.localScale.x <= 0.05f)
            {
                Destroy(gameObject);
            }
<<<<<<< Updated upstream
            return;
        }

        // Плавное изменение цвета
        displayedHealth = Mathf.MoveTowards(displayedHealth, health, colorChangeSpeed * Time.deltaTime);
        UpdateColor();
=======
        }
>>>>>>> Stashed changes
    }

    public void GetDamage(float damage)
    {
        if (isDestroying) return;

        health = Mathf.Clamp(health - damage, 0, 1);

        if (health <= 0)
        {
<<<<<<< Updated upstream
            // Мгновенно устанавливаем красный цвет
            displayedHealth = 0f;
            UpdateColor();

            // Запускаем уничтожение
            isDestroying = true;
            onDestroyObstacles.Invoke();
=======
            StartDestroy();
>>>>>>> Stashed changes
        }
    }

    void UpdateColor()
    {
        objectMaterial.color = Color.Lerp(Color.red, Color.white, displayedHealth);
    }

<<<<<<< Updated upstream
=======
    void StartDestroy()
    {
        isDestroying = true;
        onDestroyObstacles.Invoke();
    }

>>>>>>> Stashed changes
    void OnDestroy()
    {
        if (objectMaterial != null)
        {
            Destroy(objectMaterial);
        }
    }
}