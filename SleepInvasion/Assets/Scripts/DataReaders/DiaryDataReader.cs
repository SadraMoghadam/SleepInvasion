using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryDataReader : MonoBehaviour
{
    private string fileName = "Data/DiaryData";
    public List<DiaryData> diaryData;
    
    private void Awake()
    {
        diaryData = ReadDiaryData(fileName);
    }

    private List<DiaryData> ReadDiaryData(string filename)
    {
        int currentLineNumber = 0;
        int columnCount = 0;
        TextAsset txt = Resources.Load<TextAsset>(filename);
        string[] lines = txt.text.Split('\n');
        diaryData = new List<DiaryData>();
        foreach (string line in lines)
        {
            if (line == "")
            {
                continue;
            }

            string[] lineSplitted = line.Split(',');
            currentLineNumber++;

            if (currentLineNumber == 1)
            {
                continue;
            }

            int id = int.Parse(lineSplitted[0]);
            string diary = lineSplitted[1];

            DiaryData data = new DiaryData(id, diary);
            diaryData.Add(data);
        }

        return diaryData;
    }

    public DiaryData GetDiaryData(int id)
    {
        return diaryData[id];
    }
    
}
