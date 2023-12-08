using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePosition : MonoBehaviour
{
    //https://www.youtube.com/watch?v=5NTmxDSKj-Q&t=1s

    public Vector3 screenPosition;
    public Vector3 worldPosition;

    void Start()
    {
        
    }

    void Update()
    {
        screenPosition = Mouse.current.position.ReadValue();
        screenPosition.z = Camera.main.nearClipPlane + 1;

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        transform.position = worldPosition;
    }
}
