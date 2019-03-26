using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Transform player;
    public int dist;
    public float speedRotaton;
    public float speedMove;
    private Animator ch_animator;

    private void Start()
    {
        ch_animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (transform.GetComponent<PHealth>().HP > 0)
        {
            float d = Vector3.Distance(transform.position, player.transform.position);
            if ((d < dist) && (d > 1.5))
            {
                Vector3 Rotation = player.position - transform.position;
                ch_animator.SetFloat("vertical", 0.9f);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Rotation), speedRotaton * Time.deltaTime);
                transform.transform.position += transform.forward * speedMove * Time.deltaTime;
                transform.transform.localPosition = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
                transform.transform.localRotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
            }
            else
            {
                ch_animator.SetFloat("vertical", 0.0f);
            }

            if ((d <= 1.5) && (player.transform.GetComponent<PHealth>().HP > 0))
            {
                player.transform.GetComponent<PHealth>().AddDamage(1, "bullet", "bullet");
            }
        }
        else
        {
            ch_animator.SetFloat("vertical", 0.0f);
            Destroy(transform.GetComponent<Zombie>());
        }
    }
}
