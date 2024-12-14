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
    float objectHalfWidth; // ������Ʈ ���� �ʺ�
    AudioSource aud;// ��(��)�׸��� ������
    public Vector3 midPoint1;   // �߰� �� 1 (�ܺο��� �����Ǵ� ��ǥ)
    public Vector3 midPoint2;    // �߰� �� 2 (�ܺο��� �����Ǵ� ��
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
        lineRenderer.positionCount = 4; // 4���� ��
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;    // ���� ���� (1C2435)
        Color lineColor = new Color(0x1C / 255f, 0x24 / 255f, 0x35 / 255f); // RGB�� 0~1�� ��ȯ
        lineRenderer.startColor = lineColor; // ���� ������ ����
        lineRenderer.endColor = lineColor;  // ���� ���� ����
        Material lineMaterial = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material = lineMaterial;
        lineRenderer.SetPosition(0, new Vector3(-3, -5.5f, 0)); //������
        lineRenderer.SetPosition(3, new Vector3(3, -5.5f, 0)); //����
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
            transform.Translate(-2, 0, 0);
            this.hand.GetComponent<hand>().transform.Translate(-2, 0, 0);
        }
        //������ ȭ��ǥ�� ��������
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

        // ���� ��ȯ
        Vector3 objPosition = transform.position;
        if ((objPosition.x > screenBounds.x - this.objectHalfWidth && this.speed > 0)
            || (objPosition.x < -screenBounds.x + this.objectHalfWidth && this.speed < 0)) // ȭ�� �ٱ����� ������ �Ǿ��� ���
        {
            this.speed = -this.speed;
            this.aud.PlayOneShot(this.comSE);
            animator.SetTrigger("bounce");
        }

        // �̵�
        transform.Translate(this.speed, 0, 0);  // �̵�
        this.hand.GetComponent<hand>().transform.Translate(this.speed, 0, 0);
        Vector3 offset1 = new Vector3(-1, -1, 0);
        lineRenderer.SetPosition(1, transform.position + offset1);           // �߰��� 1
        Vector3 offset2 = new Vector3(1, -1, 0);
        lineRenderer.SetPosition(2, transform.position + offset2);           // �߰��� 2
        this.speed *= 0.98f;                    // ����

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