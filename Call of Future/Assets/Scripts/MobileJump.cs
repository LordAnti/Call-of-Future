using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJump : MonoBehaviour, IPointerDownHandler
{
    public bool jump = false;
    public virtual void OnPointerDown(PointerEventData ped)
    {
        jump = true;
    }
}