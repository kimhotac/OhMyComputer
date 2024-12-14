using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ClearBadScene : MonoBehaviour
{
    GameObject electric2;
    GameObject electric1;
    float sign = 1;

    int score;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        this.electric1 = GameObject.Find("electric1");
        this.electric2 = GameObject.Find("electric2");
        this.score = PlayerPrefs.GetInt("Score", 0); // ����� ���� �ҷ�����, �⺻���� 0
        Debug.Log("Score from previous scene: " + this.score);

      

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.GetKeyDown(KeyCode.Space)))
        {
            SceneManager.LoadScene("GameScene");
        }


        //-3.5���� ũ�� �������� �̵� -4.5���� ������ ���������� �̵�
        //�������� 30ȸ �̵� (���������� 60ȸ �̵� �������� 60ȸ �̵�) �ݺ�


        float x = this.electric1.GetComponent<Transform>().transform.position.x;
        if (x > -3.5f || x < -4.5f)
        {
            sign = sign * -1;
        }
        this.electric1.GetComponent<Transform>().transform.Translate((sign * 1f / 30), 0, 0);
        this.electric2.GetComponent<Transform>().transform.Translate((sign * 1f / 30), 0, 0);
        

    }


}