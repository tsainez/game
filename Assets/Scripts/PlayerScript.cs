using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float maxSpeed = 4f;

    public float maxHP = 10;
    public float currentHP;

    public bool busy = false;

    public float invulTimer = 0f;

    SpriteRenderer spRender; 

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        spRender = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (busy == false)
        {
            float MoveX = Input.GetAxis("Horizontal");
            float MoveY = Input.GetAxis("Vertical");

             if ((invulTimer - (0.4) <= 0)) // don't like how this is pretty hard coded, especially when you can possibly change how long invulTimer is
                GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(MoveX * maxSpeed, MoveY * maxSpeed);

            if (Mathf.Abs(MoveX) > 0 || Mathf.Abs(MoveY) > 0)
            {
                anim.SetFloat("FaceX", MoveX);
                anim.SetFloat("FaceY", MoveY);
                anim.Play("Move");
            }

            else if (Mathf.Abs(MoveX) == 0 || Mathf.Abs(MoveY) == 0)
            {
                anim.Play("Idle");
            }
        }
        else if (busy == true)
        {
            StartCoroutine("FinishCheck");
        }
    }

    void Update()
    {
        anim.SetBool("Busy", busy); // just to make busy visible in Unity
        anim.SetFloat("InvulTimer", invulTimer);

        RoundFace();

        if (Input.GetKeyDown(KeyCode.Space) && busy == false)
            Attack();


        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(currentHP + "/" + maxHP);
        }        

        if (currentHP <= 0)
            Die();


        if (invulTimer  > 0)
        {
            invulTimer -= Time.deltaTime;
            spRender.color = new Color(1, 1, 1, 0.5f);
        }
        else if (invulTimer <= 0)
        {
            spRender.color = new Color(1, 1, 1, 1);
        }
    }

    IEnumerator FinishCheck()
    {//      if an animation has finished
        yield return new WaitForEndOfFrame();
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && currentHP > 0)
        {
            busy = false;
            yield return null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && currentHP > 0 && invulTimer <= 0) // taking damage from an enemy
        {
            invulTimer = 0.5f;
            // GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(-anim.GetFloat("FaceX") * 10, -anim.GetFloat("FaceY") * 10);
            currentHP--;
        }
    }

    void RoundFace()
    {// the math for rounding is really messy; it can possibly be optimized. Possibly set FaceX and FaceY to int rather than floats?
        if (anim.GetFloat("FaceX") > 0)
        {
            anim.SetFloat("FaceX", Mathf.Ceil(anim.GetFloat("FaceX")));
            anim.SetFloat("FaceY", 0);
        }
        else if (anim.GetFloat("FaceX") < 0)
        {
            anim.SetFloat("FaceX", Mathf.Floor(anim.GetFloat("FaceX")));
            anim.SetFloat("FaceY", 0);
        }
        if (anim.GetFloat("FaceY") > 0)
        {
            anim.SetFloat("FaceY", Mathf.Ceil(anim.GetFloat("FaceY")));
            anim.SetFloat("FaceX", 0);
        }
        else if (anim.GetFloat("FaceY") < 0)
        {
            anim.SetFloat("FaceY", Mathf.Floor(anim.GetFloat("FaceY")));
            anim.SetFloat("FaceX", 0);
        }
    }

    void Attack()
    {
        anim.Play("Attack");
        busy = true;
    }

    void Die()
    {
        busy = true;
        anim.Play("Dead");
        StartCoroutine("DeadPause");
    }

    IEnumerator DeadPause()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("DeadScreen");
    }

}

/* code that doesn't really work
    void BusyCheck()
    {
        if (busy == true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                busy = false;
            else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                BusyCheck();
        }
        overload
    }
    -------------------------------------------------------------------------------
    void Attack()
    {
        anim.Play("Attack");
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            busy = true;
        busy = false;
    }
    crashes unity
    -------------------------------------------------------------------------------
     IEnumerator BusyCheck()
    {
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
        {
            busy = true;
            yield return null;  
        }
    }
        void Attack()
    {
        busy = true;
        anim.Play("Attack");
        StartCoroutine("BusyCheck");
        busy = false;
    }

    ----------------------------------------------------------------------------------
weird thing i'm trying so I'm saving current script in case; it turns out it works so this code is pretty irrelevant
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChar : MonoBehaviour
{
    public float maxSpeed = 4f;
    public bool busy = false;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (busy == false)
        {
            float MoveX = Input.GetAxis("Horizontal");

        GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(MoveX * maxSpeed, GetComponent<Rigidbody2D>().velocity﻿.y);

        float MoveY = Input.GetAxis("Vertical");

        GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(GetComponent<Rigidbody2D>().velocity﻿.x, MoveY * maxSpeed);

            if (Mathf.Abs(MoveX) > 0 || Mathf.Abs(MoveY) > 0)
            {
            anim.SetFloat("FaceX", MoveX);
            anim.SetFloat("FaceY", MoveY);
            anim.Play("Move");
            }

            else if (Mathf.Abs(MoveX) == 0 || Mathf.Abs(MoveY) == 0)
            {
                anim.Play("Idle");
            }
        }
        else if (busy == true)
        {
            StartCoroutine("FinishCheck");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
        anim.SetBool("Busy", busy); // just to make busy visible in Unity
    }

    IEnumerator FinishCheck()
    {//      if an animation has finished
        yield return new WaitForEndOfFrame();
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            busy = false;
            yield return null;
        }
    }

    void Attack()
    {
        anim.Play("Attack");
        busy = true;
    }
}
-------------------------------------------------------------------------------
also SwingSword
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingSword : MonoBehaviour
{
    public bool busy = false;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (busy == false)
        {
            float MoveX = Input.GetAxis("Horizontal");
            float MoveY = Input.GetAxis("Vertical");
            anim.Play("Idle");
            if (Mathf.Abs(MoveX) > 0 || Mathf.Abs(MoveY) > 0)
            {
                anim.SetFloat("FaceX", MoveX);
                anim.SetFloat("FaceY", MoveY);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
        anim.SetBool("Busy", busy); // just to make busy visible in Unity
        if (busy == true)
        {
            StartCoroutine("FinishCheck");
        }
    }

    IEnumerator FinishCheck()
    {//      if an animation has finished
        yield return new WaitForEndOfFrame();
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            busy = false;
            yield return null;
        }   
    }

    void Attack()
    {
        anim.Play("Swing");
        busy = true;
    }
}


*/
