using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenuManager : MonoBehaviour
{
    public void OnRestartClick()
    {
        print("Restarting");
        SceneManager.LoadScene(0);
    }

    public void OnExitClick()
    {
        print("Quitting");
        Application.Quit();
    }
}
