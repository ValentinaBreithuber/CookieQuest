using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Animator anim;
    [SerializeField] public GameManager gameManager;
    [SerializeField] public PlayerMovement playerMov;
    private bool isDead;
    public GameObject gameOverScreen;

    void Start()
    {
        gameOverScreen.SetActive(true);
    }

    private void OnCollisionEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player") && !isDead) {
            Debug.Log("Dead");
            anim.SetBool("idle",false);
            anim.SetBool("hurt",true);
            isDead=true;
            gameManager.gameOver();
            playerMov.death();
        }
    }
}
