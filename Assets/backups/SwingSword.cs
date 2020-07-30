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
            anim.Play("Away");
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
