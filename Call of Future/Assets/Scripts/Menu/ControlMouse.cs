using UnityEngine;

public class ControlMouse : MonoBehaviour
{
    public float x, y;
    public CameraConfig cc;
    void Start()
    {
        x = cc.X_rot_speed;
        y = cc.Y_rot_speed;
    }

    void Update()
    {
        cc.X_rot_speed = x;
        cc.Y_rot_speed = y;
    }
}
