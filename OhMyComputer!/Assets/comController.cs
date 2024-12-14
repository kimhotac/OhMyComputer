using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class comController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;
    GameObject director;
    GameObject hand;
    float speed = 0;
    Vector2 startPos;
    public AudioClip batterySE;  //충전소리
    public AudioClip comSE;
    private Animator animator;  // Animator 컴포넌트
    SpriteRenderer spriteRenderer;
    float objectHalfWidth; // 오브젝트 절반 너비
    AudioSource aud;// 선(팔)그리는 변수들
    public Vector3 midPoint1;   // 중간 점 1 (외부에서 제공되는 좌표)
    public Vector3 midPoint2;    // 중간 점 2 (외부에서 제공되는 좌
    public LineRenderer lineRenderer;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectHalfWidth = spriteRenderer.bounds.size.x / 2;
        this.director = GameObject.Find("GameDirector");
        this.hand = GameObject.Find("hand");
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Application.targetFrameRate = 60;
        this.aud = GetComponent<AudioSource>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 4; // 4개의 점
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;    // 색상 설정 (1C2435)
        Color lineColor = new Color(0x1C / 255f, 0x24 / 255f, 0x35 / 255f); // RGB를 0~1로 변환
        lineRenderer.startColor = lineColor; // 선의 시작점 색상
        lineRenderer.endColor = lineColor;  // 선의 끝점 색상
        Material lineMaterial = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material = lineMaterial;
        lineRenderer.SetPosition(0, new Vector3(-3, -5.5f, 0)); //시작점
        lineRenderer.SetPosition(3, new Vector3(3, -5.5f, 0)); //끝점
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
            transform.Translate(-2, 0, 0);
            this.hand.GetComponent<hand>().transform.Translate(-2, 0, 0);
        }
        //오른쪽 화살표가 눌렸을때
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(2, 0, 0);
            this.hand.GetComponent<hand>().transform.Translate(-2, 0, 0);
        }


        if (this.speed < -0.1f || 0.1 < this.speed)
        {
            animator.SetBool("move", true);
            if (this.speed < 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }

        }
        else
        {
            animator.SetBool("move", false);
        }

        // 방향 전환
        Vector3 objPosition = transform.position;
        if ((objPosition.x > screenBounds.x - this.objectHalfWidth && this.speed > 0)
            || (objPosition.x < -screenBounds.x + this.objectHalfWidth && this.speed < 0)) // 화면 바깥으로 나가게 되었을 경우
        {
            this.speed = -this.speed;
            this.aud.PlayOneShot(this.comSE);
            animator.SetTrigger("bounce");
        }

        // 이동
        transform.Translate(this.speed, 0, 0);  // 이동
        this.hand.GetComponent<hand>().transform.Translate(this.speed, 0, 0);
        Vector3 offset1 = new Vector3(-1, -1, 0);
        lineRenderer.SetPosition(1, transform.position + offset1);           // 중간점 1
        Vector3 offset2 = new Vector3(1, -1, 0);
        lineRenderer.SetPosition(2, transform.position + offset2);           // 중간점 2
        this.speed *= 0.98f;                    // 감속

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

        }
        // 나머지 경우
        else
        {
            this.director.GetComponent<GameDirector>().Getelectric();
            this.aud.PlayOneShot(this.batterySE);
            animator.SetTrigger("getE");
        }
        Destroy(other.gameObject);

    }
}