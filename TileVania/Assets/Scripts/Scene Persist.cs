using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        int intNumScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (intNumScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersists()
    {
        Destroy(gameObject);
    }
}