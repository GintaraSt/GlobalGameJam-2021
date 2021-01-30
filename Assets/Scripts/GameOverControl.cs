using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameOverControl : MonoBehaviour
{
    public bool gameOver = false;
    public Volume volume;

    public GameObject gameOverUI;

    public float slowdownTarget = 0.1f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            gameOver = true;
            gameOverUI.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        Debug.Log("restart");
        GameObject.FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>().ReloadScene();
        gameOver = false;
        if (volume.profile.TryGet<Bloom>(out var bloom))
        {
            bloom.intensity.overrideState = true;
            bloom.intensity.value = 1;
        }
        if (volume.profile.TryGet<Vignette>(out var vignette))
        {
            vignette.intensity.overrideState = true;
            vignette.intensity.value = 0;
            vignette.smoothness.value = 0;
        }
        //Time.timeScale = 1f;
        //Time.fixedDeltaTime = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameOverUI.SetActive(false);

    }

    public void GoToMainMenu()
    {
        //TODO: load main menu scene;
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (gameOver)
        {
            //Time.timeScale = slowdownTarget;
            //Time.fixedDeltaTime = 0.02F * Time.timeScale;
            if (volume.profile.TryGet<Bloom>(out var bloom))
            {
                bloom.intensity.overrideState = true;
                bloom.intensity.value = Mathf.Lerp(1, 25, 0.7f);
            }
            if (volume.profile.TryGet<Vignette>(out var vignette))
            {
                vignette.intensity.overrideState = true;
                vignette.intensity.value = Mathf.Lerp(0, 1, 0.1f);
                vignette.smoothness.value = Mathf.Lerp(0, 1, 0.1f);
            }
        }
    }
}
