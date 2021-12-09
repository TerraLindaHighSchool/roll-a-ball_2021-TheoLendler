﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject titleText;
    private float jumpForce = 500;
    public bool inAir = false;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "GroundObject")
            inAir = false;
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "GroundObject")
            inAir = true;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 13)
        {
            winTextObject.SetActive(true);
        }
    }
        


    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        if (movementX > 0 || movementY > 0)
        {
            titleText.SetActive(false);
        }

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }

    void OnJump()
    {
        titleText.SetActive(false);
         
        if (!inAir)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
    }
}
