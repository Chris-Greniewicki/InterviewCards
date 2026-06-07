using System;
using System.IO;
using System.Collections.Generic;

var initialOptions = new List<string> {"Search Cards", "Create Card", "Exit"};
var searchOptions = new List<string> { "Edit Card", "Delete Card", "Main Menu" };

var path = "insertpathhere";

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
    else if (selectedOption == "Create Card")
    {
        createCard();
    }
    else
    {
        DrawHeader();
        Console.WriteLine($"You selected {selectedOption}!");
    }
}

void searchMenu()
//Change this function to display list of currently existing interview cards - Option to edit/delete cards will be available once an individual card is opened
//Need to determine how this data will be displayed at first before searching via filters
//Need to determine filters to use for searching
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

void createCard()
{
    //Create new interview card
    DrawHeader();
    Console.WriteLine("Please enter interviewee name");
    string intervieweeName = Console.ReadLine();
    Console.WriteLine("Interviewee: " + intervieweeName);
    Console.WriteLine("Please enter the current date as mm/dd/yyyy");
    string interviewDate = Console.ReadLine();
    Console.WriteLine("Date: " + interviewDate);
    Console.WriteLine("Please enter related Case Number");
    string caseNumber = Console.ReadLine();
    Console.WriteLine("Case Number: " + caseNumber);
    Console.WriteLine("Please enter interview details");
    string interviewDetails = Console.ReadLine();
    Console.WriteLine("Interview Details: " + interviewDetails);
    string interviewCard = string.Join(" | ", intervieweeName, interviewDate, caseNumber, interviewDetails);
    Console.WriteLine(interviewCard);

    //File.AppendAllText(path, interviewCard);
}

void editCard()
{
    //Modify existing interview card after selecting via search
}

void deleteCard()
{
    //Delete existing interview card after selecting via search
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