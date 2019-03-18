using UnityEngine;
using UnityEngine.UI;

public class PArmor : MonoBehaviour
{
    public float AP = 100;
    public bool Player;
    public Slider slider;

    public void AddDamage(float damage)
    {
        AP -= damage;
        if (slider != null)
            slider.value = AP;

        if (AP <= 0)
        {
            if (AP < 0)
                transform.GetComponent<PHealth>().AddDamage(AP);
            if (Player == false)
            { }
            else
            {
                
                //SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
