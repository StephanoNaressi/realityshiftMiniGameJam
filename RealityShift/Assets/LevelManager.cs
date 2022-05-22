using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadNextLevel(int n)
    {
        SceneManager.LoadScene(n);
    }
    public void LostLevel()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void WinLevel()
    {
        SceneManager.LoadScene("WinLvl");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
