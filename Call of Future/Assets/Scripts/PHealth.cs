using UnityEngine;
using UnityEngine.UI;

public class PHealth : MonoBehaviour
{
    public float HP = 100;
    public bool Player;
    public Slider slider;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AddDamage(float damage)
    {
        HP -= damage;
        if (Player)
            slider.value = HP;

        if (HP <= 0)
        {
            if (Player == false)
            {
                anim.SetTrigger("Dying1");
                //Destroy(gameObject);
            } 
            else
            {
                anim.SetTrigger("Dying");
                transform.GetComponent<IKController>().ikActive = false;
                //SceneManager.LoadScene("MainMenu");
            }   
        }
    }
}
