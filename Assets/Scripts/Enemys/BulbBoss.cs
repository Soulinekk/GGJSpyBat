﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulbBoss : Enemy {
    
    public Animator anim;

    public enum States { Intro, Hot, Angry, Die,Dead }
    States state;
    CameraShake camShake;
    System.Random rnd = new System.Random();
    public bool gotHit = false;
    bool shatter = false;
    private bool introend = false;
    public void StartFight() { introend = true; }
    public float angrySpeed;
    public float jumpPower;

    public GameObject heatBullet;
    //List<GameObject> heatBeams=new List<GameObject>();
    GameObject luckyShot;

    public AudioSource initSound;
    public AudioSource angrySound;
   // public AudioSource DyingSound;
    public AudioSource ShotingSound;
    public AudioSource jumpSound;
    public AudioSource bg;
    //public AudioSource initSound;

    protected  override void Start()
    {
        base.Start();
         
        luckyShot = GameObject.Find("luckyShot");
        luckyShot.SetActive(false);
        camShake=GameObject.Find("Main Camera").GetComponent<CameraShake>();
        state = States.Intro;
        StartCoroutine(StateIntro());
        

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
        
        while (!introend)
        {
            yield return null;
        }
        camShake.ShakeCamera(0.3f, 0.1f);
        initSound.Play();
        anim.SetTrigger("Activate");
        yield return new WaitForSeconds(5f);  //PlayIntro
        state = States.Angry;
        StartCoroutine(StateAngry());
    }

    private IEnumerator StateHot()
    {
        Debug.Log("Hot");
        anim.SetTrigger("Attack");
       // heatWave.SetActive(true);
        InvokeRepeating("HeatSpawn", 0f, 0.3f);
        InvokeRepeating("PlayShotingSound", 0f, 14f);
        while (!gotHit)
        {
            
            yield return null; 
            
        }
        
        camShake.ShakeCamera(0.2f, 0.2f);
        angrySound.Play();
        yield return new WaitForSeconds(1f); //Play Transition Anim 
        gotHit = false;
        CancelInvoke("HeatSpawn");
        CancelInvoke("PlayShotingSound");
        if (transform.localPosition.y < 38)
        {
            state = States.Angry;
            StartCoroutine(StateAngry());
        }
        else
        {
            state = States.Die;
            StartCoroutine(StateDie());
        }
    }

    private IEnumerator StateAngry()
    {
        Debug.Log("Angry");
        yield return new WaitForSeconds(1f);
        while (Mathf.Abs(transform.position.x - player.transform.position.x)>0.2f)
        {
            transform.position = new Vector3(Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * angrySpeed).x, transform.position.y, transform.position.z);
            yield return null;
        }
        
        anim.SetTrigger("Jump");
        yield return new WaitForSeconds(1.5f); //buff Anim --> into Jump
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (transform.position.y < player.transform.position.y)
        {
            jumpSound.Play();
            rb.AddForce(new Vector2(0f, jumpPower));
            Physics2D.IgnoreLayerCollision(10, 11);
            while (rb.velocity.y > -1)
            {
                yield return null;
            }
            yield return null;
        }
        else
        {
            if (transform.position.y > -2)
            {
                Physics2D.IgnoreLayerCollision(10, 11);
                yield return new WaitForSeconds(0.5f);
            }
        }
        //BullBoss-10 , BGWall-11pi
        //disable collision with walls
        //8-walls,10-bulbboss
       // yield return new WaitForSeconds(0.5f);
        
        //enable collisions with walls
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("BGWall"), LayerMask.NameToLayer("BulbBoss"), false); //8,11
        
        yield return new WaitForSeconds(2f); //bulb cooling down

            //yield return new WaitForSeconds(2f);
            state = States.Hot;
            StartCoroutine(StateHot());
        

    }

    private IEnumerator StateDie()
    {
        Debug.Log("Die");
      //  heatWave.SetActive(true);
        InvokeRepeating("HeatSpawn", 0f, 0.05f);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        while (rb.velocity.y>-1)
        {
            
            transform.position = new Vector3(Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * angrySpeed).x, transform.position.y, transform.position.z);
            
            
            yield return null;
        }
        jumpSound.Stop();
        initSound.Stop();
        angrySound.Stop();
        bg.Stop();
        // public AudioSource DyingSound;
        ShotingSound.Stop();
        Destroy(player.GetComponent<Collider2D>());
        GameObject.Find("Main Camera").GetComponent<Animator>().ResetTrigger("CameraMaskOff");
        yield return new WaitForSeconds(0.3f);
    Time.timeScale = 0.2f;
        state = States.Dead;
        Debug.Log("DEAD");
        CancelInvoke("HeatSpawn");

        yield return new WaitForSeconds(1f);
        Time.timeScale = 1;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        // heatWave.SetActive(false);
        shatter = true;
        
    }
    #endregion

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "wall" && shatter)
        {
            state = States.Dead;
        }
        if (col.gameObject.tag == "BGWall")
        {
            if (state==States.Angry) {
                camShake.ShakeCamera(0.1f, 0.1f);
            }
        }
    }
    

    void HeatSpawn()
    {
        
        //GameObject g;
        if (rnd.Next(20) != 9)
        {
            // g= heatBeams[rnd.Next(heatBeams.Count)];
            Instantiate(heatBullet, transform.position, Quaternion.Euler(new Vector3(0f, 0f, rnd.Next(-160,160))), transform);
           // g = luckyShot;
        }
        else //its ur lucky day
        {
            luckyShot.SetActive(true);
            //g = luckyShot;

        }
       // g.SetActive(false);
       // g.SetActive(true);
    }
    void PlayShotingSound() {
        ShotingSound.Play();
    }
}
