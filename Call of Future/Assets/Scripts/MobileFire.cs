using UnityEngine;
using UnityEngine.EventSystems;

public class MobileFire : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Rigidbody sp;
    public Camera camera;
    public bool Enter = false;
    public virtual void OnPointerDown(PointerEventData ped)
    {
        Enter = true;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        Enter = false;
    }

    private void Update()
    {
        if (Enter == true)
        {
            sp = (Rigidbody)Instantiate(sp, GameObject.Find("SpawnBullets").transform.position, Quaternion.identity);
            sp.AddForce(camera.transform.forward * 1000);
        }
    }
}
