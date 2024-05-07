using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    [SerializeField] private Volume volume;
    private Vignette redDamageEffect;
    private float intensity;
    private bool takingDamage = false;

    private void Start()
    {
        volume.profile.TryGet<Vignette>(out redDamageEffect);
    }

    public void TakeDamageStartEffect()
    {
        if(!takingDamage)
            StartCoroutine(TakeDamageEffect());
    }

    IEnumerator TakeDamageEffect()
    {
        takingDamage = true;
        intensity = 0;

        while (intensity < 0.3f)
        {
            intensity += 0.01f;
            redDamageEffect.intensity.Override(intensity);
            yield return new WaitForSeconds(0.007f);
        }

        while (intensity > 0)
        {
            intensity -= 0.01f;
            redDamageEffect.intensity.Override(intensity);
            yield return new WaitForSeconds(0.02f);
        }
        takingDamage = false;
        yield break;
    }
}
