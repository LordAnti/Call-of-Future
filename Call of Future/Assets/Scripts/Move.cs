using System;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    //Основные параметры
    public float speedMove; //Скорость персонажа
    public float speedRun;
    public float jumpPower; // Сила прыжка
    public float speed;
    public float lastPositionY = 0f;
    public float fallDistance = 0f;
    public Slider slider;

    //Параметры геймплея для персонажа
    private float gravityForce; //Гравитация персонажа
    private Vector3 moveVector; //Направление движения персонажа

    //Ссылки на компоненты
    private CharacterController ch_controller;
    //private Animator ch_animator;
    private MobileController mContr;
    //private MobileRun mRun;
    //private MobileJump mJump;
    public Transform BodyPlayer;
    public Transform Player;
    private int i = 0;

    private void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        //ch_animator = GetComponent<Animator>();
        mContr = GameObject.FindGameObjectWithTag("Jostik").GetComponent<MobileController>();
        //mRun = GameObject.FindGameObjectWithTag("Run").GetComponent<MobileRun>();
        //mJump = GameObject.FindGameObjectWithTag("Jump").GetComponent<MobileJump>();
    }

    private void Update()
    {
        CharacterMove();
        GamingGravity();
    }

    //Метод перемещения персонажа
    private void CharacterMove()
    {
        moveVector = Vector3.zero;

        //if (mRun.run)
       //     speed = speedRun;
       // else
            speed = speedMove;

        moveVector.x = mContr.Horizontal() * speed;
        moveVector.z = mContr.Vertical() * speed;

        moveVector = transform.TransformDirection(moveVector);
        if ((moveVector.x != 0) || (moveVector.z != 0))
        {
            Vector3 direct = Vector3.RotateTowards(BodyPlayer.forward, moveVector, 1, 0.0f);
            BodyPlayer.rotation = Quaternion.LookRotation(direct);
        }
        else
            BodyPlayer.rotation = Player.rotation;

        /*if ((moveVector.x != 0) || (moveVector.z != 0) && !ch_animator.GetBool("fly"))
        {
            if (mRun.run)
                ch_animator.SetBool("run", true);
            else
            {
                ch_animator.SetBool("walk", true);
                ch_animator.SetBool("run", false);
            }
        }
        else
        {
            ch_animator.SetBool("run", false);
            ch_animator.SetBool("walk", false);
        }*/
            
        moveVector.y = gravityForce;
        /*if (moveVector.y != -1f)
            ch_animator.SetBool("fly", true);
        else
            ch_animator.SetBool("fly", false);
        ch_controller.Move(moveVector * Time.deltaTime);*/

        lastPositionY = ch_controller.transform.position.y;
        if (fallDistance < lastPositionY)
            fallDistance = lastPositionY;

        
        //высота при которой отнимаются жизни 
        if (ch_controller.isGrounded)
        {
            fallDistance = Math.Abs(fallDistance - lastPositionY);
            if (fallDistance >= 5)
            {
                Player.transform.GetComponent<Health>().AddDamage(fallDistance - 4);
                slider.value -= fallDistance - 4;
                ApplyNormal();
            }
        }

        if (fallDistance <= 5 && ch_controller.isGrounded)
            ApplyNormal();

        slider.value = Player.transform.GetComponent<Health>().HP;
    }

    //Метод гравитации
    private void GamingGravity()
    {
        if (!ch_controller.isGrounded)
        {
            gravityForce -= 20f * Time.deltaTime;
           // mJump.jump = false;
        }
        else
            gravityForce = -1f;

        //if ((Input.GetKeyDown(KeyCode.Space) || mJump.jump) && ch_controller.isGrounded)
        if (Input.GetKeyDown(KeyCode.Space) && ch_controller.isGrounded)
        {
            gravityForce = jumpPower;
          //  mJump.jump = false; 
        }  
    }

    void ApplyNormal()
    {
        fallDistance = 0;
        lastPositionY = 0;
    }
}
