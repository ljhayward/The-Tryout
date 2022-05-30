using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Message", fileName = "New Message")]
public class MessageSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string message = "Enter new message text here";
    //[SerializeField] string[] answers = { "Charlie", "Kimba", "Harvey", "Mercury", "Jeffrey" };
    //[SerializeField] int correctAnswerIndex;

    public string GetMessage()
    {
        return message;
    }

    //public int GetCorrectAnswerIndex()
    //{
    //    return correctAnswerIndex;
    //}

    //public string GetAnswer(int index)
    //{
    //    return answers[index];
    //}
}