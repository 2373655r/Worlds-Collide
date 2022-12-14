using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRight : MonoBehaviour {

    public CharacterController2D controller;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    public Animator animator;

    // Update is called once per frame
    void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal_P2") * runSpeed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump_P2"))
        {
            jump = true;
        }
	}

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
