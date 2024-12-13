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
    //���� ���͸��� point - time

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        this.batteryUI_img = GameObject.Find("battery_img");
        this.batteryUI_text = GameObject.Find("battery_text");
        this.itemGenerator = GameObject.Find("dropItemGenerator");
        UIupdate();
        InvokeRepeating("DecreaseBattery", 1f, 1f); // 1�� �ĺ��� ���ʸ��� ����
    }
    void Update()
    {
        this.itemGenerator.GetComponent<dropItemGenerator>().SetParameter(
            -((float)(point - time) / 500f + 0.05f),// �ӵ�
            0.75f - ((float)(point - time) / 1000) // Ȯ��
            );
    }

    // ���� �浹
    public void Getelectric()
    {
        this.point += 3;
        UIupdate();
    }

    // �ð� ����
    public void DecreaseBattery()
    {
        this.time++;
        UIupdate();
        if (this.point - this.time >= 100) //���͸��� 100���� Ŭ��
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

