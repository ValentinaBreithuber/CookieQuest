using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public Animator animator;
    public void StartGame()
    {
        Debug.Log("Start");
        LoadNextLevel();
    }
        public void restartLevel() {
            Debug.Log("Restart Level "+SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel() {
        Debug.Log("loading Level 1");
        StartCoroutine(LoadLevel(1));
    }

    public void LoadMainMenu() {
        Debug.Log("loading menu");

        SceneManager.LoadScene(0);
    }

    // create coroutine to delay code execution
    IEnumerator LoadLevel(int levelIndex) {
        Time.timeScale=1;
        // play animation
        animator.SetTrigger("start");
        // wait for finish
        yield return new WaitForSeconds(1);
        //load new scene
        SceneManager.LoadScene(levelIndex);
    }


    public void QuitGame()
    {
        Debug.Log("Quit");
        Time.timeScale=0;
    }
}
