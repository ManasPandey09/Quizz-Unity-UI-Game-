using UnityEngine;
using TMPro;
public class EndScreen : MonoBehaviour
{
    Score score;
    [SerializeField] TextMeshProUGUI finalScoreText;
    void Awake()
    {
        score = FindFirstObjectByType<Score>();
    }

    public void showFinalScore()
    {
        finalScoreText.text = "Congratulations!\n You got a score of " + score.GetScorePercentage() + "%";
    }
}
