using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueData
{
    public int Id;
    public string Dialogue;
    public int NextId;

    public DialogueData(int id, string dialogue, int nextId)
    {
        Id = id;
        Dialogue = dialogue;
        NextId = nextId;
    }

    public override string ToString()
    {
        return "dialogue: " + Dialogue;
    }
}
