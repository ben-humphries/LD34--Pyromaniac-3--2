using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

    public void changeScene(int sceneIndex)
    {
        Application.LoadLevel(sceneIndex);
    }
    public void nextScene()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }
    public void restartScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void quit()
    {
        Application.Quit();
    }
    public void toggleMusic(bool music)
    {
        if (music)
        {
            PlayerPrefs.SetInt("music", 1);
        }
        else
        {
            PlayerPrefs.SetInt("music", 0);
        }
    }
}
