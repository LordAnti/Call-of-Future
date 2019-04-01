using UnityEngine;

public class CheckSlotWeapon : MonoBehaviour
{
    public CharacterStatus cs;
    void Update()
    {
        print(transform.childCount);
        if (transform.childCount > 0)
            cs.isPistol = true;
        else
            cs.isPistol = false;
    }
}
