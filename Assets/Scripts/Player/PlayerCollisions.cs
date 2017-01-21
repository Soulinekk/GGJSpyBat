using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            Debug.Log("AyyyLmao");
            coll.gameObject.GetComponent<MBullet>().Reset();
        }

    }
}
