using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //go here when winEvent
    public Animator animator;
    [SerializeField] private AudioClip winSound;


    public void LoadNextLevel() {
        Debug.Log("Next Level "+SceneManager.GetActiveScene().buildIndex+1);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex+1));
    }
    public void ReloadLevel() {
        Debug.Log("Next Level "+SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadMainMenu() {
        Debug.Log("loading menu");
        SceneManager.LoadScene(0);
    }

    // create coroutine to delay code execution
    IEnumerator LoadLevel(int levelIndex) {
        SoundManager.instance.playSound(winSound);
        // play animation
        animator.SetTrigger("start");
        // wait for finish
        yield return new WaitForSeconds(1);
        //load new scene
        SceneManager.LoadScene(levelIndex);
    }
}
