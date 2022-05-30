using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Message", fileName = "New Message")]
public class MessageSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string message = "Enter new message text here";

    public string GetMessage()
    {
        return message;
    }
}