using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO question;

    [SerializeField] TextMeshProUGUI questionText;

    [SerializeField] GameObject[] answerButtons;

    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    void Start()
    {
        StartQuestion();
    }

    void StartQuestion()
    {
        GetNextQuestion();
    }

    void GetNextQuestion()
    {
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        // Texto de la pregunta
        questionText.text = question.Question;

        // Respuestas
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.GetAnswers(i);
        }

        // Resetea los sprites de las respuestas
        SetDefaultAnswerSprites();

        // Resetea el estado de los botones
        SetButtonState(true);
    }

    public void OnAnswerSelected(int index)
    {
        if (index == question.CorrectAnswerIndex)
        {
            questionText.text = "¡Respuesta correcta!";
        }
        else
        {
            questionText.text = "Respuesta incorrecta";
        }

        answerButtons[question.CorrectAnswerIndex].GetComponentInChildren<Image>().sprite = correctAnswerSprite;

        SetButtonState(false);
    }

    void SetButtonState(bool state)
    {
        foreach (var button in answerButtons)
        {
            button.GetComponent<Button>().interactable = state;
        }
    }

    void SetDefaultAnswerSprites()
    {
        foreach (var button in answerButtons)
        {
            button.GetComponentInChildren<Image>().sprite = defaultAnswerSprite;
        }
    }
}
