using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{


    //strLoadScene is used to load scenes by name. Scene name will be created by taking the level value and room value
    //and combining it into one string.
    //Ex: to load level 2, room 5, strLoadScene would become "Level 2-5"

    string strLoadScene;
    int intLevel = 0;
    int intRoom = 0;

    public CanvasGroup canvas;

    private void Start()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    void Awake()
    {
        int intNumGameSession = FindObjectsOfType<SceneTransition>().Length;
        if (intNumGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

     public void LoadNextLevel()
    {
        intRoom++;
        strLoadScene = "Level " + intLevel + "-" + intRoom;
        Debug.Log(strLoadScene);


        FindObjectOfType<ScenePersist>().ResetScenePersists();

        if (SceneExists(strLoadScene))
        {
            SceneManager.LoadScene(strLoadScene);
        }
        else
        {
            SceneManager.LoadScene("Start Screen");
            ShowCanvas();
        }
    }

    public void StartLevel1()
    {

        HideCanvas();

        intLevel = 1;
        intRoom = 1;

        strLoadScene = "Level " + intLevel + "-" + intRoom;
        if (SceneExists(strLoadScene))
        {
            SceneManager.LoadScene(strLoadScene);
        }
        else
        {
            SceneManager.LoadScene("Start Screen");
            ShowCanvas();
        }
    }

    private void HideCanvas()
    {
        canvas.interactable = false;
        canvas.alpha = 0;
    }
    private void ShowCanvas()
    {
        canvas.interactable = true;
        canvas.alpha = 1;
    }

    //https://github.dev/lordofduct/spacepuppy-unity-framework-3.0/blob/a2bc50c213c90ae955dda0fd479349a64fa530f2/SPScenes/Scenes/SPSceneManager.cs#L153#L164
    public bool SceneExists(string sceneName, bool excludeInactive = false)
    {
        if (excludeInactive)
        {
            var sc = SceneManager.GetSceneByName(sceneName);
            return sc.IsValid();
        }
        else
        {
            return SceneUtility.GetBuildIndexByScenePath(sceneName) >= 0;
        }
    }
}
