using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;


public class DiaryDataReader : MonoBehaviour
{
    private string fileName = "Data/DiaryData";
    public List<DiaryData> diaryData;
    private string[] texts = new []
    {
        "It’s been a month that we’ve been rebuilding the village after the earthquake. A watch was found under the ruins, and the time on that watch got stuck on the time that disaster happened. This time need to be remembered, in memorial for every villager who suffered from the earthquake.",
        "It’s been a month that we’ve been rebuilding the village after the earthquake. A special watch was found under the ruins, and the time on that watch got stuck on the time that disaster happened. This time need to be remembered, in memorial for every villager who suffered from the earthquake.",
        "Technology has brought our people prosperity. We’ve had a breakthrough in transforming objects into different size.For now it can be used on only a few things but it has big potentiality.",
        "Maya people used sundial to tell time. It needs to be placed in the correct place to see the shadow.So far, we have collected 4 sundials in which the symbols matches the Maya stone.",
        "The king of the village had 6 beautiful daughters. As he was building the castle, he dedicated each column to each daughter, to represent them keeping the family together. When the king died, only one princess realized the truth, and kept it safe to this day...",
        "There is a special room: when you enter it, something is different...Your mind must be the opposite of the clock!"
    };
    // private string[] texts = new []
    // {
    //     "Venus calendar:\nYear 102 Month 18 Date 02\n\nToday is a day with surprise, our people have found something very special. There was a big breakthrough In the Maya temple which we’ve been graving for months,",
    //     "Weather: Sunny\nMood: Excited\n\nand the core of the temple has finally been reached. On the top of the central hall, there was a floating stone. There’s so much to know about Maya civilization, so I decided to put the stone in my room to study it better.",
    //     "Venus calendar:\nYear 102 Month 18 Date 20\n\nToday I’ve found out the mathematics knowledge that Maya people knew. They have such fascinating way to calculate.",
    //     "Weather: Sunny\nMood: Peaceful\n\nHere is the rules of Maya number. Maya Math uses three symbols: dot, stick and shell. In our number, •| = 6, •••|| = 13, •|||=16. And my birthday date in Maya calendar would be •••|, ••••|, •, -, •, •",
    //     "Venus calendar:\nYear 102 Month 19 Date 11\n\nCuriosity has brought prosperity to our empire. Our people have been exploring the way to see the unseen, and I’m so proud that we have the technique to see what’s hidden behind the reality.",
    //     "Weather: Cloudy\nMood: Pleased\n\nWith the eye of our angel animal, truth of the Maya stone has been revealed. And a brighter future is waiting ahead of us through the white gate."
    // };
    
    private void Awake()
    {
        diaryData = DirtyReadDiaryData(fileName);
    }

    private List<DiaryData> DirtyReadDiaryData(string filename)
    {
        diaryData = new List<DiaryData>();
        for (int i = 0; i < texts.Length; i++)
        {
            int id = i;
            string diary = texts[i];
            DiaryData data = new DiaryData(id, diary);
            diaryData.Add(data);
        }

        return diaryData;
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
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            string[] lineSplitted = CSVParser.Split(line);
            currentLineNumber++;

            if (currentLineNumber == 1)
            {
                continue;
            }

            int id = int.Parse(lineSplitted[0]);
            string diary = lineSplitted[1];
            if (diary[0].Equals('\"'))
            {
                diary.Trim('\"');
            }
            // else if (diary[^1].Equals('"'))
            // {
            //     diary.Remove(diary.Length - 1);
            // }

            DiaryData data = new DiaryData(id, diary);
            diaryData.Add(data);
        }

        return diaryData;
    }

    // private List<DiaryData> ReadDiaryData(string filename)
    // {
    //     int currentLineNumber = 0;
    //     int columnCount = 0;
    //     TextAsset txt = Resources.Load<TextAsset>(filename);
    //     string[] lines = txt.text.Split('\n');
    //     diaryData = new List<DiaryData>();
    //     foreach (string line in lines)
    //     {
    //         if (line == "")
    //         {
    //             continue;
    //         }
    //
    //         string[] lineSplitted = line.Split(new[] { "," }, StringSplitOptions.None);
    //         currentLineNumber++;
    //
    //         if (currentLineNumber == 1)
    //         {
    //             continue;
    //         }
    //
    //         int id = int.Parse(lineSplitted[0]);
    //         string diary = lineSplitted[1];
    //         // if (diary.Contains("\""))
    //         // {
    //         //     diary = diary.Replace("\"", "\"\"");
    //         //     diary = String.Format("\"{0}\"", diary);
    //         // }
    //         // else if (diary.Contains(",") || diary.Contains(System.Environment.NewLine))
    //         // {
    //         //     diary = String.Format("\"{0}\"", diary);
    //         // }
    //
    //         DiaryData data = new DiaryData(id, diary);
    //         diaryData.Add(data);
    //     }
    //
    //     return diaryData;
    // }

    public DiaryData GetDiaryData(int id)
    {
        return diaryData[id];
    }
    
}
