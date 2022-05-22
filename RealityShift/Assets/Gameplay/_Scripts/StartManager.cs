using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public void Play(int index)
    {
        bool isGoingToLevel = SceneManager.GetSceneByName("LostLvl") != SceneManager.GetActiveScene();
        if (isGoingToLevel) { PlayerPrefs.SetInt("currentLevel", index); }
        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
             Application.Quit();
        #endif
    }

    public void RetryLevel()
    {
        Play(PlayerPrefs.GetInt("currentLevel"));
    }
}
