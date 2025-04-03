using System.Collections.Generic;
using UnityEngine;

public class SampleScriptController : MonoBehaviour
{
    public List<SampleScript> scripts;

    public void ActivateAll()
    {
        foreach (var script in scripts)
        {
            script.Use();
        }
    }
}
