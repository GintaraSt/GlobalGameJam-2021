﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterScene : MonoBehaviour
{
    public string SceneName;
    public void EnterNextScene()
    {
        SceneManager.LoadScene(SceneName);
    }
    public void AplicationQuit(){
        Application.Quit();
    }
}
