using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public string scene;

    public void LoadSceneButton()
    {
        Debug.Break();
        SceneManager.LoadScene("level1Back");
    }
}