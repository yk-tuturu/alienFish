using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;  
using System.IO;  
using System.Text;

public class Dialogue_trigger : MonoBehaviour
{
    public List<Dialogue> story = new List<Dialogue>();
    public DialogueManager dialogueManager;
    public UnityEvent onIntroComplete;
    public UnityEvent onAttackLanded;
    public UnityEvent onAttackFinished;

    void Start()
    {
        TriggerDialogue("overLordDialogue");
    }

    public void TriggerDialogue(string filename)
    {
        ReadFile(filename);

        // fix this later
        UnityEvent onComplete = new UnityEvent();

        dialogueManager.StartDialogue(story);
        Debug.Log("dialogueTriggered " + filename);
        story = new List<Dialogue>();
    }

    public void ReadFile(string filename)
    {
        string path = "Assets/Resources/Story/" + filename + ".txt";
        using (FileStream fs = File.OpenRead(path))  
        {  
            string[] readText = File.ReadAllLines(path);
            int counter = 1;
            Dialogue temp = new Dialogue();
            foreach (string s in readText)
            {
                if (counter == 1)
                {
                    temp.speaker = s;
                }
                else if (counter == 2)
                {
                    temp.sentence = s;
                    
                }
                else if (counter == 3)
                {
                    temp.commands = s.Split(", ");
                    story.Add(temp);
                    temp = new Dialogue();
                    counter = 0;
                }
                counter++;
            }
        }  
    }
}
