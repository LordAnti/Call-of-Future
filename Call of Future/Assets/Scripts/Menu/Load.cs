using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public GameObject loadingInfo, loadingIcon;
    private AsyncOperation async;
    public SceneStatus ss;
    IEnumerator Start()
    {
        loadingIcon.SetActive(true);
        loadingInfo.SetActive(false);
        if (ss.isNewGame == true)
        {
            async = SceneManager.LoadSceneAsync("NewGame");
            yield return true;
            async.allowSceneActivation = true;
        }
        //loadingIcon.SetActive(false);
       // loadingInfo.SetActive(true);
    }
}