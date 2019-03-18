using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleCursor : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public float secondgametime = 0;
    public byte i = 0;
    public GameObject player;
    public float distance;
    bool Double = false;
    public Camera camera;
    public bool Ent = false;
    public virtual void OnPointerDown(PointerEventData ped)
    {
        Ent = true;
        i++;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        Ent = false;
    }

    private void Update()
    {
        if (i != 0)
            secondgametime += Time.deltaTime;

        if (secondgametime >= 1)
            i = 0;

        if (i == 0)
            secondgametime = 0;

        if (i == 2)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            if (Double)
            {
                Double = false;
                camera.transform.Translate(Vector3.forward * -0.7f);
            }
            else
            {
                Double = true;
                camera.transform.Translate(Vector3.forward * 0.7f);
            }
            i = 0;
        }
    }
}
