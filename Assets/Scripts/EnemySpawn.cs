using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject monster;
    public static EnemySpawn instance;
    public Queue<GameObject> m_queue = new Queue<GameObject>();
    public float xPos;
    public float zPos;
    private Vector3 RandomVector;

    private float initialSpawnCoolTime = 4.0f;
    public float subCoolTime = 0;

    public GameManager gameManager;
    private float wave;

    void OnEnable()
    {
        wave = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().waveCount;
        subCoolTime = 0;
    }

    void Start()
    {
        instance = this;

        for (int i = 0; i < 50; i++)
        {
            GameObject t_object = Instantiate(monster, this.gameObject.transform);
            m_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }

        StartCoroutine(EnemySpawnPhase());
    }

    public void InsertQueue(GameObject p_object)
    {

        m_queue.Enqueue(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetQueue()
    {
        GameObject t_object = m_queue.Dequeue();
        t_object.SetActive(true);

        return t_object;
    }

    IEnumerator EnemySpawnPhase()
    {
        while (true)
        {
            if (m_queue.Count != 0)
            {
                xPos = Random.Range(-0.1f, 0.1f);
                zPos = Random.Range(-3.5f, 3.5f);
                RandomVector = new Vector3(xPos, 0.0f, zPos);
                GameObject t_object = GetQueue();
                t_object.transform.position = gameObject.transform.position + RandomVector;
            }
            yield return new WaitForSeconds(initialSpawnCoolTime - subCoolTime);
        }
    }
}
