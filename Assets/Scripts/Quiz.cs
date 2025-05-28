using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] List<QuestionSO> questions;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider progressBar;

    [SerializeField] GameObject[] answerButtons;

    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] GameManager gameManager;

    Timer timer;
    bool gotAnswered;
    QuestionSO question;
    Score score;

    void Start()
    {
        timer = GetComponent<Timer>();
        score = GetComponent<Score>();

        InitializeProgressBar();

        StartQuestion();
    }

    void InitializeProgressBar()
    {
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void StartQuestion()
    {
        GetNextQuestion();

        timer.StartTimer();

        gotAnswered = false;
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            GetRandomQuestion();
            DisplayQuestion();
            score.AddQuestionVisited();
            progressBar.value = score.QuestionsVisited;
        }
        else
        {
            //questionText.text = "¡No hay más preguntas!";
            gameManager.GameOver();
        }

    }

    void GetRandomQuestion()
    {
        int randomIndex = Random.Range(0, questions.Count);
        question = questions[randomIndex];

        // Elimina la pregunta seleccionada de la lista para no repetirla
        questions.RemoveAt(randomIndex);
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

            score.AddCorrectAnswer();
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

        ShowScore();
    }

    void ShowCorrectAnswer()
    {
        answerButtons[question.CorrectAnswerIndex].GetComponentInChildren<Image>().sprite = correctAnswerSprite;
    }

    void ShowScore()
    {
        //scoreText.text = "Puntuación: " + Mathf.RoundToInt(score.CorrectAnswers / (float)score.QuestionsVisited * 100) + "%";
        scoreText.text = "Puntuación: " + score.CorrectAnswers + "/" + score.QuestionsVisited;
    }
}
