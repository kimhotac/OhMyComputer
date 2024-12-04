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
        this.point += 2;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.batteryUI = GameObject.Find("battery1");
        InvokeRepeating("DecreaseBattery", 1f, 1f); // 1초 후부터 매초마다 실행
        this.batterypoint = GameObject.Find("point");

    }
    public void DecreaseBattery()
    {
        this.batteryUI.GetComponent<Image>().fillAmount -= 0.1f;
    }
    // Update is called once per frame

    public void stop()
    {
        CancelInvoke("DecreaseBattery"); // 반복 호출 취소
    }
    public void update()
    {
        this.batterypoint.GetComponent<Text>().text= this.point.ToString()+"point";
    }
}

