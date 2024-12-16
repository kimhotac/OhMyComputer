using TMPro; // TextMeshPro�� ����ϱ� ���� ���ӽ����̽�
using UnityEngine;

public class finalPoint : MonoBehaviour
{
    public TextMeshProUGUI finalPoint_Text; // UI �ؽ�Ʈ�� ������ ����

    void Start()
    {
        // PlayerPrefs���� ����� ������ �ҷ��ɴϴ�. �⺻���� 0�Դϴ�.
        int score = PlayerPrefs.GetInt("Score", 0);

        // UI �ؽ�Ʈ�� ������ ǥ���մϴ�.
        if (finalPoint_Text != null)
        {
            finalPoint_Text.text = "Score: " + score.ToString();
        }
        else
        {
            Debug.LogError("ScoreText�� ������� �ʾҽ��ϴ�. Inspector���� �����ϼ���.");
        }
    }
}