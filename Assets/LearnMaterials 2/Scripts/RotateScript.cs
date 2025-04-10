using UnityEngine;
using UnityEngine.UI; // ��� ������ � UI

public class RotateScript : SampleScript
{
    [Tooltip("�������� �������� � �������� � �������")]
    public float rotationSpeed = 10f;

    [Tooltip("���� �������� ������ ��� X")]
    public float targetRotationX = 0f;

    [Tooltip("���� �������� ������ ��� Y")]
    public float targetRotationY = 0f;

    [Tooltip("���� �������� ������ ��� Z")]
    public float targetRotationZ = 0f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool isRotating = false;
    private float progress = 0f;

    private void Start()
    {
        initialRotation = transform.rotation;
        CalculateTargetRotation();
    }

    private void Update()
    {
        if (isRotating)
        {
            progress += rotationSpeed * Time.deltaTime / GetMaxRotationAngle();
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, progress);

            if (progress >= 1f)
            {
                isRotating = false;
                progress = 0f;
            }
        }
    }

    public override void Use()
    {
        StartRotation();
    }

    public void StartRotation()
    {
        if (!isRotating)
        {
            initialRotation = transform.rotation;
            CalculateTargetRotation();
            progress = 0f;
            isRotating = true;
        }
    }

    private void CalculateTargetRotation()
    {
        targetRotation = initialRotation * Quaternion.Euler(targetRotationX, targetRotationY, targetRotationZ);
    }

    private float GetMaxRotationAngle()
    {
        return Mathf.Max(Mathf.Abs(targetRotationX), Mathf.Abs(targetRotationY), Mathf.Abs(targetRotationZ));
    }

    public void ResetRotation()
    {
        transform.rotation = initialRotation;
        isRotating = false;
        progress = 0f;
    }
}
