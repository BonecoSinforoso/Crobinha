using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
}
