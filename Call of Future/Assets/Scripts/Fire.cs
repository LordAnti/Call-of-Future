using UnityEngine;
using UnityEngine.EventSystems;

public class Fire : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public CharacterStatus cs;
    public virtual void OnPointerDown(PointerEventData ped)
    {
        if (cs.isPistol == true)
        {
            cs.isFire = true;
        }
        else
            OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        cs.isFire = false;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        if (cs.isPistol == false)
            cs.isFire = true;
    }
}