using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 10f;
    public Rigidbody2D rbody;
    public float inputDelay = 0.1f;
    private Vector2 movement;
    public Animator animator;
    public bool canUseDash = true;
    public float dashSpeed = 30f;
    float tempSpeedConteiner;
    public float thrust;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tempSpeedConteiner = moveSpeed;
    }

    void Update()
    {
        #region Dash
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);

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

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && canUseDash)
        {
            StartCoroutine(DashCooldown());
            animator.SetTrigger("Dash");
            






            /*#region if's
                if (h != 0)
                {
                    animator.SetBool("MovingH", true);
                    animator.SetTrigger("Dash");
                    StartCoroutine(DashCooldown());
                    //StartCoroutine(Dash());
            }
                else
                {
                    animator.SetBool("MovingH", false);
                    animator.SetTrigger("Dash");
                    StartCoroutine(DashCooldown());
                    //StartCoroutine(Dash());
            }

                if (v != 0)
                {
                    animator.SetBool("MovingV", true);
                    animator.SetTrigger("Dash");
                    StartCoroutine(DashCooldown());
                    //StartCoroutine(Dash());
            }
                else
                {
                    animator.SetBool("MovingV", false);
                    animator.SetTrigger("Dash");
                    StartCoroutine(DashCooldown());
                    //StartCoroutine(Dash());
            }
            #endregion*/
        }
        #endregion
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Movement(h, v);
    }

    void ForceDash()
    {
        rbody.AddForce(transform.up*thrust);
    }

    void ForceDash2()
    {
        rbody.AddForce(-transform.up * thrust);
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

    IEnumerator DashCooldown()
    {
        canUseDash = false;
        yield return new WaitForSeconds(2);
        canUseDash = true;
    }
}
