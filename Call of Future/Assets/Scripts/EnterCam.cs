using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnterCam : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    float x, y;

    //Для положения камеры
    public Transform camTrans;
    public Transform pivot;
    public Transform Character;
    public Transform mTransform;

    public CharacterStatus characterStatus;
    public CameraConfig cameraConfig;
    public bool leftPivot;
    public float delta;

    public float mouseX;
    public float mouseY;
    public float smoothX;
    public float smoothY;
    public float smoothXVelocity;
    public float smoothYVelocity;
    public float lookAngel;
    public float titAngle;
    public float rotationSpeed;

    //Для персонажа
    public Vector3 rotationDirection;
    public Vector3 moveDirection;
    public Transform targetLook;
    public Animator anim;

    void Update()
    {
        delta = Time.deltaTime;

        HandlePosition();
        OnAnimatorIK();

        Vector3 targetPosition = Vector3.Lerp(mTransform.position, Character.position, 1);
        mTransform.position = targetPosition;
    }

    void LateUpdate()
    {
        anim.SetLookAtPosition(targetLook.position);
        anim.SetLookAtWeight(1.0f, 1.0f, 1.0f);
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {

    }

    public virtual void OnDrag(PointerEventData ped)
    {
        if (Time.timeScale == 1)
        {
            x = ped.delta.x / 200;
            y = -ped.delta.y / 200;

            if ((x != 0) || (y != 0)) //Поворот камеры
            {
                mouseX = ped.delta.x / 200;
                mouseY = ped.delta.y / 200;

                if (cameraConfig.turnSmooth > 0)
                {
                    smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXVelocity, cameraConfig.turnSmooth);
                    smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothYVelocity, cameraConfig.turnSmooth);
                }
                else
                {
                    smoothX = mouseX;
                    smoothY = mouseY;
                }

                lookAngel += smoothX * cameraConfig.Y_rot_speed;
                Quaternion targetRot = Quaternion.Euler(0, lookAngel, 0);
                mTransform.rotation = targetRot;

                titAngle -= smoothY * cameraConfig.Y_rot_speed;
                titAngle = Mathf.Clamp(titAngle, cameraConfig.minAngle, cameraConfig.maxAngle);
                pivot.localRotation = Quaternion.Euler(titAngle, 0, 0);
            }

            /*if (x != 0)
            {
                
            }
                Character.transform.Rotate(Character.TransformDirection(Vector3.up), x * 7f, Space.World);
            if (y != 0)
            {
                //Character.transform.Rotate(Character.TransformDirection(Vector3.right), y * 0.2f, Space.World);
            }
            Character.eulerAngles = new Vector3(Character.eulerAngles.x, Character.eulerAngles.y, 0);*/
        }
    }

    //Положение камеры
    void HandlePosition()
    {
        float targetX = cameraConfig.normalX;
        float targetY = cameraConfig.normalY;
        float targetZ = cameraConfig.normalZ;

        targetX = cameraConfig.aimX;
        targetZ = cameraConfig.aimZ;

        if (leftPivot)
        {
            targetX = -targetX;
        }

        Vector3 newPivotPosition = pivot.localPosition;
        newPivotPosition.x = targetX;
        newPivotPosition.y = targetY;

        Vector3 newCameraPosition = camTrans.localPosition;
        newCameraPosition.z = targetZ;

        float t = delta * cameraConfig.pivotSpeed;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
        camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, newCameraPosition, t);
    }

    void OnAnimatorIK()
    {
        anim.SetLookAtPosition(targetLook.position);
        anim.SetLookAtWeight(1.0f, 1.0f, 1.0f);

        /*Transform head = anim.GetBoneTransform(HumanBodyBones.Head);
        Vector3 forward = (targetLook.position - head.position).normalized;
        Vector3 up = Vector3.Cross(forward, transform.right);
        Quaternion rotation = Quaternion.Inverse(transform.rotation) * Quaternion.LookRotation(forward, up);
        anim.SetBoneLocalRotation(HumanBodyBones.Head, rotation);*/
    }
}
