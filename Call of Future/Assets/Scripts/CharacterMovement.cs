using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public Transform CameraTransform;
    public CharacterStatus characterStatus;

    public Animator anim;

    public float vertical;
    public float horizontal;
    public float moveAmount;
    public float rotationSpeed;

    public Vector3 rotationDirection;
    public Vector3 moveDirection;

    //Для движения
    //Основные параметры
    public float speedMove; //Скорость персонажа
    public float speedRun;
    public float jumpPower; // Сила прыжка
    public float speed;

    //Параметры геймплея для персонажа
    private float gravityForce; //Гравитация персонажа
    private Vector3 moveVector; //Направление движения персонажа

    //Ссылки на компоненты
    private CharacterController ch_controller;
    private MobileController mContr;

    //Для падения и урона
    public float lastPositionY = 0f;
    public float fallDistance = 0f;

    //Оружие
    public GameObject pistol;
    public GameObject aim;

    private void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        mContr = GameObject.FindGameObjectWithTag("Jostik").GetComponent<MobileController>();
    }

    private void Update()
    {
        RotationNormal();
        GamingGravity();
        MoveUpdate();
        RotationNormal();

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (characterStatus.isPistol == true)
                characterStatus.isPistol = false;
            else
                characterStatus.isPistol = true;
        }

        if (characterStatus.isPistol == true)
        {
            anim.SetBool("isPistol", true);
            pistol.SetActive(true);
            aim.SetActive(true);
        }
        else
        {
            anim.SetBool("isPistol", false);
            pistol.SetActive(false);
            aim.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (gravityForce == -1f)        
            anim.applyRootMotion = true;
    }

    public void MoveUpdate()
    {
        //Для получения направления камеры
        Vector3 moveDir = CameraTransform.forward;
        moveDir += CameraTransform.right;
        moveDir.Normalize();
        moveDirection = moveDir;
        rotationDirection = CameraTransform.forward;

        //Движение персонажа
        moveVector = Vector3.zero;

        if (characterStatus.isSprint)
            speed = speedRun;
        else
            speed = speedMove;

        /*moveVector.x = mContr.Horizontal() * speed;
        moveVector.z = mContr.Vertical() * speed;

        moveVector = transform.TransformDirection(moveVector);*/

        moveVector.y = gravityForce;
        if (moveVector.y != -1f)
        {
            anim.SetBool("isFly", true);
            moveVector.x = mContr.Horizontal() * speed;
            moveVector.z = mContr.Vertical() * speed;

            moveVector = transform.TransformDirection(moveVector);
        }
        else
            anim.SetBool("isFly", false);
            
        ch_controller.Move(moveVector * Time.deltaTime);
            

        //Анимация движения
        if (characterStatus.isSprint == false)
        {
            vertical = mContr.Vertical();
            horizontal = mContr.Horizontal();
        }
        else
        {
            vertical = mContr.Vertical() * 2;
            horizontal = mContr.Horizontal() * 2;
        }
        //moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));

        anim.SetFloat("vertical", vertical);
        anim.SetFloat("horizontal", horizontal);

        //Для урона
        lastPositionY = ch_controller.transform.position.y;
        if (fallDistance < lastPositionY)
            fallDistance = lastPositionY;


        //высота при которой отнимаются жизни 
        if (ch_controller.isGrounded)
        {
            fallDistance = Math.Abs(fallDistance - lastPositionY);
            if (fallDistance > 2.5)
            {
                GetComponent<PHealth>().AddDamage((fallDistance - 1) * (fallDistance - 1) * ((fallDistance - 1) / 3), "fall", "fall");
                fallDistance = 0;
                lastPositionY = 0;
            }
        }

        if (fallDistance <= 2.5 && ch_controller.isGrounded)
        {
            fallDistance = 0;
            lastPositionY = 0;
        }
    }

    //Вращение персонажа
    public void RotationNormal()
    {
        rotationDirection = moveDirection;

        Vector3 targetDir = rotationDirection;
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
            targetDir = transform.forward;

        Quaternion lookDir = Quaternion.LookRotation(targetDir);
        Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookDir, 0.4f);
        transform.rotation = targetRot;
        transform.Rotate(transform.TransformDirection(Vector3.up), -15);
    }

    //Метод гравитации
    private void GamingGravity()
    {
        if (!ch_controller.isGrounded)
        {
            anim.applyRootMotion = false;
            gravityForce -= 20f * Time.deltaTime;
        }   
        else
            gravityForce = -1f;

        if ((Input.GetKeyDown(KeyCode.Space) || characterStatus.isJump == true) && ch_controller.isGrounded)
        {
            gravityForce = jumpPower;
            characterStatus.isJump = false;
        }
    }
}