using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MBullet : MonoBehaviour {

    protected Rigidbody2D rb;
    public float bulletSpeed=1;
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
    protected virtual IEnumerator Deactive() { yield return new WaitForSeconds(15f); Reset(); }
}
