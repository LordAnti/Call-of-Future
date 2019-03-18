using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Класс настроек для игры
/// </summary>
public class Settings : MonoBehaviour
{
    #region Объявление переменных
    /// <summary>
    /// Переменная, которая отвечает за настройку громкости Mixer
    /// </summary>
    public AudioMixer am;
    /// <summary>
    /// Переменная, которая отвечает за передачу значения громкости из Игрового меню
    /// </summary>
    private float newValue;
    /// <summary>
    /// Переменная, которая отвечает за определения Scene Settings и все изменения 
    /// </summary>
    public static Settings settings;
    /// <summary>
    /// Определение класса Паузы с его изменениями в реальном времени
    /// </summary>
    Pause pause = Pause.pause;
    #endregion
    #region Функционал
    /// <summary>
    /// Содержет стартовые настройки
    /// </summary>
    void Start()
    {
        settings = this;
        GameObject.Find("Slider").GetComponent<Slider>().value = newValue;
        GameObject.Find("Dropdown").GetComponent<Dropdown>().value = QualitySettings.GetQualityLevel();
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
    #endregion
}
