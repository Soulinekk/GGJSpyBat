using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbBoss : Enemy {

    public enum States { Intro, Hot, Angry, Die,Dead }
    States state;

    System.Random rnd = new System.Random();
    bool gotHit = false;
    bool shatter = false;
    public float angrySpeed;
    public float jumpPower;

    GameObject heatWave;
    List<GameObject> heatBeams=new List<GameObject>();

    protected  override void Start()
    {
        base.Start();
        state = States.Intro;
        StartCoroutine(StateIntro());
// HeatWave
        foreach(Transform o in GetComponentsInChildren<Transform>())
        {
            if (o.gameObject.name == "HeatWave")
            {
                heatWave = o.gameObject;
                break;
            }
        }
        foreach(MBullet b in heatWave.GetComponentsInChildren<MBullet>())
        {
            heatBeams.Add(b.gameObject);
            b.gameObject.SetActive(false);
           
        }
        heatWave.SetActive(false);
//
    }


    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case States.Intro:
                
                break;
            case States.Hot:
                
                break;
            case States.Angry:

                break;
            case States.Die:

                break;
            case States.Dead:

                break;
        }

    }

    public void setState(States s)
    {
        state = s;
    }

    #region states coroutines
    private IEnumerator StateIntro()
    {
        Debug.Log("Intro");
        yield return new WaitForSeconds(1f);  //PlayIntro
        state = States.Hot;
        StartCoroutine(StateHot());
    }

    private IEnumerator StateHot()
    {
        Debug.Log("Hot");
        heatWave.SetActive(true);
        InvokeRepeating("HeatSpawn", 0f, 0.5f);
        while (!gotHit)
        {
            //Initialize HeatWave
            
            
            
            yield return null; 
            
        }
        foreach(GameObject g in heatBeams)
        {
            g.SetActive(false);
        }
        heatWave.SetActive(false);
        yield return new WaitForSeconds(1f); //Play Transition Anim 
        state = States.Angry;
        StartCoroutine(StateAngry());
    }

    private IEnumerator StateAngry()
    {
        Debug.Log("Angry");
        while (transform.position.y != player.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * angrySpeed).y, transform.position.z);
            yield return null;
        }
        yield return new WaitForSeconds(1f); //buff Anim --> into Jump
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0f, jumpPower));
        //disable collision with walls
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Walls"), LayerMask.NameToLayer("BulbBoss"),true);//8-walls,12-bulbboss
        while (rb.velocity.y > 0)
            yield return null;
        //enable collisions with walls
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Walls"), LayerMask.NameToLayer("BulbBoss"), false);
        yield return new WaitForSeconds(1f); //bulb cooling down
        if(transform.position.y>100000) //kill hight
        {
            state = States.Die;
            StartCoroutine(StateDie());
        }
        else
        {
            state = States.Hot;
            StartCoroutine(StateHot());
        }
        
    }

    private IEnumerator StateDie()
    {
        Debug.Log("Die");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        while (rb.velocity.y>-0.2)
        {
            transform.position = new Vector3(transform.position.x, Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * angrySpeed).y, transform.position.z);
            yield return null;
        }
        shatter = true;
        
    }
    #endregion

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall" && shatter)
        {
            state = States.Dead;
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "bossKiller")
        {
            Debug.Log("shjeeeet");
            gotHit = true;
        }

    }

    void HeatSpawn()
    {


       GameObject g = heatBeams[rnd.Next(heatBeams.Count)];
        g.SetActive(false);
        g.SetActive(true);
    }
}
