using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class AI_pers2 : MonoBehaviour
{
    
    Animator animator;
    NavMeshAgent agent;
    //CharacterController cs;
    public GameObject player;
    public float visible = 15f;
    public float angleV = 70f;
    public float dist;
    public Collider other;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //cs = GetComponent<CharacterController>();

    }

    void Update()
    {
        if ((transform.GetComponent<PHealth>().HP > 0) && (player.transform.GetComponent<PHealth>().HP > 0))
            AI();
        else
        {
            animator.speed = 1;
            animator.SetBool("Punching", false);
            animator.SetFloat("vertical", 0.0f);
        }

        if (transform.GetComponent<PHealth>().HP < 0)
        {
            animator.speed = 3;
            animator.SetTrigger("Dying1");
        }
    }

    public void AI()
    {
        /*if (transform.GetComponent<PHealth>().HP <= 0)
            agent.enabled = false;*/
        dist = Vector3.Distance(player.transform.position, transform.position); //Дистанция до игрока
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= 1.25f)
            {
                agent.enabled = false;
                if (transform.GetComponent<PHealth>().HP > 0)
                {
                    transform.LookAt(player.transform);
                    animator.speed = 2;
                    animator.SetBool("Punching", true);
                    transform.LookAt(player.transform);
                }
            }
            else if ((distance < visible) && (transform.GetComponent<PHealth>().HP > 0))
            {
                agent.enabled = true;
                Quaternion look = Quaternion.LookRotation(player.transform.position - transform.position);
                float angle = Quaternion.Angle(transform.rotation, look); //Смотрит в игрока
                if (angle < angleV)
                {
                    RaycastHit hit;
                    Ray ray = new Ray(transform.position + Vector3.up, player.transform.position - transform.position);
                    if (Physics.Raycast(ray, out hit, visible))
                    {
                        if (player.GetComponent<PHealth>().HP > 0)
                            if (hit.transform.gameObject == player) //если луч попадает в игрока то идти к игроку
                            {
                                if (player != null)
                                    agent.destination = player.transform.position;
                            }
                    }
                    Debug.DrawLine(ray.origin, hit.point, Color.red); //Отсвечивать красным
                }
            }
            else
            {
                agent.enabled = false;
                animator.SetFloat("vertical", 0.0f);
                //gameObject.GetComponent<Animator>().SetTrigger("idle");
            }
        }
        if ((agent.velocity.magnitude > 1.35f))
        {
            animator.speed = 1;
            agent.enabled = true;
            animator.SetBool("Punching", false);
            agent.SetDestination(player.transform.position);
            animator.SetFloat("vertical", 0.9f);
        }
        else
        {
            animator.SetFloat("vertical", 0.0f);
        }
    }
}

