using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
[SerializeField] private AudioClip deathSound;
    public GameObject gameOverScreen;
    public Animator animator;
    public int cookieCount=0;

    void Start()
    {
        gameOverScreen.SetActive(false);
    }
    
    public TMP_Text cookieText;

    public void restartLevel() {
        Debug.Log("Restart Level "+SceneManager.GetActiveScene().buildIndex);
            
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
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

    public void quitGame() {
            Debug.Log("Go to Main menu");
            SceneManager.LoadScene(0); //main menu
    }

    public void gameOver() {
        Time.timeScale=0;
        SoundManager.instance.playSound(deathSound);
        gameOverScreen.SetActive(true);
    }

    public void pointIncrease()
    {
        cookieCount = cookieCount+1;
        cookieText.text = "Points: "+cookieCount.ToString() + "/7";
    }
}
