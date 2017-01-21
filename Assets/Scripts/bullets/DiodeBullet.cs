using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiodeBullet : MBullet {

    Rigidbody2D rb;
    GameObject player;
    public GameObject parent;
    public float speed;
    bool stop;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stop = true;
        player = GameObject.Find("Player");
       // parent = GetComponentInParent<Diode>().gameObject;
    }
    void OnEnable()
    {
        Vector2 toPlayer = (player.transform.position - transform.position).normalized;
        rb.AddForce(toPlayer * speed);
        StartCoroutine("Deactive");
        //stop = false;
    }
	// Use this for initialization
	void Update () {
        /*
      //Debug.Log("oi");
      if(transform.position == player.transform.position)
      {
          transform.position = parent.transform.position;
          stop = true;
          gameObject.SetActive(false);
      }

      if (stop)
      {
          Vector2 toPlayer = (player.transform.position - transform.position).normalized;
          rb.AddForce(toPlayer * speed);
          StartCoroutine("Deactive");
          stop = false;
      }*/
    }



    IEnumerator Deactive()
    {

        yield return new WaitForSeconds(2.8f);
        Reset();

    }

    public override void Reset()
    {
        transform.position = parent.transform.position;
        //stop = true;
        gameObject.SetActive(false);
    }

}
