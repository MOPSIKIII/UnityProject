using UnityEngine;
using UnityEngine.Events;

public class ObstacleMem : MonoBehaviour
{
    [Header("���������")]
<<<<<<< Updated upstream
    [Range(0, 1)] public float health = 1f;
    public float colorChangeSpeed = 3f;
    public float shrinkSpeed = 1f;
=======
    [Range(0, 1)] public float health = 1f; // ������������ currentVolume � health ��� �������
    public float colorChangeSpeed = 3f; // ��������� �������� ��� ��������� �����
    public float shrinkSpeed = 1f; // �������� ���������� �������
>>>>>>> Stashed changes

    [Header("�������")]
    public UnityEvent onDestroyObstacles;

    private Material objectMaterial;
    private bool isDestroying = false;
<<<<<<< Updated upstream
    private float displayedHealth;
=======
    private float displayedHealth; // ������� ������������ �������� ��������
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
        // ������� ������������� ������������� ��������
        displayedHealth = Mathf.MoveTowards(
            displayedHealth,
            health,
            colorChangeSpeed * Time.deltaTime
        );

        UpdateColor();

>>>>>>> Stashed changes
        if (isDestroying)
        {
            // �������� ����������
            transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
            if (transform.localScale.x <= 0.05f)
            {
                Destroy(gameObject);
            }
<<<<<<< Updated upstream
            return;
        }

        // ������� ��������� �����
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
            // ��������� ������������� ������� ����
            displayedHealth = 0f;
            UpdateColor();

            // ��������� �����������
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