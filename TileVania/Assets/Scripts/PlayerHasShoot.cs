using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerHasShoot : MonoBehaviour
{
    Image image;
    PlayerMovement playerMovement;

    void Start()
    {
        image = GetComponent<Image>();
    }


    void Update()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        if (!playerMovement.PlayerHasShootAbility())
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }
        else
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        }
    }
}
