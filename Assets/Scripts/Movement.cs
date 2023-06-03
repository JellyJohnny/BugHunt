using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Movement : MonoBehaviour
{
    CharacterController controller;

    float hor;
    float ver;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jump = 1f;

    Vector3 velocity;
    bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();  
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        float x = hor;
        float z = ver;

        //Vector3 move = transform.right * x + transform.forward * z;
        Vector3 move = Camera.main.transform.right * x + Camera.main.transform.forward * z;

        if (controller.enabled)
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown("space") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        if (controller.enabled)
        {
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
