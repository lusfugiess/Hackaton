using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public string level1; 

    public void OnButtonClick()
    {
        SceneManager.LoadScene(level1);
    }
}
