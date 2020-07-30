using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public float maxSpeed = 2f;

    public float maxHP = 5;
    public float currentHP = 5;

    public Transform playerTransform;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;

        //follow player
        if (Mathf.Round(transform.position.x) < Mathf.Round(playerTransform.position.x))
        {
            GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(maxSpeed, GetComponent<Rigidbody2D>().velocity﻿.y);
            anim.SetFloat("FaceX", 1);
        }
        else if (Mathf.Round(transform.position.x) > Mathf.Round(playerTransform.position.x))
        {
            GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(-maxSpeed, GetComponent<Rigidbody2D>().velocity﻿.y);
            anim.SetFloat("FaceX", -1);
        }
        else if (Mathf.Round(transform.position.x) == Mathf.Round(playerTransform.position.x))
        {
            GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(GetComponent<Rigidbody2D>().velocity﻿.x, GetComponent<Rigidbody2D>().velocity﻿.y);
            anim.SetFloat("FaceX", 0);
        }

        if (Mathf.Round(transform.position.y) < Mathf.Round(playerTransform.position.y))
        {
            GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(GetComponent<Rigidbody2D>().velocity﻿.x, maxSpeed);
            anim.SetFloat("FaceY", 1);
        }
        else if (Mathf.Round(transform.position.y) > Mathf.Round(playerTransform.position.y))
        {
            GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(GetComponent<Rigidbody2D>().velocity﻿.x, -maxSpeed);
            anim.SetFloat("FaceY", -1);
        }
        else if (Mathf.Round(transform.position.y) == Mathf.Round(playerTransform.position.y))
        {
            GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(GetComponent<Rigidbody2D>().velocity﻿.x, GetComponent<Rigidbody2D>().velocity﻿.y);
            anim.SetFloat("FaceY", 0);
        }
    }

    void Update()
    {
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerWeapon")) // taking damage from a player weapon
        {
            GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(-anim.GetFloat("FaceX") * 30, -anim.GetFloat("FaceY") * 30);
            currentHP--;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") // dealing damage to the player
        {
            GameObject player = GameObject.Find("Character");
            player.GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(anim.GetFloat("FaceX") * 30, anim.GetFloat("FaceY") * 30);
        }
    }

    void Die()
    {
        anim.Play("Dead");
        Destroy(gameObject);
    }
}


/*old code
void FixedUpdate()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;

        //follow player
        if (Mathf.Round(transform.position.x) < Mathf.Round(playerTransform.position.x))
        {
            GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(maxSpeed, GetComponent<Rigidbody2D>().velocity﻿.y);
            anim.SetFloat("FaceX", 1);
        }
        else if (Mathf.Round(transform.position.x) > Mathf.Round(playerTransform.position.x))
        {
            GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(-maxSpeed, GetComponent<Rigidbody2D>().velocity﻿.y);
            anim.SetFloat("FaceX", -1);
        }
        else if (Mathf.Round(transform.position.x) == Mathf.Round(playerTransform.position.x))
        {
            GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(GetComponent<Rigidbody2D>().velocity﻿.x, GetComponent<Rigidbody2D>().velocity﻿.y);
            anim.SetFloat("FaceX", 0);
        }

        if (Mathf.Round(transform.position.y) < Mathf.Round(playerTransform.position.y))
        {
            GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(GetComponent<Rigidbody2D>().velocity﻿.x, maxSpeed);
            anim.SetFloat("FaceY", 1);
        }
        else if (Mathf.Round(transform.position.y) > Mathf.Round(playerTransform.position.y))
        {
            GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(GetComponent<Rigidbody2D>().velocity﻿.x, -maxSpeed);
            anim.SetFloat("FaceY", -1);
        }
        else if (Mathf.Round(transform.position.y) == Mathf.Round(playerTransform.position.y))
        {
            GetComponent<Rigidbody2D>().velocity﻿ = new Vector2(GetComponent<Rigidbody2D>().velocity﻿.x, GetComponent<Rigidbody2D>().velocity﻿.y);
            anim.SetFloat("FaceY", 0);
        }
    }
*/