using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePosition : MonoBehaviour
{

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
