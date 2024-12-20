﻿using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject batteryUI_img;
    GameObject batteryUI_text;
    GameObject itemGenerator;
    int point = 3;
    int time = 0;
    GameObject finalPointUI_text; //최종 점수
    //현재 배터리는 point - time

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        this.batteryUI_img = GameObject.Find("battery_img");
        this.batteryUI_text = GameObject.Find("battery_text");
        this.itemGenerator = GameObject.Find("dropItemGenerator");
        InvokeRepeating("DecreaseBattery", 1f, 1f); // 1초 후부터 매초마다 실행
        UIupdate();
    }

    void Update()
    {
        this.itemGenerator.GetComponent<dropItemGenerator>().SetParameter(
            (point - time) > 90 ? (0.002f * (point - time) - 0.37f) : -((point - time) / 1000f + 0.1f),// 속도
            (point - time) > 90 ? 0.80f : 0.75f - ((float)(point - time) / 1000) // 확률
            );
    }

    // 전기 충돌
    public void Getelectric()
    {
        this.point += 4;
        UIupdate();
    }

    // 시간 증가
    public void DecreaseBattery()
    {
        this.time++;
        UIupdate();
        if (this.point - this.time >= 100 || this.point - this.time == 0) //배터리가 100보다 클떄
            stop();
    }


    //게임종료
    public void stop()
    {
        CancelInvoke("DecreaseBattery"); // 반복 호출 취소
        this.itemGenerator.GetComponent<dropItemGenerator>().gen_flag = false;

        if (this.point - this.time >= 100) //배터리가 100보다 클떄
        {
            PlayerPrefs.SetInt("Score", 1000 - this.time);
            PlayerPrefs.Save();
            SceneManager.LoadScene("ClearGoodScene");
        }
        else if (this.point - this.time == 0) //배터리 0일때
        {
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.Save();
            SceneManager.LoadScene("ClearBadScene2");
        }
        else
        {
            PlayerPrefs.SetInt("Score", this.point - this.time);
            PlayerPrefs.Save();
            SceneManager.LoadScene("ClearBadScene");
        }
    }

    //UI 업데이트
    void UIupdate()
    {
        this.batteryUI_img.GetComponent<Image>().fillAmount = (float)(this.point - this.time) / 100;
        this.batteryUI_text.GetComponent<Text>().text = ((this.point - this.time) > 100 ? 100 : (this.point - this.time)).ToString() + "%";
    }
}

