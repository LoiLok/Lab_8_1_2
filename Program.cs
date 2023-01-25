using System;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics.SymbolStore;
using System.Timers;

class Table
{
    public static void WriteTable()
    {
        for (int i = 0; i <= 70; i++)
        {
            Console.Write("-");
        }
        for (int j = 1; j <= 13; j++)
        {
            Console.SetCursorPosition(0, j);
            Console.Write("|");
            Console.SetCursorPosition(70, j);
            Console.Write("|");
        }
        Console.SetCursorPosition(0, 14);
        for (int z = 0; z <= 70; z++)
        {
            Console.Write("-");
        }
    }
}
class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("Params.txt");
        int[] start_second = new int[lines.Length], stop_second = new int[lines.Length], all_seconds = new int[lines.Length];
        string[] position = new string[lines.Length], color = new string[lines.Length], text = new string[lines.Length];
        FillArraysWithValues(lines, start_second,stop_second, position, color, text);
        IdentifyAllSeconds(lines, start_second, stop_second, all_seconds);
        Table.WriteTable();
        FillTable(start_second, stop_second, position, color, text);
        Console.WriteLine();
        Console.WriteLine();
    }
    static void IdentifyAllSeconds(string[] lines, int[] start_second, int[] stop_second, int[] all_seconds)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            all_seconds[i] = stop_second[i] - start_second[i] + 1;
        }
    }
    static void FillTable(int[] start_second, int[] stop_second, string[] position, string[] color, string[] text)
    {
        int max = stop_second.Max();
        for (int i = 0; i < max + 1; i++)
        {
            for (int j = 0; j < start_second.Length; j++)
            {
                if (start_second[j] == i)
                {
                    Write(position[j], color[j], text[j]);
                }
                if (stop_second[j] == i)
                {
                    Delete(position[j], text[j]);
                }
            }
            Thread.Sleep(1000);
        }
    }
    static void FillArraysWithValues(string[] lines, int[] start_second, int[] stop_second, string[] position, string[] color, string[] text)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].ToString();
            string time = GetTime(line);
            int start = GetStart(time);
            int stop = GetStop(time);
            start_second[i] = start;
            stop_second[i] = stop;
            position[i] = GetPosition(line);
            color[i] = GetColor(line);
            text[i] = GetText(line);
        }
    }
    static string GetTime(string line)
    {
        line = line.Replace(" - ", " ");
        string time = line.Substring(0, 11);
        return time;
    }
    static int GetStart(string time)
    {
        time = time.Substring(0, 6);
        int start;
        int index = time.IndexOf(":");
        int minutes = Convert.ToInt32(time.Substring(0, index));
        int seconds = Convert.ToInt32(time.Substring(3));
        start = minutes * 60 + seconds;
        return start;
    }
    static int GetStop(string time)
    {
        time = time.Substring(6);
        int stop;
        int index = time.IndexOf(":");
        int minutes = Convert.ToInt32(time.Substring(0, index));
        int seconds = Convert.ToInt32(time.Substring(3));
        stop = minutes * 60 + seconds;
        return stop;
    }

    static string GetPosition(string line)
    {
        int index = line.IndexOf("[");
        if (index == -1)
        {
            return "Bottom";
        }
        int index_second = line.IndexOf(',');
        int pos = index_second - index;
        line = line.Substring(index + 1, pos - 1);
        return line;
    }
    static string GetColor(string line)
    {
        int index = line.IndexOf("[");
        if (index == -1)
        {
            return "White";
        }
        int index_first = line.IndexOf(",");
        int index_second = line.IndexOf(']');
        int pos = index_second - index_first;
        line = line.Substring(index_first + 2, pos - 2);
        return line;
    }
    static string GetText(string line)
    {
        int index = line.IndexOf("]");
        if (index == -1)
        {
            line = line.Substring(14);
            return line;
        }
        line = line.Substring(index + 2);
        return line;
    }
    static void Write(string position, string color, string text)
    {
        GetColorWord(color);
        switch(position)
        {
            case "Top":
                Console.SetCursorPosition(35 - text.Length / 2, 1);
                Console.Write(text);
                break;
            case "Bottom":
                Console.SetCursorPosition(35 - text.Length / 2, 13);
                Console.Write(text);
                break;
            case "Right":
                Console.SetCursorPosition(70 - text.Length, 7);
                Console.Write(text);
                break;

            case "Left":
                Console.SetCursorPosition(1, 7);
                Console.Write(text);
                break;
        }
    }
    static void GetColorWord(string color)
    {
        if (color == "Blue") Console.ForegroundColor = ConsoleColor.Blue;
        else if (color == "Green") Console.ForegroundColor = ConsoleColor.Green;
        else if (color == "Red") Console.ForegroundColor = ConsoleColor.Red;
        else if (color == "White") Console.ForegroundColor = ConsoleColor.White;
    }
    static void Delete(string position,string text)
    {
        int textLength = text.Length;
        if (position == "Top")
        {
            Console.SetCursorPosition(35-text.Length/2,1);
            DeleteWord(textLength);
        }
        else if (position == "Bottom")
        {
            Console.SetCursorPosition(35-text.Length/2, 13);
            DeleteWord(textLength);
        }
        else if (position == "Right")
        {
            Console.SetCursorPosition(69-text.Length, 7);
            DeleteWord(textLength);
        }
        else if (position == "Left")
        {
            Console.SetCursorPosition(1, 7);
            DeleteWord(textLength);
        }
    }
    static void DeleteWord(int length)
    {
        for (int i = 0; i <= length; i++)
        {
            Console.Write(" ");
        }
    }
}