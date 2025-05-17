using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class robi : PlayerMovement
{
    public AIPath aiPath;


    // Update is called once per frame
    void Update()
    {
        //Flip sprite
        if (aiPath.desiredVelocity.x >= 0.01f) {
            transform.localScale = new Vector3(1f,1f,1f);
        } else if (aiPath.desiredVelocity.x <= -0.01f) {
            transform.localScale = new Vector3(-1f,1f,1f);
        }
    }

     private void OnCollisionEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player") && !isDead) {
           death();
        }
    }

    private void death() {
        isDead=true;
        SoundManager.instance.playSound(deathSound);
        gManag.gameOver();
        Debug.Log("Dead");
        anim.SetTrigger("hurt");
        anim.SetBool("hurt",true);
        new WaitForSeconds(2f);
        anim.SetBool("hurt",false);
    }
}
