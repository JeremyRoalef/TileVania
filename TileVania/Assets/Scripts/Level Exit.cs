using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float fltLevelLoadDelay = 1f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(fltLevelLoadDelay);

        int intCurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int intNextSceneIndex = intCurrentSceneIndex + 1;

        if (intNextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            intNextSceneIndex = 0;
        }

        SceneManager.LoadScene(intNextSceneIndex);
    }
}
