using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToScene : MonoBehaviour
{
    public void ChangeScene(int nScene)
    {
        SceneManager.LoadScene(nScene);
        Time.timeScale = 1;
    }
}
