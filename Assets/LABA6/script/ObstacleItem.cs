using UnityEngine;
using UnityEngine.Events;

public class ObstacleItem : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 1)] public float health = 1f;
    public float colorChangeSpeed = 3f;
    public float shrinkSpeed = 1f;

    [Header("Event")]
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
            transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
            if (transform.localScale.x <= 0.05f)
            {
                Destroy(gameObject);
            }
            return;
        }
        displayedHealth = Mathf.MoveTowards(displayedHealth, health, colorChangeSpeed * Time.deltaTime);
        UpdateColor();
    }

    public void GetDamage(float damage)
    {
        if (isDestroying) return;

        health = Mathf.Clamp(health - damage, 0, 1);

        if (health <= 0)
        {
            
            displayedHealth = 0f;
            UpdateColor();

            
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