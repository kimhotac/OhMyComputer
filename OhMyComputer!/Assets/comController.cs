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
    public AudioClip batterySE;  //�����Ҹ�
    public AudioClip comSE;
    private Animator animator;  // Animator ������Ʈ
    SpriteRenderer spriteRenderer;
    AudioSource aud;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.director = GameObject.Find("GameDirector");
        this.hand = GameObject.Find("hand");
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Application.targetFrameRate = 60;
        this.aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // �������� ��Ʈ��
        // ���������� ���̸� ���Ѵ�
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺�� Ŭ���� ��ǥ
            this.startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // ���콺�� ������ �� ��ǥ
            Vector2 endPos = Input.mousePosition;
            float swipeLength = endPos.x - this.startPos.x;

            // �������� ���̸� ó�� �ӵ��� ��ȯ�Ѵ�
            this.speed = swipeLength / 500.0f;
        }

        //Ű���� ��Ʈ��
        //���� ȭ��ǥ�� ��������
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(-2,0,0);
            this.hand.GetComponent<hand>().transform.Translate(-2, 0, 0);
        }
        //������ ȭ��ǥ�� ��������
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(2,0,0);
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


        transform.Translate(this.speed, 0, 0);  // �̵�
        this.hand.GetComponent<hand>().transform.Translate(this.speed, 0, 0);
        this.speed *= 0.98f;                    // ����

        Vector3 objPosition = transform.position;

        if ((objPosition.x > screenBounds.x && this.speed > 0)
            || (objPosition.x < -screenBounds.x && this.speed < 0)) // ȭ�� �ٱ����� ������ �Ǿ��� ���
        {
            this.speed =-this.speed;
            this.aud.PlayOneShot(this.comSE);
            animator.SetTrigger("bounce");
        }

        GetComponent<AudioSource>().Play();
    }

    // 2D�浹����
    void OnTriggerEnter2D(Collider2D other)
    {

        //���� ���
        if (other.gameObject.tag == "water")
        {
            Debug.Log("������");
            this.director.GetComponent<GameDirector>().stop();
            
        }
        // ������ ���
        else
        {
            this.director.GetComponent<GameDirector>().Getelectric();
            this.aud.PlayOneShot(this.batterySE);
            animator.SetTrigger("getE");
        }
        Destroy(other.gameObject);

    }
}