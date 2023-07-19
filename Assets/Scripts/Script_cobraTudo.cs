using UnityEngine;

public class Script_cobraTudo : MonoBehaviour
{
    GameObject obj_adm;
    int direcao = 0;
    [SerializeField] float moveSpeed;
    [SerializeField] float moveCd;
    [SerializeField] GameObject prefab_obj_cobraBloco;
    [SerializeField] GameObject[] obj_bloco;
    Vector2 cobraPos;
    [SerializeField] Vector2[] blocoPos;
    [SerializeField] float[] blocoAndarPodeTempo;
    [SerializeField] int blocoTamanho;
    [SerializeField] float tempo;

    void Start()
    {
        obj_adm = GameObject.Find("obj_adm");
        Invoke("Move", moveCd);
    }

    void Update()
    {
        bool _direcaoMudou = false;

        if (Input.GetKeyDown(KeyCode.D) && direcao != 2)
        {
            _direcaoMudou = true;
            direcao = 0;
        }
        if (Input.GetKeyDown(KeyCode.W) && !_direcaoMudou && direcao != 3)
        {
            _direcaoMudou = true;
            direcao = 1;
        }
        if (Input.GetKeyDown(KeyCode.A) && !_direcaoMudou && direcao != 0)
        {
            _direcaoMudou = true;
            direcao = 2;
        }
        if (Input.GetKeyDown(KeyCode.S) && !_direcaoMudou && direcao != 1)
        {
            direcao = 3;
        }
    }

    void Move()
    {
        tempo++;

        cobraPos = transform.position;

        switch (direcao)
        {
            case 0:
                transform.position += Vector3.right * moveSpeed;
                break;
            case 1:
                transform.position += Vector3.up * moveSpeed;
                break;
            case 2:
                transform.position += Vector3.left * moveSpeed;
                break;
            case 3:
                transform.position += Vector3.down * moveSpeed;
                break;
        }

        for (int i = 0; i < blocoTamanho; i++)
        {
            if (obj_bloco != null)
            {
                blocoPos[i] = obj_bloco[i].transform.position;

                if (tempo >= blocoAndarPodeTempo[i])
                {
                    if (i == 0) obj_bloco[i].transform.position = cobraPos;
                    else obj_bloco[i].transform.position = blocoPos[i - 1];
                }
            }
        }

        Invoke("Move", moveCd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Comida"))
        {
            obj_bloco[blocoTamanho] = Instantiate(prefab_obj_cobraBloco, transform.position, Quaternion.identity);
            blocoAndarPodeTempo[blocoTamanho] = tempo + blocoTamanho + 1;

            blocoTamanho++;

            Destroy(collision.gameObject);
            obj_adm.GetComponent<Script_admGameTudo>().ObjComidaSpawn();
        }
    }
}