using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class dropitemcontroller : MonoBehaviour
{
    GameObject com;
    // Start is called before the first frame update
    void Start()
    {
        this.com = GameObject.Find("com");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.1f, 0);

        if(transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }
}
