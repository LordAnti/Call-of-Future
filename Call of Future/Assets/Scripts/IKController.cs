using System.Collections;
using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKController : MonoBehaviour
{
    protected Animator anim;

    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform lookObj;
    public CharacterStatus cs;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if(anim)
        {
            if (ikActive)
            {
                if(lookObj != null)
                {
                    anim.SetLookAtWeight(1, 1, 1);
                    if (cs.isPistol == true)
                    {
                        Vector3 v3 = lookObj.position;
                        v3.y -= 30f;
                        anim.SetLookAtPosition(v3);
                    }
                    else
                        anim.SetLookAtPosition(lookObj.position);
                }

                if(rightHandObj != null)
                {
                    anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                anim.SetLookAtWeight(0);
            }
        }
    }
}
