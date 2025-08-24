using UnityEngine;

public class Score : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    public int GetScorePercentage()
    {
        if (questionsSeen == 0) return 0;
        return Mathf.RoundToInt((float)correctAnswers / questionsSeen * 100);
    }
}
