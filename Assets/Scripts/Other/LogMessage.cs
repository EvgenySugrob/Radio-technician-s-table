using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogMessage : MonoBehaviour
{
    [SerializeField] TMP_Text textMessage;
    [SerializeField] bool isSolderPressedMessage;

    public void SetMessageText(bool isSolder,string message)
    {
        isSolderPressedMessage = isSolder;
        textMessage.text = message;
    }
    public bool IsSolderMessageLast()
    {
        return isSolderPressedMessage;
    }
}
