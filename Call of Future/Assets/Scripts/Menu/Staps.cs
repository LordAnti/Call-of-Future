using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Staps : MonoBehaviour, IPointerDownHandler
{
    /// <summary>
    /// Переменная для определения состояния паузы
    /// </summary>
    public bool paused = false;

    public GameObject cameraGameMenu;

    public GameObject cameraLocation;

    public GameObject canvasGameMenu;

    public GameObject location;

    public GameObject file;

    public AudioMixer am;

    private float newValue;

    public static Staps staps;

    void Start()
    {
        Settings settings = Settings.settings;
        staps = this;
        am.GetFloat("masterVolume", out newValue);
        //GameObject.Find("SliderAudio").GetComponent<Slider>().value = newValue;
        //GameObject.Find("Dropdown").GetComponent<Dropdown>().value = QualitySettings.GetQualityLevel();
        Debug.Log("Пустое - "+newValue.ToString());
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        paused = false;
        canvasGameMenu.SetActive(false);
        cameraGameMenu.SetActive(false);
        cameraLocation.SetActive(true);
        location.SetActive(true);
    }

    public void ExitToLocation()
    {
        Time.timeScale = 1;
        paused = false;
        canvasGameMenu.SetActive(false);
        cameraGameMenu.SetActive(false);
        cameraLocation.SetActive(true);
        location.SetActive(true);
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        Time.timeScale = 0;
        paused = true;
        cameraLocation.SetActive(false);
        canvasGameMenu.SetActive(true);
        cameraGameMenu.SetActive(true);
        location.SetActive(false);
    }
    /// <summary>
    /// Метод, отвечает за регулировку громкости звука
    /// </summary>
    /// <param name="sliderValue">Параметр, отвечает за передачу значения в Mixer</param>
    public void AudioVolume(float sliderValue)
    {
        am.SetFloat("masterVolume", sliderValue);
    }
    /// <summary>
    /// Метод, отвечает за качество изображения игры
    /// </summary>
    /// <param name="q">Параметр, в который передается индекс из DropDown для выбора качества отрисовки</param>
    public void Quality(int q)
    {
        q = GameObject.Find("Dropdown").GetComponent<Dropdown>().value;
        QualitySettings.SetQualityLevel(q);
    }

    public void X_Orientation(float X_OrientationValue)
    {
        CameraConfig config = new CameraConfig();
        config.X_rot_speed = X_OrientationValue;
    }

    public void Y_Orientation(float Y_OrientationValue)
    {
        CameraConfig config = new CameraConfig();
        config.Y_rot_speed = Y_OrientationValue;
    }
}
