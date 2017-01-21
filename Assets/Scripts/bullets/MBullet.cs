﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MBullet : MonoBehaviour {

    protected Rigidbody2D rb;
    public float bulletSpeed=100;
    protected Vector2 startingPos;
    public virtual void Reset(){
        transform.localPosition=startingPos;
        gameObject.SetActive(false);
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPos = transform.localPosition;
    }
    protected virtual void OnEnable()
    {
        rb.AddForce(transform.up * bulletSpeed);
        
    }
    protected virtual IEnumerator Deactive() { Reset(); yield return null; }
}
