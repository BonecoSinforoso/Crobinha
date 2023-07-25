using UnityEngine;

public class Script_cobraTudo : MonoBehaviour
{
    GameObject obj_adm;
    int direcao = 0;
    bool direcaoMudarPode = true;
    [SerializeField] float moveSpeed;
    [SerializeField] float moveCd;
    [SerializeField] GameObject emp_cobraBlocoPai;
    [SerializeField] GameObject prefab_obj_cobraBloco;
    [SerializeField] GameObject[] obj_cobraBloco;
    Vector2 cobraPos;
    [SerializeField] Vector2[] blocoPos;
    [SerializeField] float[] blocoAndarPodeTempo;
    [SerializeField] int blocoTamanho;
    [SerializeField] float tempo;
    [SerializeField] int comidaGanho;
    [Space]
    [SerializeField] AudioClip[] aud_coleta;
    [SerializeField] AudioClip aud_morte;
    AudioSource audioSource;

    void Start()
    {
        obj_adm = GameObject.Find("obj_adm");
        audioSource = GetComponent<AudioSource>();
        direcao = Random.Range(0, 4);

        Invoke("Move", moveCd);
    }

    void Update()
    {
        if (direcaoMudarPode)
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
                _direcaoMudou = true;
                direcao = 3;
            }

            if (_direcaoMudou) direcaoMudarPode = false;
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
            if (obj_cobraBloco != null)
            {
                blocoPos[i] = obj_cobraBloco[i].transform.position;

                if (tempo >= blocoAndarPodeTempo[i])
                {
                    if (i == 0) obj_cobraBloco[i].transform.position = cobraPos;
                    else obj_cobraBloco[i].transform.position = blocoPos[i - 1];

                    obj_cobraBloco[i].GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        direcaoMudarPode = true;

        Invoke("Move", moveCd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Comida"))
        {
            for (int i = 0; i < comidaGanho; i++)
            {
                obj_cobraBloco[blocoTamanho] = Instantiate(prefab_obj_cobraBloco, transform.position, Quaternion.identity);
                obj_cobraBloco[blocoTamanho].transform.parent = emp_cobraBlocoPai.transform;
                blocoAndarPodeTempo[blocoTamanho] = tempo + blocoTamanho + 1;

                blocoTamanho++;
            }

            Destroy(collision.gameObject);
            obj_adm.GetComponent<Script_admGameTudo>().ObjComidaSpawn();
            audioSource.PlayOneShot(aud_coleta[Random.Range(0, aud_coleta.Length - 1)]);

            if (blocoTamanho == 664) obj_adm.GetComponent<Script_admGameTudo>().Ganhou();
        }

        if (collision.CompareTag("Bloco"))
        {
            obj_adm.GetComponent<Script_admGameTudo>().Perdeu();

            audioSource.PlayOneShot(aud_morte);
        }

        if (collision.CompareTag("Parede"))
        {
            obj_adm.GetComponent<Script_admGameTudo>().Perdeu();

            audioSource.PlayOneShot(aud_morte);
        }
    }
}