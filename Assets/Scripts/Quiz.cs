using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;

    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Image timerImage;

    [SerializeField] TextMeshProUGUI scoreText;
    Score score;

    bool hasAnsweredEarly;
    Timer timer;
    void Start()
    {
        scoreText.text = "Score: 0%";
        timer = FindFirstObjectByType<Timer>();
        score = FindFirstObjectByType<Score>();
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            nextQuestion();
            timer.loadNextQuestion = false;
            hasAnsweredEarly = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            displayAnswer(-1);
            setButtonState(false);
        }
    }

    void displayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = currentQuestion.GetAnswer(i);
            }
        }
    }

    void nextQuestion()
    {
        if (questions.Count > 0)
        {
            setButtonState(true);
            setDefaultButtonSprites();
            getRandomQuestion();
            displayQuestion();
            score.IncrementQuestionsSeen();
        }
    }
    
    void getRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    public void onAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        displayAnswer(index);
        setButtonState(false);
        timer.cancelTimer();
        scoreText.text = "Score: " + score.GetScorePercentage() + "%";
    }

    void displayAnswer(int index)
    {
        Image buttonImage;
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct Answer!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            score.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            questionText.text = "Wrong Answer! Correct answer is: " + currentQuestion.GetAnswer(correctAnswerIndex);
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void setButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void setDefaultButtonSprites()
    {
        Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = defaultAnswerSprite;
    }
}
