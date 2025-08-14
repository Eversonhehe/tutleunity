using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headbob : MonoBehaviour
{
    public Animator camAnim;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            camAnim.ResetTrigger("idle");
            camAnim.SetTrigger("walk");
           
        }
        else
        {
            camAnim.ResetTrigger("walk");
            camAnim.SetTrigger("idle");
        }
    }
}