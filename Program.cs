using System;
using System.IO;
using System.Collections.Generic;

var initialOptions = new List<string> {"Search Cards", "Create Card", "Exit"};
var searchOptions = new List<string> { "Edit Card", "Delete Card", "Main Menu" };

string header = @"
==========================
===Field Interview Card===
==========================
";

mainMenu();

//Functions

void mainMenu()
{
    DrawHeader();
    Console.WriteLine("What would you like to do?");
    string selectedOption = SelectFromList(initialOptions);
    if (selectedOption == "Search Cards")
    {
        searchMenu();
    }
    else if (selectedOption == "Exit" || selectedOption == "backspace")
    {
        Console.WriteLine("Exiting Program...");
        Environment.Exit(0);
    }
    else
    {
        DrawHeader();
        Console.WriteLine($"You selected {selectedOption}!");
    }
}

void searchMenu()
{
    DrawHeader();
    Console.WriteLine("What would you like to do?");
    string selectedOption = SelectFromList(searchOptions);
    if (selectedOption == "Main Menu" || selectedOption == "backspace")
    {
        mainMenu();
    }
    else
    {
        Console.WriteLine($"You selected {selectedOption}!");
    }
}

//Clear console and display header
void DrawHeader()
{
    Console.Clear();
    Console.WriteLine(header);
}

//Display options as selectable
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
        else if (key == ConsoleKey.Backspace)
        {
            return "backspace";
        }
        Console.SetCursorPosition(0, Console.CursorTop - options.Count);
    } while (key != ConsoleKey.Enter);

    Console.CursorVisible = true;
    return options[selectedIndex];
}