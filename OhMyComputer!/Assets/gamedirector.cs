using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject batteryUI_img;
    GameObject batteryUI_text;
    GameObject itemGenerator;
    int point = 50;
    int time = 0;
    GameObject generator;
    //���� ���͸��� point - time

    // Start is called before the first frame update
    void Start()
    {
        this.generator = GameObject.Find("ItemGenerator");
        this.batteryUI_img = GameObject.Find("battery_img");
        this.batteryUI_text = GameObject.Find("battery_text");
        this.itemGenerator = GameObject.Find("dropItemGenerator");
        UIupdate();
        InvokeRepeating("DecreaseBattery", 1f, 1f); // 1�� �ĺ��� ���ʸ��� ����
    }
    void Update()
    {
        if (time < 10)
        {
            this.generator.GetComponent<dropItemGenerator>().SetParameter(0.7f, -1.0f,1);
        }
        else if (time < 20)
        {
            this.generator.GetComponent<dropItemGenerator>().SetParameter(0.6f, -1.25f,4);
        }
        else if (time < 30)
        {
            this.generator.GetComponent<dropItemGenerator>().SetParameter(0.5f, -1.5f,5);
        }
        else if (time < 40)
        {
            this.generator.GetComponent<dropItemGenerator>().SetParameter(0.4f, -1.56f,6);
        }
        else if (time < 50)
        {
            this.generator.GetComponent<dropItemGenerator>().SetParameter(0.3f, -0.8f,7);
        }
        else
        {
            this.generator.GetComponent<dropItemGenerator>().SetParameter(0.2f, -0.9f,8);
        }
    }

    // ���� �浹
    public void Getelectric()
    {
        this.point += 2;
        UIupdate();
    }

    // �ð� ����
    public void DecreaseBattery()
    {
        this.time++;
        UIupdate();
        if (this.point >= 100) //���͸��� 100���� Ŭ��
        {
            stop();
            SceneManager.LoadScene("ClearGoodScene");
        }
        if (this.point - this.time == 0) //���͸� 0�϶�
        {
            stop();
            SceneManager.LoadScene("ClearBadScene2");
        }
    }
   

    // �� �浹 OR ���͸� 0% OR ���͸� �������� ���� ��������
    public void stop()
    {
        CancelInvoke("DecreaseBattery"); // �ݺ� ȣ�� ���
        Destroy(itemGenerator);
    }

    //UI ������Ʈ
    void UIupdate()
    {
        this.batteryUI_img.GetComponent<Image>().fillAmount = (float)(this.point - this.time) / 100;
        this.batteryUI_text.GetComponent<Text>().text = (this.point - this.time).ToString() + "%";
    }
}

