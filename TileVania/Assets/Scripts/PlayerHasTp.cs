using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerHasTp : MonoBehaviour
{
    Image image;
    GameSession gameSession;

    void Start()
    {
        image = GetComponent<Image>();
    }


    void Update()
    {
        gameSession = FindObjectOfType<GameSession>();

        if (!gameSession.PlayerHasTpAbility())
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }
        else
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        }
    }
}
