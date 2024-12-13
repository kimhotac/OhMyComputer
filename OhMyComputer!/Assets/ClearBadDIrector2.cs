using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearBadScene2 : MonoBehaviour
{
    int score;
    // Start is called before the first frame update
    void Start()
    {
        this.score = PlayerPrefs.GetInt("Score", 0); // 저장된 점수 불러오기, 기본값은 0
        Debug.Log("Score from previous scene: " + this.score);

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