using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiodeBullet : MBullet {

   
    GameObject player;
    public GameObject parent;
    bool stop;
    protected override void Awake()
    {
        base.Awake();
       // bulletSpeed = 10;
        stop = true;
        player = GameObject.Find("Player");
        parent = transform.parent.gameObject;
    }
    protected override void OnEnable()
    {
        Vector2 toPlayer = (player.transform.position - transform.position).normalized;
        rb.AddForce(toPlayer * bulletSpeed);
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



    protected override IEnumerator Deactive()
    {

        yield return new WaitForSeconds(2.8f);
        Reset();

    }

}
