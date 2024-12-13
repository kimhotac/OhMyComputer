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
    public AudioClip elecSE;    //����Ҹ�
    public AudioClip batterySE;  //�����Ҹ�
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
        }
        //������ ȭ��ǥ�� ��������
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(2,0,0);
        }


        transform.Translate(this.speed, 0, 0);  // �̵�
        this.speed *= 0.98f;                    // ����

        Vector3 objPosition = transform.position;

        if ((objPosition.x > screenBounds.x && this.speed > 0)
            || (objPosition.x < -screenBounds.x && this.speed < 0)) // ȭ�� �ٱ����� ������ �Ǿ��� ���
        {
            this.speed =-this.speed;
            this.aud.PlayOneShot(this.comSE);
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
            SceneManager.LoadScene("ClearBadScene");
            
        }
        // ������ ���
        else
        {
            this.director.GetComponent<GameDirector>().Getelectric();
            this.aud.PlayOneShot(this.batterySE);
        }
        Destroy(other.gameObject);

    }
}