using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter question text here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;

    public string GetQuestion()
    {
        return question;
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

    public string GetAnswer(int index)
    {
        if (index < 0 || index >= answers.Length)
        {
            Debug.LogError("Index out of range");
            return null;
        }
        return answers[index];
    }
}
