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
            if(Input.GetKeyDown(KeyCode.Space))
            {
                gotHit = true;
            }
            
            yield return null; 
            
        }
        gotHit = false;
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
        while (Mathf.Abs(transform.position.x - player.transform.position.x)>0.2f)
        {
            transform.position = new Vector3(Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * angrySpeed).x, transform.position.y, transform.position.z);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f); //buff Anim --> into Jump
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        rb.AddForce(new Vector2(0f, jumpPower));
        Physics2D.IgnoreLayerCollision(8, 10);
        //disable collision with walls
        //8-walls,10-bulbboss
       // yield return new WaitForSeconds(0.5f);
        while (rb.velocity.y > -1)
        {
            yield return null;
        }
        //enable collisions with walls
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Walls"), LayerMask.NameToLayer("BulbBoss"), false);
        
        yield return new WaitForSeconds(1f); //bulb cooling down
        if (transform.position.y > -4) //kill hight
        {
            //yield return new WaitForSeconds(1.5f);
            state = States.Die;
            StartCoroutine(StateDie());
        }
        else
        {
            yield return new WaitForSeconds(2f);
            state = States.Hot;
            StartCoroutine(StateHot());
        }

    }

    private IEnumerator StateDie()
    {
        Debug.Log("Die");
        heatWave.SetActive(true);
        InvokeRepeating("HeatSpawn", 0f, 0.5f);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        while (rb.velocity.y>-2)
        {
            transform.position = new Vector3(Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * angrySpeed).x, transform.position.y, transform.position.z);
            
            
            yield return null;
        }
        state = States.Dead;
        Debug.Log("DEAD");
        CancelInvoke("HeatSpawn");
        heatWave.SetActive(false);
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
