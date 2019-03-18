using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 30;
    public string[] targetTags = { "Vasya"};

    void OnTriggerEnter(Collider coll)
    {
        foreach (string currentTag in targetTags)
        {
            if (currentTag == coll.transform.tag)
            {
                coll.transform.GetComponent<Health>().AddDamage(damage);
            }
        }
    }
}