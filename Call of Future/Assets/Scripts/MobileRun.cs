using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileRun : MonoBehaviour, IPointerDownHandler
{
    public bool run = false;
    public virtual void OnPointerDown(PointerEventData ped)
    {
        if (run == false)
        {
            run = true;
            GetComponent<Image>().sprite = Resources.Load<Sprite>(@"CanvasInterface/КнопкаХодьбы");
        }
        else
        {
            run = false;
            GetComponent<Image>().sprite = Resources.Load<Sprite>(@"CanvasInterface/КнопкаБега");
        }
    }
}