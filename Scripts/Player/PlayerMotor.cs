using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController playerCotroller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool crouching;
    private float crouchTimer;
    private bool lerpCrouch;
    private bool sprinting;


    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1f;
    // Start is called before the first frame update
    void Start()
    {
        playerCotroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = playerCotroller.isGrounded;
        if(lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                playerCotroller.height = Mathf.Lerp(playerCotroller.height, 1, p);
            else
                playerCotroller.height = Mathf.Lerp(playerCotroller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0;
            }
        }

    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        playerCotroller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;

        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        playerCotroller.Move(playerVelocity * Time.deltaTime);
        //Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Couch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if(sprinting)
        {
            speed = 8f;
        }
        else
        {
            speed = 5;
        }
    }
}
