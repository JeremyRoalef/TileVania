using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameCanvas : MonoBehaviour
{

    Image image;

    bool boolUpdateShootTimer = false;
    float fltShootCD = 5f;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        UpdateTimer();
    }

    public void SetTimer()
    {
        boolUpdateShootTimer = true;
        image.fillAmount = 0;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.33f);

    }

    void UpdateTimer()
    {

        if (!boolUpdateShootTimer)
        {
            return;
        }

        fltShootCD -= Time.deltaTime;

        if (fltShootCD >= 0)
        {
            image.fillAmount = 1 - (fltShootCD / 5);
        }
        else
        {
            boolUpdateShootTimer = false;
            image.fillAmount = 0;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            fltShootCD = 5f;
        }
    }

    public bool ShootOnCooldown()
    {
        if (boolUpdateShootTimer == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
