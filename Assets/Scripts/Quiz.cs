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

    Timer timer;
    bool gotAnswered;

    void Start()
    {
        timer = GetComponent<Timer>();

        StartQuestion();
    }

    void StartQuestion()
    {
        GetNextQuestion();

        timer.StartTimer();

        gotAnswered = false;
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

        ShowCorrectAnswer();

        SetButtonState(false);

        gotAnswered = true;

        timer.CancelTimer();
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

    void Update()
    {
        if (!gotAnswered && timer.State == Timer.TimerState.Reviewing)
        {
            questionText.text = "Tiempo agotado";

            SetButtonState(false);

            ShowCorrectAnswer();
        }
        else if (timer.State == Timer.TimerState.ReviewEnded)
        {
            StartQuestion();
        }
    }

    void ShowCorrectAnswer()
    {
        answerButtons[question.CorrectAnswerIndex].GetComponentInChildren<Image>().sprite = correctAnswerSprite;
    }
}
