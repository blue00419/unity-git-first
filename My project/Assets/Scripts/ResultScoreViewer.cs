using UnityEngine;
using TMPro;

public class ResultScoreViewer : MonoBehaviour
{
    private TextMeshProUGUI textResultScore;

    private void Awake(){
        textResultScore = GetComponent<TextMeshProUGUI>();

        int score = PlayerPrefs.GetInt("Score");

        textResultScore.text = "Result Score "+score;
    }
}
