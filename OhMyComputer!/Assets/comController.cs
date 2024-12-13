using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class comController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;
    GameObject director;
    float speed = 0;
    Vector2 startPos;
    public AudioClip elecSE;    //전기소리
    public AudioClip batterySE;  //충전소리
    public AudioClip comSE; 
    AudioSource aud;

    void Start()
    {
        this.director = GameObject.Find("GameDirector");
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Application.targetFrameRate = 60;
        this.aud = GetComponent<AudioSource>();
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
            this.aud.PlayOneShot(this.comSE);
        }

        GetComponent<AudioSource>().Play();
    }

    // 2D충돌판정
    void OnTriggerEnter2D(Collider2D other)
    {

        //물인 경우
        if (other.gameObject.tag == "water")
        {
            Debug.Log("지지직");
            this.director.GetComponent<GameDirector>().stop();
            SceneManager.LoadScene("ClearBadScene");
            
        }
        // 나머지 경우
        else
        {
            this.director.GetComponent<GameDirector>().Getelectric();
            this.aud.PlayOneShot(this.batterySE);
        }
        Destroy(other.gameObject);

    }
}