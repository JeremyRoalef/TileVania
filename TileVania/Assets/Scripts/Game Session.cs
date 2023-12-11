using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{

    [SerializeField] int intPlayerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    bool boolPlayerHasShootAbility = false;
    bool boolPlayerHasTpAbility = false;

    int intScore = 0;

    void Awake()
    {
        int intNumGameSession = FindObjectsOfType<GameSession>().Length;
        if (intNumGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = intPlayerLives.ToString();

        //https://docs.unity3d.com/ScriptReference/Cursor-visible.html

    }

    public void ProcessPlayerDeath()
    {
        if (intPlayerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int intPointsToAdd)
    {
        intScore += intPointsToAdd;
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersists();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        
    }


    void TakeLife()
    {
        intPlayerLives --;
        int intCurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(intCurrentSceneIndex);
        livesText.text = intPlayerLives.ToString();
    }

    void Update()
    {
        scoreText.text = "Score: " + intScore.ToString();
    }

    public bool PlayerHasShootAbility()
    {
        return boolPlayerHasShootAbility;
    }
    public bool PlayerHasTpAbility()
    {
        return boolPlayerHasTpAbility;
    }

    public void PlayerPickedUpTpAbility(bool boolHasPickedUpTp)
    {
        boolPlayerHasTpAbility = boolHasPickedUpTp;
    }
    public void PlayerPickedUpShootAbility(bool boolHasPickedUpShoot)
    {
        boolPlayerHasShootAbility = boolHasPickedUpShoot;
    }
}
