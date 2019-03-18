using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Rigidbody sp;
    public Camera camera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sp = (Rigidbody)Instantiate(sp, GameObject.Find("SpawnBullets").transform.position, Quaternion.identity);
            sp.AddForce(camera.transform.forward * 1000);
        }
    }
}
