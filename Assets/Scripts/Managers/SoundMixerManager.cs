using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private Slider enemySlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider ambientSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("Master"))
        {
            audioMixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat("Master")) * 20f);
            masterSlider.value = PlayerPrefs.GetFloat("Master");
        }
            

        if (PlayerPrefs.HasKey("Effects"))
        {
            audioMixer.SetFloat("Effects", Mathf.Log10(PlayerPrefs.GetFloat("Effects")) * 20f);
            effectsSlider.value = PlayerPrefs.GetFloat("Effects");
        }
            

        if (PlayerPrefs.HasKey("Enemy"))
        {
            audioMixer.SetFloat("Enemy", Mathf.Log10(PlayerPrefs.GetFloat("Enemy")) * 20f);
            enemySlider.value = PlayerPrefs.GetFloat("Enemy");
        }
            

        if (PlayerPrefs.HasKey("Music"))
        {
            audioMixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("Music")) * 20f);
            musicSlider.value = PlayerPrefs.GetFloat("Music");
        }
            

        if (PlayerPrefs.HasKey("Ambient"))
        {
            audioMixer.SetFloat("Ambient", Mathf.Log10(PlayerPrefs.GetFloat("Ambient")) * 20f);
            ambientSlider.value = PlayerPrefs.GetFloat("Ambient");
        }
            
    }

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("Master", level);
    }

    public void SetSoundEffectsVolume(float level)
    {
        audioMixer.SetFloat("Effects", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("Effects", level);
    }

    public void SetEnemyVolume(float level)
    {
        audioMixer.SetFloat("Enemy", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("Enemy", level);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("Music", level);
    }

    public void SetAmbientVolume(float level)
    {
        audioMixer.SetFloat("Ambient", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("Ambient", level);
    }
}
