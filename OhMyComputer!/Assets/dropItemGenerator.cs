using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropItemGenerator : MonoBehaviour
{
    public GameObject electricPrefab;
    public GameObject waterPrefab;
    float span = 1.0f;
    float delta = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            float randomValue = Random.value; // 0.0f ~ 1.0f »çÀÌÀÇ °ª

            if (randomValue < 0.7f) // 70% È®·ü
            {
                GameObject et = Instantiate(electricPrefab);
                int px = Random.Range(-6, 7);
                et.transform.position = new Vector3(px, 7, 0);
            }
            else // 30% È®·ü
            {
                GameObject wt = Instantiate(waterPrefab);
                int wx = Random.Range(-6, 7);
                wt.transform.position = new Vector3(wx, 7, 0);
            }
        }
    }
}