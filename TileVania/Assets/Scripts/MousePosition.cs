using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePosition : MonoBehaviour
{
    //https://www.youtube.com/watch?v=5NTmxDSKj-Q&t=1s

    public Vector3 screenPosition;
    public Vector3 worldPosition;

    bool boolPlayerCanTeleport = true;

    void Start()
    {
        //https://docs.unity3d.com/ScriptReference/Cursor-visible.html
        Cursor.visible = false;
    }

    void Update()
    {
        /*
        screenPosition = Mouse.current.position.ReadValue();
        screenPosition.z = Camera.main.nearClipPlane + 1;

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        transform.position = worldPosition;
        */
    }

    private void LateUpdate()
    {
        screenPosition = Mouse.current.position.ReadValue();
        screenPosition.z = Camera.main.nearClipPlane + 1;

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        transform.position = worldPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "OutOfBounds")
        {
            Debug.Log("TRIGGERED");
            boolPlayerCanTeleport = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "OutOfBounds")
        {
            Debug.Log("TRIGGERED");
            boolPlayerCanTeleport = true;
        }
    }

    public Vector3 GetMousePosition()
    {
        return worldPosition;
    }
    public bool CanTeleport()
    {
        return boolPlayerCanTeleport;
    }
}
