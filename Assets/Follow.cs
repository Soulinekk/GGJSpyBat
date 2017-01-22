using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    Transform cam;

	// Use this for initialization
	void Start () {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 v = new Vector3(cam.position.x, cam.position.y, transform.position.z);
        gameObject.transform.position = v;
	}
}
