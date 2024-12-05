using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject batteryUI_img;
    GameObject batteryUI_text;
    GameObject itemGenerator;
    int point = 50;
    int time = 0;
    //현재 배터리는 point - time

    // Start is called before the first frame update
    void Start()
    {
        this.batteryUI_img = GameObject.Find("battery_img");
        this.batteryUI_text = GameObject.Find("battery_text");
        this.itemGenerator = GameObject.Find("dropItemGenerator");
        UIupdate();
        InvokeRepeating("DecreaseBattery", 1f, 1f); // 1초 후부터 매초마다 실행
    }

    // 전기 충돌
    public void Getelectric()
    {
        this.point += 2;
        UIupdate();
    }

    // 시간 증가
    public void DecreaseBattery()
    {
        this.time++;
        UIupdate();
    }
    // Update is called once per frame

    // 물 충돌
    public void stop()
    {
        CancelInvoke("DecreaseBattery"); // 반복 호출 취소
        Destroy(itemGenerator);
    }

    //UI 업데이트
    void UIupdate()
    {
        this.batteryUI_img.GetComponent<Image>().fillAmount = (float)(this.point - this.time) / 100;
        this.batteryUI_text.GetComponent<Text>().text = (this.point - this.time).ToString() + "%";
    }
}

