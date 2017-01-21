using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public Animator anim;


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
}
