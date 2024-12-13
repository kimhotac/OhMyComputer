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
    //현재 배터리는 point - time

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        this.batteryUI_img = GameObject.Find("battery_img");
        this.batteryUI_text = GameObject.Find("battery_text");
        this.itemGenerator = GameObject.Find("dropItemGenerator");
        UIupdate();
        InvokeRepeating("DecreaseBattery", 1f, 1f); // 1초 후부터 매초마다 실행
    }
    void Update()
    {
        this.itemGenerator.GetComponent<dropItemGenerator>().SetParameter(
            -((float)(point - time) / 500f + 0.05f),// 속도
            0.75f - ((float)(point - time) / 1000) // 확률
            );
    }

    // 전기 충돌
    public void Getelectric()
    {
        this.point += 3;
        UIupdate();
    }

    // 시간 증가
    public void DecreaseBattery()
    {
        this.time++;
        UIupdate();
        if (this.point - this.time >= 100) //배터리가 100보다 클떄
        {
            stop();
            SceneManager.LoadScene("ClearGoodScene");
        }
        if (this.point - this.time == 0) //배터리 0일때
        {
            stop();
            SceneManager.LoadScene("ClearBadScene2");
        }
    }


    // 물 충돌 OR 배터리 0% OR 배터리 완충으로 인한 게임종료
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

