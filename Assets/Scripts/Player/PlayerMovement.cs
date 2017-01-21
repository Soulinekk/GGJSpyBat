using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 10f;
    public Rigidbody2D rbody;
    public float inputDelay = 0.1f;
    private Vector2 movement;
    public int dashAnimationDirection;
    public Animator animator;

	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	void Update () {

        #region
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            animator.SetBool("Dash", true);
        }
        else
        {
            animator.SetBool("Dash", false);
        }

        if (h != 0)
        {
            animator.SetBool("MovingH", true);
        }
        else
        {
            animator.SetBool("MovingH", false);
        }

        if (v != 0)
        {
            animator.SetBool("MovingV", true);
        }
        else
        {
            animator.SetBool("MovingV", false);
        }

        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);
        #endregion
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Movement(h, v);
        //Dash(h, v);

        if(Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift))
        {
            animator.SetBool("Dash", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            animator.SetBool("Dash", false);
        }
        animator.SetFloat("Horizontal", v);
        animator.SetFloat("Vertical", v);
    }

    void Movement(float h, float v)
    {
        if (Mathf.Abs(v) > inputDelay || Mathf.Abs(h) > inputDelay)
        {
            movement.Set(h, v);
            movement = movement.normalized * moveSpeed * Time.deltaTime;
            rbody.AddForce(movement);
        }
    }

    /*void Dash(float h, float v)
    {
        // Up
        if (Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.RightShift)))
        {
            if (v > 0 && h==0)
            {
                dashAnimationDirection = 1;
                Debug.Log("Up");
            }

            if (v < 0 && h == 0)
            {
                dashAnimationDirection = 2;
                Debug.Log("Down");
            }

            if (h < 0 && v == 0)
            {
                dashAnimationDirection = 3;
                Debug.Log("Left");
            }

            if (h > 0 && v == 0)
            {
                dashAnimationDirection = 4;
                Debug.Log("Right");
            }
        }
    }*/
}
