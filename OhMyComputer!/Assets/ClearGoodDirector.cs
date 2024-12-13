using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearGooddirector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int score = PlayerPrefs.GetInt("Score", 0); // ����� ���� �ҷ�����, �⺻���� 0
        Debug.Log("Score from previous scene: " + score);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.GetKeyDown(KeyCode.Space)))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
