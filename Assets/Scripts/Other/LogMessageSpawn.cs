using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LogMessageSpawn : MonoBehaviour
{
    [Header("LogMessage")]
    [SerializeField] GameObject prefabMessage;
    [SerializeField] GameObject parentSpawnMessage;

    [SerializeField] List<LogMessage> messagesList;

    public void GetTextMessageInLog(bool isSolder,string message)
    {
        if(messagesList.Count>0)
        {
            if (isSolder && messagesList.Last().IsSolderMessageLast())
            {
                Debug.Log("Повторяющееся сообщение ");
            }
            else
            {
                SpawnMessage(isSolder, message);
            }
        }
        else
        {
            SpawnMessage(isSolder, message);
        }
    }
    private void SpawnMessage(bool isSolder, string message)
    {
        GameObject logMessage = Instantiate(prefabMessage, parentSpawnMessage.transform);
        logMessage.GetComponent<LogMessage>().SetMessageText(isSolder, message);
        messagesList.Add(logMessage.GetComponent<LogMessage>());
    }
}
