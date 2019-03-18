using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float HP = 100;
    public bool Player;

    public void AddDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            if (!Player)
                Destroy(gameObject);
            else
                SceneManager.LoadScene("MainMenu");
        }
    }
}