using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private GameObject enemySoundEffects;
    [SerializeField] private GameObject soundEffects;
    [SerializeField] private GameObject ambientSounds;
    [SerializeField] private GameObject musicSounds;

    [SerializeField] private AudioClip abientClip;
    [SerializeField] private AudioClip musicClip;
    private int index = 0;

    public bool playMusic = false;

    public int maxEffect = 0;

    private void Start()
    {
        PlayAmbientSounds();
        if(playMusic)
            PlayMusic();
    }

    public void PlayAmbientSounds()
    {
        AudioSource audioSource = Instantiate(ambientSounds, GameController.instance.character.transform.position, Quaternion.identity).GetComponent<AudioSource>();
        audioSource.clip = abientClip;
        audioSource.volume = 0.4f;
        audioSource.Play();
    }

    public void PlayMusic()
    {
        AudioSource audioSource = Instantiate(musicSounds, GameController.instance.character.transform.position, Quaternion.identity).GetComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    public void PlaySoundEffectsInARow(AudioClip[] audioClips, Transform spawTransform, float volume)
    {
        if (index == audioClips.Length)
        {
            index = 0;
        }
        AudioSource audioSource = ObjectPoolManager.SpawnObject(soundEffects, spawTransform.position, Quaternion.identity, ObjectPoolManager.PoolType.Audio).GetComponent<AudioSource>();

        audioSource.clip = audioClips[index];
        index++;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLenght = audioSource.clip.length;

            

        StartCoroutine(RemoveObject(clipLenght, audioSource.gameObject, false));
    }

    public void PlaySoundEffectsRandom(AudioClip[] audioClips, Transform spawTransform, float volume)
    {
        if(maxEffect < 4)
        {
            AudioSource audioSource = ObjectPoolManager.SpawnObject(enemySoundEffects, spawTransform.position, Quaternion.identity, ObjectPoolManager.PoolType.Audio).GetComponent<AudioSource>();

            int random = Random.Range(0, audioClips.Length);

            audioSource.clip = audioClips[random];

            audioSource.volume = volume;

            audioSource.Play();
            maxEffect++;

            float clipLenght = audioSource.clip.length;

            StartCoroutine(RemoveObject(clipLenght, audioSource.gameObject, true));
        }
    }

    public void PlaySoundEffect(AudioClip audioClip, Transform spawTransform, float volume)
    {
        AudioSource audioSource = ObjectPoolManager.SpawnObject(soundEffects, spawTransform.position, Quaternion.identity, ObjectPoolManager.PoolType.Audio).GetComponent<AudioSource>();

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLenght = audioSource.clip.length;

        StartCoroutine(RemoveObject(clipLenght, audioSource.gameObject, false));
    }

    IEnumerator RemoveObject(float waitTime, GameObject objectToRemove, bool isEffect)
    {
        yield return new WaitForSeconds(waitTime);
        if(isEffect)
        {
            maxEffect--;
        }
        ObjectPoolManager.RemoveObjectToPool(objectToRemove);
        yield break;
    }
}
