using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Задаёт указанным объектам значение activeSelf, равное state.
/// </summary>
[HelpURL("https://docs.google.com/document/d/1GP4_m0MzOF8L5t5pZxLChu3V_TFIq1czi1oJQ2X5kpU/edit?usp=sharing")]
public class GameObjectActivator : MonoBehaviour
{
    [SerializeField]
    private List<StateContainer> targets = new List<StateContainer>();

    [SerializeField]
    private bool debug = false;

    private void Awake()
    {
        if (targets == null || targets.Count == 0)
        {
            Debug.LogWarning("Список targets пуст, GameObjectActivator не будет работать!", gameObject);
            return;
        }

        foreach (var item in targets)
        {
            if (item.targetGO != null)
            {
                item.defaultValue = item.targetGO.activeSelf;
            }
            else
            {
                Debug.LogError("У StateContainer отсутствует targetGO! Проверьте настройки в инспекторе.", gameObject);
            }
        }
    }

    public void ActivateModule()
    {
        SetStateForAll();
    }

    public void ReturnToDefaultState()
    {
        foreach (var item in targets)
        {
            if (item.targetGO != null)
            {
                item.targetState = item.defaultValue;
                item.targetGO.SetActive(item.defaultValue);
            }
        }
    }

    private void SetStateForAll()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null && targets[i].targetGO != null)
            {
                targets[i].targetGO.SetActive(targets[i].targetState);
                targets[i].targetState = !targets[i].targetState;
            }
            else
            {
                Debug.LogError($"Элемент {i} равен null или не имеет targetGO. Проверьте ссылки в инспекторе!", gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(transform.position, 0.3f);

            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] != null && targets[i].targetGO != null)
                {
                    Gizmos.color = targets[i].targetState ? Color.green : Color.red;
                    Gizmos.DrawLine(transform.position, targets[i].targetGO.transform.position);
                }
            }
        }
    }
}

[System.Serializable]
public class StateContainer
{
    [Tooltip("Объект, которому нужно задать состояние")] 
    public GameObject targetGO;

    [Tooltip("Целевое состояние. Если отмечено, объект будет включен")] 
    public bool targetState = false;

    [HideInInspector] 
    public bool defaultValue;
}
