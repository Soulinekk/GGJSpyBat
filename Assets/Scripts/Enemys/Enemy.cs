using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject player;
	// Use this for initialization
	protected virtual void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	protected void Update () {
		
	}
}
