using System.Collections;
using UnityEngine;


[HelpURL("https://docs.google.com/document/d/1rdTEVSrCcYOjqTJcFCHj46RvnbdJhmQUb3gHMDhVftI/edit?usp=sharing")]
public class ScalerModule : MonoBehaviour
{
    [Header("Scale Settings")]
    [Tooltip("Target scale when activated")]
    [Min(0.1f)] // Ограничение минимального значения
    public Vector3 targetScale = new Vector3(2, 2, 2);

    [Tooltip("Default scale of the object")]
    [Min(0.1f)]
    public Vector3 defaultScale = Vector3.one; // Делаем настраиваемым из инспектора

    [Space(10)]
    [Tooltip("Speed of scaling animation")]
    [Min(0.01f)]
    public float changeSpeed = 1f;

    [Header("Debug")]
    [SerializeField]
    private bool toDefault = false;

    private Transform myTransform;

    private void Awake()
    {
        myTransform = transform;
        // Сохраняем начальный scale, если defaultScale не был изменен в инспекторе
        if (defaultScale == Vector3.one)
        {
            defaultScale = myTransform.localScale;
        }
    }

    // Метод для вызова из редактора
    [ContextMenu("Activate Module")]
    public void ActivateModule()
    {
        Vector3 target = toDefault ? defaultScale : targetScale;
        StopAllCoroutines();
        StartCoroutine(ScaleCoroutine(target));
        toDefault = !toDefault;
    }

    // Метод для вызова из редактора
    [ContextMenu("Return to Default")]
    public void ReturnToDefaultState()
    {
        toDefault = true;
        ActivateModule();
    }

    private IEnumerator ScaleCoroutine(Vector3 target)
    {
        Vector3 start = myTransform.lossyScale;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * changeSpeed;
            myTransform.localScale = Vector3.Lerp(start, target, t);
            yield return null;
        }
        myTransform.localScale = target;
    }

    // Добавляем метод для задания из задания (возможно опечатка в ActionMethodule)
    public void ActionMethodule()
    {
        ActivateModule(); // Или другая логика, если требуется
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Нажатие пробела
        {
            ActivateModule();
        }
    }
}