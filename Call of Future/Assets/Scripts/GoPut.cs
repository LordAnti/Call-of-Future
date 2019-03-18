using UnityEngine;

public class GoPut : MonoBehaviour
{
    private Vector3 moveVector;
    private Vector3 thisVector;
    private Animator ch_animator;
    private CharacterController ch_controller;
    private float gravityForce;
    void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
        thisVector = ch_controller.transform.position;
    }

    void Update()
    {
        CharacterMove();
        GamingGravity();
    }

    private void CharacterMove()
    {
        moveVector = Vector3.zero;
        moveVector.y = gravityForce;


        if (moveVector.y != -1f)
            ch_animator.SetBool("fly", true);
        else
            ch_animator.SetBool("fly", false);
        ch_controller.Move(moveVector * Time.deltaTime);
    }

    private void GamingGravity()
    {
        if (!ch_controller.isGrounded)
            gravityForce -= 20f * Time.deltaTime;
        else
            gravityForce = -1f;
    }
}