using System;
using System.IO;
using System.Collections.Generic;

var initialOptions = new List<string> {"Search Cards", "Create Card", "Exit"};
var searchOptions = new List<string> { "Edit Card", "Delete Card", "Main Menu" };
var displayOptions = new List<string> { "Edit Card", "Delete Card", "Main Menu" };

var myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
var ivCardsPath = Path.Combine(myDocumentsPath, "Interview Cards");
var ivCardsFolder = Directory.CreateDirectory(ivCardsPath);
string filePath = Path.Combine(ivCardsPath, "Interview Cards.txt");

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
//ADDRESS ISSUE where if there are no cards yet, error occurs
//ADDRESS ISSUE - Ensure all cards align together in list regardless of info length within reason
//Add Option to edit/delete cards once an individual card is opened
//Need to determine how this data will be displayed at first before searching via filters
//Need to determine filters to use for searching
{
    DrawHeader();
    var cardList = File.ReadAllLines(filePath);
    var displayList = new List<string>();
    foreach (var line in cardList)
    {
        var sections = line.Split("|");
        displayList.Add($"{sections[0]} | {sections[1]} | {sections[2]}");
    }
    displayList.Add("-------------------------------");
    displayList.AddRange(searchOptions);
    Console.WriteLine("What would you like to do?");
    string selectedOption = SelectFromList(displayList);
    if (selectedOption == "Main Menu" || selectedOption == "backspace")
    {
        mainMenu();
    }
    else if (selectedOption == "-------------------------------")
    {
        //ADDRESS ISSUE - This bar should do nothing on selection, preferably not even be selectable, just be a separator between menus
        mainMenu();
    }
    else
    {
        var index = displayList.IndexOf(selectedOption);
        displayCard(index);
    }
}

void displayCard(int index)
{
    //Display selected card for review
    //ADDRESS ISSUE - Make text start new line at end of word instead of mid-word
    var cardList = File.ReadAllLines(filePath);
    DrawHeader();
    var test = cardList[index].Split(" | ");
    foreach (var line in test)
    {
        Console.WriteLine(line);
    }
    string selectedOption = SelectFromList(displayOptions);
    if (selectedOption == "Edit Card")
    {
        editCard(index);
    }
    else if (selectedOption == "Delete Card")
    {
        deleteCard();
    }
    else if (selectedOption == "Main Menu" || selectedOption == "backspace")
    {
        mainMenu();
    }
}

void createCard()
{
    //Create new interview card
    DrawHeader();
    Console.WriteLine("Please enter interviewee name");
    string intervieweeName = "Interviewee Name: " + Console.ReadLine();
    Console.WriteLine(intervieweeName);
    Console.WriteLine("Please enter the current date as mm/dd/yyyy");
    string interviewDate = "Interview Date: " + Console.ReadLine();
    Console.WriteLine(interviewDate);
    Console.WriteLine("Please enter related Case Number");
    string caseNumber = "Case Number: " + Console.ReadLine();
    Console.WriteLine(caseNumber);
    Console.WriteLine("Please enter interview details");
    string interviewDetails = "Interview Details: " + Console.ReadLine();
    Console.WriteLine(interviewDetails);
    string interviewCard = string.Join(" | ", intervieweeName, interviewDate, caseNumber, interviewDetails);
    Console.WriteLine(interviewCard);

    File.AppendAllText(filePath, interviewCard + Environment.NewLine);
    mainMenu();
}

void editCard(int index)
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