using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour{
    public void Restart()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
