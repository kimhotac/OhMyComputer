using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject batteryUI;
    // Start is called before the first frame update
    void Start()
    {
        this.batteryUI = GameObject.Find("battery1");
        InvokeRepeating("DecreaseBattery", 1f, 1f); // 1�� �ĺ��� ���ʸ��� ����
    }
    public void DecreaseBattery()
    {
        this.batteryUI.GetComponent<Image>().fillAmount -= 0.1f;
    }
    // Update is called once per frame

    void stop()
    {
        CancelInvoke("DecreaseBattery"); // �ݺ� ȣ�� ���
    }
    void update()
    {

    }
}

