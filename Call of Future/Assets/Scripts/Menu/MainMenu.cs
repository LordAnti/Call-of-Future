using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Класс Главного меню
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Открытие сцены с загрузкой, в случае, если игра долго грузится
    /// </summary>
    public SceneStatus ss;
    public void PlayPressed()
    {
        ss.isNewGame = true;
        SceneManager.LoadScene("Loading");
    }
    /// <summary>
    /// Выключение игры
    /// </summary>
    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Игра закрылась.");
    }  
}