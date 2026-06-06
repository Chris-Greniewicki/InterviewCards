using System;
using System.Collections.Generic;

var options = new List<string>{"Create Card", "Edit Card", "Delete Card"};

string header = @"
==========================
===Field Interview Card===
==========================
";

void DrawHeader()
{
    Console.Clear();
    Console.WriteLine(header);
}

DrawHeader();

Console.WriteLine("What would you like to do?");
string selectedOption = SelectFromList(options);
DrawHeader();
Console.WriteLine($"You selected {selectedOption}!");


static string SelectFromList(List<string> options)
{
    int selectedIndex = 0;
    ConsoleKey key;

    Console.CursorVisible = false;

    do
    {
        for (int i = 0; i < options.Count; i++)
        {
            if (i == selectedIndex)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
            }
            else
            {
                Console.Write("  ");
            }

            Console.WriteLine(options[i]);
            Console.ResetColor();
        }
        key = Console.ReadKey(intercept: true).Key;

        if (key == ConsoleKey.UpArrow && selectedIndex > 0)
        {
            selectedIndex--;
        }
        else if (key == ConsoleKey.DownArrow && selectedIndex < options.Count - 1)
        {
            selectedIndex++;
        }

        Console.SetCursorPosition(0, Console.CursorTop - options.Count);
    } while (key != ConsoleKey.Enter);

    Console.CursorVisible = true;
    return options[selectedIndex];
}
//Switch this over to a police field interview card system where we ask the user a series of questions and then print out a report at the end.