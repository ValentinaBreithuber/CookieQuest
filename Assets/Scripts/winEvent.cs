using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winEvent : MonoBehaviour
{
    public LevelLoader levelLoader;
    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            levelLoader.LoadNextLevel();
        }
    }
}
