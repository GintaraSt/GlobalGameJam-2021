using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicByLevel : MonoBehaviour
{
    public AudioClip[] audioList;
    public string[] sceneNames;
    private string sceneBefore, currentScene;
    void Start()
    {
        sceneBefore = SceneManager.GetActiveScene().name;
        currentScene = sceneBefore;
        this.gameObject.GetComponent<AudioSource>().clip = audioList[0];
        this.gameObject.GetComponent<AudioSource>().Play();

    }
    private int ReturnSceneNumber(string name){
        for (int i = 0; i<sceneNames.Length; i++){
            if (name.Equals(sceneNames[i])) return i;
        }
        return 0;
    }
    void Update()
    {
        if( !(currentScene.Equals( SceneManager.GetActiveScene().name ) ) ){
            currentScene = SceneManager.GetActiveScene().name;
            if( !( currentScene.Equals(sceneBefore) ) ){
                sceneBefore = currentScene;
                this.gameObject.GetComponent<AudioSource>().Stop();
                this.gameObject.GetComponent<AudioSource>().clip = audioList[ReturnSceneNumber(currentScene)];
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }
}
