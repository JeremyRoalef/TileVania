using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class tpUIscript : MonoBehaviour
{

    Image image;

    bool boolUpdateTpTimer = false;
    float fltTpCD = 5f;

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
        boolUpdateTpTimer = true;
        image.fillAmount = 0;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.33f);

    }

    void UpdateTimer()
    {

        if (!boolUpdateTpTimer)
        {
            return;
        }

        fltTpCD -= Time.deltaTime;

        if (fltTpCD >= 0)
        {
            image.fillAmount = 1 - (fltTpCD / 5);
        }
        else
        {
            boolUpdateTpTimer = false;
            image.fillAmount = 0;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            fltTpCD = 5f;
        }
    }

    public bool TpOnCooldown()
    {
        if (boolUpdateTpTimer == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
