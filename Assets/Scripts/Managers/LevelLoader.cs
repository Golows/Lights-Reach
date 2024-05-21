using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;
    public void LoadNextScene(int index)
    {
        StartCoroutine(LoadScene(index));
    }

    public void LoadAfterDeath(int index)
    {
        animator.SetTrigger("Start");
        SceneManager.LoadScene(index);
    }

    IEnumerator LoadScene(int index)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(0.333f);
        SceneManager.LoadScene(index);
    }
}
