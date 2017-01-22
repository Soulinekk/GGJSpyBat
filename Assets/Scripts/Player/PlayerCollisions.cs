using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    public BulbBoss boss;
    public Animator anim;
    public float fallingSpeed = 10;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            Death();
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            Death();
            //coll.gameObject.transform.localScale = new Vector3(10f, 10f, 10f);
        }
        if (coll.gameObject.tag == "Collectable")
        {
            if (coll.name == "BossTrigger")
            {
                boss.StartFight();
            }
            else if (coll.name == "luckyShot")
            {
                boss.gotHit = true;
                coll.transform.localRotation = Quaternion.Euler(Vector3.zero);
                coll.gameObject.SetActive(false);

            }
        }
        if (coll.gameObject.tag == "enemy")
        {

            Death();
            //coll.gameObject.transform.localScale = new Vector3(10f, 10f, 10f);
        }

        #region camera related

        if (coll.gameObject.tag == "CameraMask")
        {
            anim.SetBool("CameraMask", true);
            anim.SetBool("CameraMaskOff", false);
            anim.SetBool("CameraMaskFast",false);
        }

        if (coll.gameObject.tag == "CameraMaskOff")
        {
            anim.SetBool("CameraMask", false);
            anim.SetBool("CameraMaskOff", true);
            anim.SetBool("CameraMaskFast", false);
        }

        if (coll.gameObject.tag == "CameraMaskFast")
        {
            anim.SetBool("CameraMask", false);
            anim.SetBool("CameraMaskOff", false);
            anim.SetBool("CameraMaskFast", true);
        }
        #endregion
    }

    public void TurnOnMask()
    {
        anim.SetBool("CameraMask", true);
        anim.SetBool("CameraMaskOff", false);
        anim.SetBool("CameraMaskFast", false);
    }

    public void Death()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        StartCoroutine(LateDeath());
    }

    IEnumerator LateDeath()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().shouldFollow = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
