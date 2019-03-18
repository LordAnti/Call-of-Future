using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    private EnterCam EC;
    public GameObject player;
    public Transform playerBody;
    public Canvas canvas;
    public float speedCam = 15;
    public float speedScrolls = 15;
    public float minDistance;
    public float maxDistance;

    private bool isActive = false;
    public float distance;
    private float x;
    private float y;

    private void LateUpdate()
    {
        //Получение значения сдвига мыши
        x = Input.GetAxis("Mouse X") * speedCam * 10;
        y = Input.GetAxis("Mouse Y") * -speedCam * 10;
        EC = GameObject.FindGameObjectWithTag("Enter").GetComponent<EnterCam>();

        if (Input.GetMouseButtonDown(1) && Input.touchCount == 0 && Time.timeScale == 1)
        {
            isActive = true;
        }

        if (Input.GetMouseButtonUp(1) && Input.touchCount == 0)
        {
            isActive = false;
        }

        //Вращение
        if (isActive)
        {
            playerBody.RotateAround(playerBody.transform.position, transform.up, x * Time.deltaTime);
            playerBody.RotateAround(playerBody.transform.position, transform.right, y * Time.deltaTime);
            playerBody.eulerAngles = new Vector3(playerBody.eulerAngles.x, playerBody.eulerAngles.y, 0);

            if (Input.touchCount > 0)
            {
                x = Input.touches[0].deltaPosition.x;
                y = -Input.touches[0].deltaPosition.y;
            }

            if (x != 0)
                playerBody.transform.Rotate(playerBody.TransformDirection(Vector3.up), x * speedCam, Space.World);
            if (y != 0)
                playerBody.transform.Rotate(playerBody.TransformDirection(Vector3.right), y * speedCam, Space.World);

            playerBody.eulerAngles = new Vector3(playerBody.eulerAngles.x, playerBody.eulerAngles.y, 0);
        }

        //Приближение/удаление
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            /*if (Input.GetAxis("Mouse ScrollWheel") > 0 && distance > minDistance)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speedScrolls);
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && distance < maxDistance)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * -speedScrolls);
            }*/
        }
    }
}
