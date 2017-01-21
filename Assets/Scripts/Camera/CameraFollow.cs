using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public bool shouldFollow = true;
    public Transform player;
    public float cameraDistance = 10f;
    public float dampTime = 0.15f;
    private Vector2 velocity = Vector2.zero;
    public float maxDampSpeed = 1000f;

	void Start () {
        
    }
	
	void Update () {
		
	}

    private void FixedUpdate()
    {
        CameraDamp();
    }

    void CameraDamp()
    {
        if (shouldFollow == true)
        {
            Vector2 destination = new Vector2(player.position.x + cameraDistance, player.position.y + cameraDistance);
            gameObject.transform.position = Vector2.SmoothDamp(gameObject.transform.position, destination, ref velocity, dampTime, maxDampSpeed, Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -50f);
        }
    }
}
