using System;
using System.IO;
using UnityEngine;
using UnityEditor;

public class StatisticLine : MonoBehaviour
{
    private void Start()
    {
        PrintTotalLine();    
    }

    private static void PrintTotalLine()
    {
        string[] fileName = Directory.GetFiles("Assets/Scripts", "*.cs", SearchOption.AllDirectories);

        int totalLine = 0;
        foreach (var temp in fileName)
        {
            int nowLine = 0;
            StreamReader sr = new StreamReader(temp);
            while (sr.ReadLine() != null)
            {
                nowLine++;
            }

            totalLine += nowLine;
        }

        Debug.Log(String.Format("Total code lines: {0}", totalLine));
    }
}
