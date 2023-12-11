using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] float fltLevelLoadDelay = 1f;

    //Updade script to do things if scene level index is last room in the level
    // ex: if level 1-4 is last level in level 1, return to main menu
    //change how to swap scenes from using indexes to scene names

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }

    }

    public IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(fltLevelLoadDelay);

        int intCurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int intNextSceneIndex = intCurrentSceneIndex + 1;

        if (intNextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            intNextSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersists();
        SceneManager.LoadScene(intNextSceneIndex);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1-1");
    }
    public void Tutorial()
    {
        SceneManager.LoadScene(1);
    }
}
