using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MySetActive : MonoBehaviour, IPointerDownHandler
{
    public GameObject gameObject;

    private bool value;

    public virtual void OnPointerDown(PointerEventData ped)
    {
        if (value)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
