
using UnityEngine;

public class GoPlayer : MonoBehaviour
{
    public Transform player;
    public int dist;
    public Transform Vasya;
    public float speedRotaton;
    public float speedMove;
    private Animator ch_animator;

    private void Start()
    {
        ch_animator = GetComponent<Animator>();
    }

    void Update()
    {
        float d = Vector3.Distance(transform.position, player.transform.position);
        if ((d < dist) && (d > 1.5))
        {
            Vector3 Rotation = player.position - Vasya.position;
            ch_animator.SetBool("walk", true);
            Vasya.rotation = Quaternion.Slerp(Vasya.rotation, Quaternion.LookRotation(Rotation), speedRotaton*Time.deltaTime);
            Vasya.transform.position += Vasya.forward * speedMove * Time.deltaTime;
            Vasya.transform.localPosition = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            Vasya.transform.localRotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
        }
        else
            ch_animator.SetBool("walk", false);

        if (d <= 1.5)
        {
            player.transform.GetComponent<Health>().AddDamage(2);
        }
    }
}
