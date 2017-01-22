using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    public Animator anim;
    public float fallingSpeed = 10;


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            Debug.Log("AyyyLmao");
            coll.gameObject.GetComponent<MBullet>().Reset();
        }
        if (coll.gameObject.tag == "Collectable")
        {
            if (coll.name == "DataFly")
            {
                PlayerData.Points += 1;
                coll.gameObject.SetActive(false);
            }
        }
        if (coll.gameObject.tag == "enemy")
        {

            Death();
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
