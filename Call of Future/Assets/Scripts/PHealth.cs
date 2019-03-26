using UnityEngine;
using UnityEngine.UI;

public class PHealth : MonoBehaviour
{
    public float HP;
    public float AP;
    public Slider slHealth;
    public Slider slArmor;
    Animator anim;
    public bool Player;

    void Start()
    {
        anim = GetComponent<Animator>();
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
                HP -= AP;
                AP = 0;
            }
        }

        if (slHealth != null)
            slHealth.value = HP;
        if (slArmor != null)
            slArmor.value = AP;

        if (HP <= 0)
        {
            anim.SetBool("Dyiling1", true);
        }

    }
}
