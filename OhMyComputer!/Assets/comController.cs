using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;
    float speed = 0;
    Vector2 startPos;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "water")
        {
            Debug.Log("Tag=water");
        }
        else
        {
            Debug.Log("Tag=electric");
        }
    }
    void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
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
            || (objPosition.x < -screenBounds.x && this.speed < 0))
        {
            this.speed =-this.speed;
        }
    }
}
