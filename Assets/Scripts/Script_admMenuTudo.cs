using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_admMenuTudo : MonoBehaviour
{
    [SerializeField] Text txt_versao;

    void Start()
    {
        Application.targetFrameRate = 60;
        txt_versao.text = Application.version;
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("Scene_game");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
