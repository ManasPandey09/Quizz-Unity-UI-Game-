using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool isAnsweringQuestion;
    float timerValue;
    [SerializeField] float timeForQuestion = 30f;
    [SerializeField] float timeForAnswer = 10f;
    public float fillFraction;
    public bool loadNextQuestion;
    void Update()
    {
        updateTimer();
    }

    public void cancelTimer()
    {
        timerValue = 0f;
    }

    void updateTimer()
    {
        timerValue -= Time.deltaTime;
        if (isAnsweringQuestion)
        {
            if (timerValue > 0f)
            {
                fillFraction = timerValue / timeForQuestion;
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeForAnswer;
            }
        }
        else
        {   if (timerValue > 0f)
            {
                fillFraction = timerValue / timeForAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeForQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
