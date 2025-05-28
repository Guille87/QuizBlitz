using UnityEngine;

[CreateAssetMenu(fileName = "New QuestionSO", menuName = "Scriptable Objects/QuestionSO")]
public class QuestionSO : ScriptableObject
{
    [SerializeField]
    [TextArea(3, 10)]
    [Tooltip("Texto de la pregunta")]
    string question = "Añade aquí el texto de la pregunta";
    public string Question => question;

    // Respuestas
    [SerializeField]
    string[] answers = new string[4];
    public string GetAnswers(int i) => answers[i];

    // Índice de la respuesta correcta
    [SerializeField][Range(0, 3)]
    [Tooltip("Índice de la respuesta correcta (0-3)")]
    int correctAnswerIndex = 0;
    public int CorrectAnswerIndex => correctAnswerIndex;
}
