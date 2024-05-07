using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private GameObject soundEffect;
    private int index = 0;

    public void PlaySoundEffectsInARow(AudioClip[] audioClips, Transform spawTransform, float volume)
    {
        if(index == audioClips.Length)
        {
            index = 0;
        }
        AudioSource audioSource = ObjectPoolManager.SpawnObject(soundEffect, spawTransform.position, Quaternion.identity, ObjectPoolManager.PoolType.Audio).GetComponent<AudioSource>();

        audioSource.clip = audioClips[index];
        index++;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLenght = audioSource.clip.length;

        StartCoroutine(RemoveObject(clipLenght, audioSource.gameObject));
    }

    public void PlaySoundEffectsRandom(AudioClip[] audioClips, Transform spawTransform, float volume)
    {
        
        AudioSource audioSource = ObjectPoolManager.SpawnObject(soundEffect, spawTransform.position, Quaternion.identity, ObjectPoolManager.PoolType.Audio).GetComponent<AudioSource>();

        int random = Random.Range(0, audioClips.Length);

        audioSource.clip = audioClips[random];

        audioSource.volume = volume;

        audioSource.Play();

        float clipLenght = audioSource.clip.length;

        StartCoroutine(RemoveObject(clipLenght, audioSource.gameObject));
    }

    public void PlaySoundEffect(AudioClip audioClip, Transform spawTransform, float volume)
    {
        AudioSource audioSource = ObjectPoolManager.SpawnObject(soundEffect, spawTransform.position, Quaternion.identity, ObjectPoolManager.PoolType.Audio).GetComponent<AudioSource>();

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLenght = audioSource.clip.length;

        StartCoroutine(RemoveObject(clipLenght, audioSource.gameObject));
    }

    IEnumerator RemoveObject(float waitTime, GameObject objectToRemove)
    {
        yield return new WaitForSeconds(waitTime);
        ObjectPoolManager.RemoveObjectToPool(objectToRemove);
        yield break;
    }
}
