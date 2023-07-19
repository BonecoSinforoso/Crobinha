using UnityEngine;

public class Script_admGameTudo : MonoBehaviour
{
    [SerializeField] GameObject prefab_obj_comida;

    void Start()
    {
        Application.targetFrameRate = 60;

        //Teste();
    }

    void Update()
    {

    }

    public void ObjComidaSpawn()
    {
        Vector2 _pos = new Vector2(Random.Range(0, 35) * 0.5f - 8.5f, Random.Range(0, 19) * 0.5f - 4.5f);
        Instantiate(prefab_obj_comida, _pos, Quaternion.identity);
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
}