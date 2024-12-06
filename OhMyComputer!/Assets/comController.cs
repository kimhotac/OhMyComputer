using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;
    GameObject director;
    float speed = 0;
    Vector2 startPos;

    void Start()
    {
        this.director = GameObject.Find("GameDirector");
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

        // 스와이프 컨트롤
        // 스와이프의 길이를 구한다
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스를 클릭한 좌표
            this.startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 마우스를 떼었을 때 좌표
            Vector2 endPos = Input.mousePosition;
            float swipeLength = endPos.x - this.startPos.x;

            // 스와이프 길이를 처음 속도로 변환한다
            this.speed = swipeLength / 500.0f;
        }

        //키보드 컨트롤
        //왼쪽 화살표가 눌렸을때
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(-2,0,0);
        }
        //오른쪽 화살표가 눌렸을때
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(2,0,0);
        }


        transform.Translate(this.speed, 0, 0);  // 이동
        this.speed *= 0.98f;                    // 감속

        Vector3 objPosition = transform.position;

        if ((objPosition.x > screenBounds.x && this.speed > 0)
            || (objPosition.x < -screenBounds.x && this.speed < 0)) // 화면 바깥으로 나가게 되었을 경우
        {
            this.speed =-this.speed;
        }
    }

    // 2D충돌판정
    void OnTriggerEnter2D(Collider2D other)
    {
        //물인 경우
        if (other.gameObject.tag == "water")
        {
            this.director.GetComponent<GameDirector>().stop();
        }
        // 나머지 경우
        else
        {
            this.director.GetComponent<GameDirector>().Getelectric();
        }
        Destroy(other.gameObject);
    }
}