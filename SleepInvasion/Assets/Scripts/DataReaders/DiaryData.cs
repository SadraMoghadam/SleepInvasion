using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryData
{
    public int Id;
    public string Diary;

    public DiaryData(int id, string diary)
    {
        Id = id;
        Diary = diary;
    }

    public override string ToString()
    {
        return "name: " + Diary;
    }
}
