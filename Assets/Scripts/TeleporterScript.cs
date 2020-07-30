using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterScript : MonoBehaviour
{
    public string var;
    public bool open;

    public int enemyCount;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Open", open);
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
            open = true;

        if (open == true)
        {
            anim.Play("OK");
        }
        else
            anim.Play("NOTOK");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && open == true)
        {
            SceneManager.LoadScene(var);
        }
    }

}
