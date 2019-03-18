using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModeRun : MonoBehaviour, IPointerDownHandler
{
    public CharacterStatus cs;

    void Start()
    {
        cs.isSprint = false;
        GetComponent<Image>().sprite = Resources.Load<Sprite>(@"CanvasInterface/КнопкаБега");
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        if (cs.isSprint == false)
        {
            cs.isSprint = true;
            GetComponent<Image>().sprite = Resources.Load<Sprite>(@"CanvasInterface/КнопкаХодьбы");
        }
        else
        {
            cs.isSprint = false;
            GetComponent<Image>().sprite = Resources.Load<Sprite>(@"CanvasInterface/КнопкаБега");
        }
    }
}
