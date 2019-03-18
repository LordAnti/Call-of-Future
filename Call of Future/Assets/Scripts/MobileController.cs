using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image Jostik;
    private Image MoveJostik;
    private Vector2 inputVector; // Координаты джостика

    private void Start()
    {
        Jostik = GetComponent<Image>();
        var theBarRectTransform = Jostik.transform as RectTransform;
        theBarRectTransform.sizeDelta = new Vector2(Screen.width / 5, Screen.height / 3);
        MoveJostik = transform.GetChild(0).GetComponent<Image>();
        MoveJostik.color = Color.clear;
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        MoveJostik.color = Color.white;
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        MoveJostik.color = Color.clear;
        inputVector = Vector2.zero;
        MoveJostik.rectTransform.anchoredPosition = Vector2.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (Time.timeScale == 1)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Jostik.rectTransform, ped.position, ped.pressEventCamera, out pos))
            {
                pos.x = (pos.x / Jostik.rectTransform.sizeDelta.x);
                pos.y = (pos.y / Jostik.rectTransform.sizeDelta.y);

                inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
                inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

                MoveJostik.rectTransform.anchoredPosition = new Vector2(inputVector.x * (Jostik.rectTransform.sizeDelta.x / 2), inputVector.y * (Jostik.rectTransform.sizeDelta.y / 2));
            }
        }
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.y != 0)
            return inputVector.y;
        else
            return Input.GetAxis("Vertical");
    }
}
