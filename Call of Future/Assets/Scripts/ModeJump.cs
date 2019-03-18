using UnityEngine;
using UnityEngine.EventSystems;

public class ModeJump : MonoBehaviour, IPointerDownHandler
{
    public CharacterStatus cs;
    public CharacterController ch_controller;
    public virtual void OnPointerDown(PointerEventData ped)
    {
        if (ch_controller.isGrounded)
            cs.isJump = true;
    }
}
