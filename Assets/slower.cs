using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slower : MonoBehaviour
{
    public float slowling = 0.5f;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Actor"))
        {
            if (other.GetComponent<Unit1>())
            {
                Debug.Log("slow");
                other.GetComponent<Unit1>().speed *= slowling;
                other.GetComponent<Rigidbody2D>().velocity = other.GetComponent<Rigidbody2D>().velocity.normalized * other.GetComponent<Unit1>().speed;

            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Actor"))
        {
            if (other.GetComponent<Unit1>())
            {
                other.GetComponent<Unit1>().speed /= slowling;
                other.GetComponent<Rigidbody2D>().velocity = other.GetComponent<Rigidbody2D>().velocity.normalized * other.GetComponent<Unit1>().speed;

            }
        }
    }
}
