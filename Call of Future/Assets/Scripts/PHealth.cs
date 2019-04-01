using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PHealth : MonoBehaviour
{
    public float HP;
    public float AP;
    public Slider slHealth;
    public Slider slArmor;
    Animator anim;
    public bool Player;
    int i = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (slHealth != null)
            slHealth.value = HP;
        if (slArmor != null)
            slArmor.value = AP;
    }

    public void AddDamage(float damage, string typeDamage, string typeFall)
    {
        if (typeDamage == "fall")
        {
            HP -= damage;
        }
        else
        if (typeDamage == "bullet")
        {
            if (AP > 0)
                AP -= damage;
            else
                HP -= damage;

            if (AP < 0)
            {
                HP += AP;
                AP = 0;
            }
        }

        if (slHealth != null)
            slHealth.value = HP;
        if (slArmor != null)
            slArmor.value = AP;

        if (HP <= 0)
        {
            anim.speed = 3;
            if (Player == true)
            {
                anim.SetTrigger("Dying");
                transform.GetComponent<IKController>().ikActive = false;
                InvokeRepeating("timerUpdate", 2f, 1);
                //Destroy(transform.GetComponent<CharacterController>());
            }
            else
            {
                anim.speed = 3;
                anim.SetTrigger("Dying1");
            }
        }
    }

    public void timerUpdate()
    {
        i++;
        if (i > 10)
            SceneManager.LoadScene("MainMenu");
    }
}
