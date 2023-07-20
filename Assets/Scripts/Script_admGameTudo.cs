using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_admGameTudo : MonoBehaviour
{
    [SerializeField] GameObject prefab_obj_comida;
    [Space]
    [SerializeField] Button btn_restart;
    [SerializeField] Text txt_resultado;
    [SerializeField] Image img_painel;

    void Start()
    {
        Application.targetFrameRate = 60;

        ObjComidaSpawn();

        //Teste();
    }

    public void ObjComidaSpawn()
    {
        Vector2 _pos = new Vector2(Random.Range(0, 35) * 0.5f - 8.5f, Random.Range(0, 19) * 0.5f - 4.5f);

        RaycastHit2D _hit = Physics2D.Raycast(_pos, Vector2.zero);

        if (!_hit.collider) Instantiate(prefab_obj_comida, _pos, Quaternion.identity);
        else ObjComidaSpawn();
    }

    void Teste()
    {
        for (int i = 0; i < 35; i++)
        {
            for (int j = 0; j < 19; j++)
            {
                Instantiate(prefab_obj_comida, new Vector3(i * 0.5f - 8.5f, j * 0.5f - 4.5f, 0), Quaternion.identity);
            }
        }
    }

    public void Perdeu()
    {
        Time.timeScale = 0;
        txt_resultado.gameObject.SetActive(true);
        txt_resultado.text = "You Lose!";

        btn_restart.gameObject.SetActive(true);

        img_painel.gameObject.SetActive(true);
    }

    public void SceneRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }
}