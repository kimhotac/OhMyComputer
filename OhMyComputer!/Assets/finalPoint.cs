using TMPro; // TextMeshPro를 사용하기 위한 네임스페이스
using UnityEngine;

public class finalPoint : MonoBehaviour
{
    public TextMeshProUGUI finalPoint_Text; // UI 텍스트를 연결할 변수

    void Start()
    {
        // PlayerPrefs에서 저장된 점수를 불러옵니다. 기본값은 0입니다.
        int score = PlayerPrefs.GetInt("Score", 0);

        // UI 텍스트에 점수를 표시합니다.
        if (finalPoint_Text != null)
        {
            finalPoint_Text.text = "Score: " + score.ToString();
        }
        else
        {
            Debug.LogError("ScoreText가 연결되지 않았습니다. Inspector에서 연결하세요.");
        }
    }
}
