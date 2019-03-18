using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
/// <summary>
/// Класс для работы Паузы
/// </summary>
public class Pause : MonoBehaviour, IPointerDownHandler
{
    #region Объявление переменных


    /// <summary>
    /// Переменная для определения состояния паузы
    /// </summary>
    private bool paused = false;
    /// <summary>
    /// Переменная для отображения окон
    /// </summary>
    private int window = 100;
    /// <summary>
    /// Публичная переменная для применения параметров изменения к GUI объектам
    /// </summary>
    public GUISkin skin;
    /// <summary>
    /// Переменная, отмечает за ползунок в Громкости, Чувствительности камеры
    /// </summary>
    public float ValueSpliter, SpliterX = 4, SpliterY = 4;
    /// <summary>
    /// Отвечает зя создание нового Slider
    /// </summary>
    private Slider mainSlider;
    /// <summary>
    /// Статическая переменная вызывается в других классах с сохранением изменений в данном классе
    /// </summary>
    public static Pause pause;
    /// <summary>
    /// Отвечает за регулирование громкости в игре
    /// </summary>
    public AudioMixer am;
    /// <summary>
    /// Отвечает за уровни Графики в игры
    /// </summary>
    public string[] items;
    /// <summary>
    /// Содержет в себе значение выбранного качества Графики в игре
    /// </summary>
    private string selectedItem;
    /// <summary>
    /// Отвечает за условие, при котором меняется пложение кнопки "Назад"
    /// </summary>
    private bool edit = false;
    /// <summary>
    /// Переменная, которая определяет уровень Графики в игре
    /// </summary>
    public int i;
    /// <summary>
    /// Изменения, которые вносятся при старте 
    /// </summary>
    #endregion
    void Start()
    {
        SpliterX = 4;
        SpliterY = 4;
        pause = this;
        selectedItem = items[QualitySettings.GetQualityLevel()];
    }
    #region Меню закрытие и открытие
    /// <summary>
    /// Отвечает за нажатие, т.е. откртие и закрытие Игрового меню (Паузы)
    /// </summary>
    /// <param name="ped">Параметр передачи</param>
    public virtual void OnPointerDown(PointerEventData ped)
    {
        if (!paused)
        {
            Time.timeScale = 0;
            paused = true;
            SceneManager.LoadScene("GameMenu");
        }
        else
        {
            Time.timeScale = 1;
            paused = false;
            window = 100;
        }
    }
    #endregion
    #region GUI функция
    /// <summary>
    /// Отчечает за взаимодествие с GUI элементами
    /// </summary>
    void OnGUI()
    {
        Settings settings = Settings.settings;
        //settings.am.GetFloat("masterVolume", out ValueSpliter);
        Debug.Log("P" + QualitySettings.GetQualityLevel());
        GUI.backgroundColor = new Color32(255, 255, 255, 150);
        GUI.skin = skin;
        #region Игровое меню (Главное)
        if (window == 0)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Игровое меню");
            GUI.backgroundColor = new Color32(255, 0, 112, 200);
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 8, Screen.width / 3, Screen.height / 9), "Продолжить"))
            {
                Time.timeScale = 1;
                paused = false;
                window = 100;
            }
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 7, Screen.width / 3, Screen.height / 9), "Настройки"))
            {
                window = 3;
            }
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 6, Screen.width / 3, Screen.height / 9), "Персонаж"))
            {
                window = 7;
            }
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 5, Screen.width / 3, Screen.height / 9), "Выход в Главное меню"))
            {
                Time.timeScale = 1;
                paused = false;
                window = 100;
                SceneManager.LoadScene("MainMenu");
            }
        }
        #endregion

        #region Персонаж
        if(window == 7)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Персонаж");
            GUI.backgroundColor = new Color32(255, 0, 112, 200);
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 8, Screen.width / 3, Screen.height / 9), "Инвентарь"))
            {
                window = 8;
            }
        }
        #endregion


        #region Игровое меню (Настройки)
        if (window == 3)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Настройки");
            GUI.backgroundColor = new Color32(255, 0, 112, 200);
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 8, Screen.width / 3, Screen.height / 9), "Громкость"))
            {
                window = 4;
            }
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 7, Screen.width / 3, Screen.height / 9), "Качество графики"))
            {
                window = 5;
            }
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 6, Screen.width / 3, Screen.height / 9), "Чувствительность камеры"))
            {
                window = 6;
            }
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 5, Screen.width / 3, Screen.height / 9), "Назад"))
            {
                window = 0;
            }
        }
        #endregion
        if(window == 4)
        {
            GUI.backgroundColor = new Color32(255, 255, 255, 150);
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Громкость");
            GUI.backgroundColor = new Color32(255, 0, 112, 200);
            GUI.backgroundColor = new Color32(255, 255, 255, 150);
            ValueSpliter = GUI.HorizontalSlider(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 7, Screen.width / 3, Screen.height / 9), ValueSpliter, -80.0F, 0.0F);
            am.SetFloat("masterVolume", ValueSpliter);
            GUI.backgroundColor = new Color32(255, 0, 112, 200);
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 6, Screen.width / 3, Screen.height / 9), "Назад"))
            {
                window = 3;
            }
        }
        if (window == 5)
        {
            GUI.backgroundColor = new Color32(255, 255, 255, 150);
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Качество графики");
            GUI.backgroundColor = new Color32(255, 255, 255, 255);
            GUI.contentColor = Color.black;
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 5, Screen.width / 3, Screen.height / 9), selectedItem)) edit = true;
            if (edit)
            {
                for (i = 0; i < items.Length; i++)
                {
                    if (GUI.Button(new Rect(Screen.width / 3, (Screen.height - Screen.height / 17 * (8 - i)), Screen.width / 3, Screen.height / 17), items[i]))
                    {
                        selectedItem = items[i];
                        edit = false;
                        QualitySettings.SetQualityLevel(i);
                    }
                }
            }
            GUI.backgroundColor = new Color32(255, 0, 112, 200);
            GUI.contentColor = Color.white;
            if (edit)
            {
                if (GUI.Button(new Rect(Screen.width / 3, (Screen.height - Screen.height / 9), Screen.width / 3, Screen.height / 10), "Назад") || Input.GetKeyUp(KeyCode.Escape))
                {
                    window = 0;
                }
            }
            else
            {
                if (GUI.Button(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 4, Screen.width / 3, Screen.height / 9), "Назад") || Input.GetKeyUp(KeyCode.Escape))
                {
                    window = 0;
                }
            }
        }
        if (window == 6)
        {
            GUI.backgroundColor = new Color32(255, 255, 255, 150);
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Чувствительность камеры");
            GUI.Label(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 8, Screen.width / 3, Screen.height / 9), "По X:");
            GUI.backgroundColor = new Color32(255, 0, 112, 200);
            GUI.backgroundColor = new Color32(255, 255, 255, 150);
            SpliterX = GUI.HorizontalSlider(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 7, Screen.width / 3, Screen.height / 9), SpliterX, 0.0F, 6.0F);

            GUI.Label(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 6, Screen.width / 3, Screen.height / 9), "По Y:");
            GUI.backgroundColor = new Color32(255, 0, 112, 200);
            GUI.backgroundColor = new Color32(255, 255, 255, 150);
            SpliterY = GUI.HorizontalSlider(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 5, Screen.width / 3, Screen.height / 9), SpliterY, 0.0F, 6.0F);
            GUI.backgroundColor = new Color32(255, 0, 112, 200);

            if (GUI.Button(new Rect(Screen.width / 3, Screen.height - Screen.height / 9 * 4, Screen.width / 3, Screen.height / 9), "Назад"))
            {
                window = 3;
            }
        }
    }
    #endregion

   
}