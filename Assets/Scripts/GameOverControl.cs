using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameOverControl : MonoBehaviour
{
    bool gameOver = false;
    public Volume volume;

    public float slowdownTarget = 0.1f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            gameOver = true;
        }
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (gameOver)
        {
            Time.timeScale = slowdownTarget;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            if (volume.profile.TryGet<Bloom>(out var bloom))
            {
                bloom.intensity.overrideState = true;
                bloom.intensity.value = Mathf.Lerp(1, 25, 0.7f);
            }
            if (volume.profile.TryGet<FilmGrain>(out var filmGrain))
            {
                filmGrain.intensity.overrideState = true;
                filmGrain.intensity.value = Mathf.Lerp(0, 1, 0.1f);
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
