using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_OptionPopup : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnClickContinue()
    {
        GameManager.Instance.Continue();
        Hide();
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickExit()
    {

    }
}
