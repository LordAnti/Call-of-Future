using UnityEngine;

public class Strike : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PHealth>() != null)
        {
            other.transform.GetComponent<PHealth>().AddDamage(3, "bullet", "bullet");
            print("Получен удар");
            print(other.transform.name);
        }
    }
}
