using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject batteryUI;
    GameObject batterypoint;
    int point = 50;

    public void Getelectric()
    {
        Debug.Log("getelectric ����" + this.point);
        this.point += 2;
        this.batteryUI.GetComponent<Image>().fillAmount = this.point/100;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.batteryUI = GameObject.Find("battery1");
        InvokeRepeating("DecreaseBattery", 1f, 1f); // 1�� �ĺ��� ���ʸ��� ����
        this.batterypoint = GameObject.Find("point");
        this.batteryUI.GetComponent<Image>().fillAmount = this.point / 100;

    }
    public void DecreaseBattery()
    {
        Debug.Log(this.point);
        this.point -= 1;
        this.batteryUI.GetComponent<Image>().fillAmount = this.point/100;
    }
    // Update is called once per frame

    public void stop()
    {
        CancelInvoke("DecreaseBattery"); // �ݺ� ȣ�� ���
    }
    public void update()
    {
        this.batterypoint.GetComponent<Text>().text= this.point.ToString()+"point";
    }
}

