using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropItemGenerator : MonoBehaviour
{
    public GameObject electricPrefab;
    public GameObject waterPrefab;
    public bool gen_flag = true;
    float span = 0.6f;
    float delta = 0;
    float speed = -0.03f;
    float rate = 70f;

    float waterProbability = 0.0f; // �� ������ �ʱ� Ȯ�� (0%)
    float waterProbabilityIncreaseRate = 2; // ���� �� ������ Ȯ�� ������

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetParameter( float speed, float rate)
    {
        this.speed = speed;
        this.rate = rate;
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span && this.gen_flag)
        {
            this.delta = 0;
            if (this.waterProbability < 1.0f)
                this.waterProbability += waterProbabilityIncreaseRate;
                float randomValue = Random.value; // 0.0f ~ 1.0f ������ ��

                if (randomValue < this.rate) // 70%
                {
                    GameObject et = Instantiate(electricPrefab);
                    int px = Random.Range(-7, 7);
                    et.transform.position = new Vector3(px, 7, 0);
                    et.GetComponent<dropitemcontroller>().dropSpeed = this.speed;
                }
                else // 30% Ȯ��
                {
                    GameObject wt = Instantiate(waterPrefab);
                    int wx = Random.Range(-7, 7);
                    wt.transform.position = new Vector3(wx, 7, 0);
                    wt.GetComponent<dropitemcontroller>().dropSpeed = this.speed;
                }
            
        }
    }
}