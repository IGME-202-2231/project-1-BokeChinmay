using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    string sceneToLoad;


    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
