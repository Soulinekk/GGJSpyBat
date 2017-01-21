using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diode : Enemy {
    // Use this for initialization
    public GameObject bullet;
	void Awake () {
        base.Start();
        bullet = GetComponentInChildren<DiodeBullet>().gameObject;
        
        
        }
        // Debug.Log(radar.name);
    
	
	// Update is called once per frame
    void Start() { bullet.SetActive(false); }
	void Update () {
		
	}


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            Debug.Log("hi");
            InvokeRepeating("Shot", 0.5f, 3);
           // 
            //energyBar.value += 2f;
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            CancelInvoke("Shot");
        }
    }

    void Shot()
    {
        //Vector3 rot = transform.position-player.transform.position;
        bullet.SetActive(true);
        Quaternion rotation = Quaternion.LookRotation
             (player.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        bullet.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        //Vector2 toPlayer = (player.transform.position - transform.position).normalized;
        // bullet.GetComponent<DiodeBullet>o();
        //bullet.transform.LookAt(player.transform);
    }
}
