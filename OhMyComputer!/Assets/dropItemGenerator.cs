using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropItemGenerator : MonoBehaviour
{
    public GameObject electricPrefab;
    public GameObject waterPrefab;
    float span = 1.0f;
    float delta = 0;
    float speed = -0.03f;
    int itemCount = 1;

    float waterProbability = 0.0f; // 물 아이템 초기 확률 (0%)
    float waterProbabilityIncreaseRate = 2; // 매초 물 아이템 확률 증가율

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetParameter(float span, float speed, int itemCount)
    {
        this.span = span;
        this.speed = speed;
        this.itemCount = itemCount; // 아이템 수량 설정
    }
    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            if (this.waterProbability < 1.0f)
                this.waterProbability += waterProbabilityIncreaseRate;

            for (int i = 0; i < this.itemCount; i++) // 설정된 수량만큼 아이템 생성
            {
                float randomValue = Random.value; // 0.0f ~ 1.0f 사이의 값

                if (randomValue < 0.7f) // 70%
                {
                    GameObject et = Instantiate(electricPrefab);
                    int px = Random.Range(-6, 7);
                    et.transform.position = new Vector3(px, 7, 0);
                    et.GetComponent<dropitemcontroller>().dropSpeed = this.speed;
                }
                else // 30% 확률
                {
                    GameObject wt = Instantiate(waterPrefab);
                    int wx = Random.Range(-6, 7);
                    wt.transform.position = new Vector3(wx, 7, 0);
                    wt.GetComponent<dropitemcontroller>().dropSpeed = this.speed;
                }
            }
        }
    }
}