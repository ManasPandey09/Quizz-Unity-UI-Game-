using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Image timerImage;

    bool hasAnsweredEarly;
    Timer timer;
    void Start()
    {
        timer = FindFirstObjectByType<Timer>();
        nextQuestion();
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
        questionText.text = question.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = question.GetAnswer(i);
            }
        }
    }

    void nextQuestion()
    {
        setButtonState(true);
        setDefaultButtonSprites();
        displayQuestion();
    }

    public void onAnswerSelected(int index)
    {   
        hasAnsweredEarly = true;
        displayAnswer(index);
        setButtonState(false);
        timer.cancelTimer();
    }

    void displayAnswer(int index)
    {
        Image buttonImage;
        if (index == question.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct Answer!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            correctAnswerIndex = question.GetCorrectAnswerIndex();
            questionText.text = "Wrong Answer! Correct answer is: " + question.GetAnswer(correctAnswerIndex);
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
