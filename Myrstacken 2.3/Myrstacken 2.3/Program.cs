using System;
using System.Collections.Generic;

class Program
{

    List<Ant> ants = new List<Ant>();

    static void Main()
    {
        Program p = new Program();
        p.Run();
    }

    void Run()
    {
        Intro();
        Commands();
    }

    private void Intro()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(" _______            _     _ _ _  ");
        Console.WriteLine("(       )       _  | |   (_) | | ");
        Console.WriteLine("|(*) (*)|____ _| |_| |___|_| | | ");
        Console.WriteLine("|  ___  |  _ (_   _)  _  | | | | ");
        Console.WriteLine("| |   | | | | || | | | | | | | | ");
        Console.WriteLine("|_|   |_|_| |_||__)|_| |_|_|_)_) ");
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("\nType \"help\" for available commands\n");
    }

    private void Commands()
    {
        while (true)
        {
            Console.WriteLine("Enter command: ");
            string input = Console.ReadLine().ToLower();

            if (input == "help")
            {
                Help();
            }
            else if (input == "add")
            {
                AddAnt();
            }
            else if (input == "count")
            {
                AntCount();
            }
            else if (input == "anthill")
            {
                PrintAnthill();
            }
            else if (input == "search")
            {
                SearchAnt();
            }
            else if (input == "remove")
            {
                RemoveAnt();
            }
            else if (input == "quit")
            {
                Quit();
            }
            else if (input == "clear")
            {
                Clear();
            }
            else
            {
                Console.WriteLine("Unknown command!\n");
            }
        }
    }

    private void Help()
    {
        Console.WriteLine("\n " + "<Help menu>");
        Console.WriteLine("Type 'add' to add an ant to the anthill");
        Console.WriteLine("Type 'count' to view the number of ants in the anthill");
        Console.WriteLine("Type 'anthill' to view all ants with their respective number of legs");
        Console.WriteLine("Type 'search' to search for ants, either by their name or their number of legs");
        Console.WriteLine("Type 'remove' to permanently remove an ant from the anthill");
        Console.WriteLine("Type 'clear' to clear console window of all junk");
        Console.WriteLine("Type 'quit' to exit program\n");
    }

    private void AddAnt()
    {
        Console.WriteLine("Enter your ant's name: ");
        string name = Console.ReadLine();

        /*
         * Robin:
         * Det äärsynd att du omvandlar namnet här, när du redan gör det i konstruktorn. 
         * Det förstör lite det snygga du gjorde där. Det hade varit extra snuggt om du
         * även utförde kontrollen av längden och mellanslag i Ant-konstruktorn! Kanske
         * lite överkurs dock.
         */
        name = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();

        if (name.Length > 10)
        {
            Console.WriteLine("Ants can't have names with more than 10 characters!");
            return;
        }
        if (name.Contains(" "))
        {
            Console.WriteLine("Names can't contain spaces!");
            return;
        }
        for (int i = 0; i < ants.Count; i++)
        {
            if (name == ants[i].GetName())
            {
                Console.WriteLine("An ant with that name already exists!");
                return;
            }
        }
        while (true)
        {
            Console.WriteLine("Enter your ant's number of legs: ");
            string legsAmount = Console.ReadLine();

            try
            {
                int realLegs = int.Parse(legsAmount);

                if (realLegs < 0)
                {
                    Console.WriteLine("Ants can't have a negative number of legs!");
                    return;
                }
                Ant ant = new Ant(name, realLegs);
                ants.Add(ant);
                Console.WriteLine("You added \"" + name + "\" with " + realLegs + " legs\n");
                return;
            }
            catch
            {
                Console.WriteLine("Invalid input! (Only integers allowed)");
            }
        }
    }

    private void AntCount()
    {
        if (ants.Count > 0)
        {
            Console.WriteLine("Number of ants in anthill:\n" + ants.Count + "\n");
        }
        else
        {
            Console.WriteLine("There are no ants in the anthill!\n");
        }
    }

    private void PrintAnthill()
    {
        if (ants.Count > 0)
        {
            Console.WriteLine("\nYour ants: ");
        }
        else
        {
            Console.WriteLine("\nThere are no ants in the anthill!\n");
            return;
        }
        for (int i = 0; i < ants.Count; i++)
        {
            Console.WriteLine(ants[i].ToString());
        }
        Console.WriteLine("");
    }

    /*
     * Robin:
     * Bra kommentar! Dock så kollar den inte mot konstruktorn, utan mot det som finns sparat i 
     */
    /// <summary>
    /// Checks whether there are any ants in the anthill at all - if not - writes corresponding error message and returns to called method
    /// 
    /// Checks input from user and stores input in string
    /// Checks whether input is "name" - calls method "SearchByName" 
    /// Responds to what the method "SearchByName" returns by writing out a specific message (either the found ant, according to the class "Ant":s constructor, or corresponding error message)
    /// 
    /// Checks whether input is "legs" - calls method "SearchByLegs"
    /// if input is anything else - Writes out corresponding error message
    /// </summary>
    private void SearchAnt()
    {
        if (ants.Count == 0)
        {
            Console.WriteLine("\nThere are no ants in the anthill, and therefore nothing to search for\n");
            return;
        }
        Console.WriteLine("Type 'name' to search for an ant by its name\nType 'legs' to search for all ants with that number of legs");
        string input = Console.ReadLine();

        if (input == "name")
        {
            Ant A = SearchByName();
            if (A != null)
            {
                Console.WriteLine("\nYou found: \n" + A + "\n");
            }
            else
            {
                Console.WriteLine("\nThere is no ant with that name!\n");
            }
        }
        else if (input == "legs")
        {
            SearchByLegs();
        }
        else
        {
            Console.WriteLine("\nInvalid command!\n");
        }
    }

    /// <summary>
    /// Checks input from user and stores input in string
    /// Iterates through the "anthill" list, comparing each item:s name with input
    /// If input and an item's name in the list matches, returns the item's index
    /// 
    /// If after iteration through the list, no item's name from the list matches input, returns null
    /// </summary>
    private Ant SearchByName()
    {
        Console.WriteLine("Enter an ant's name to search for: ");
        /*
         * Robin:
         * vi hade kunnat effektivisera genom att kalla på ToLower() direkt. Nu så utförs det arbetet
         * en gång extra för varje iteration efter den första.
         */
        string input = Console.ReadLine();

        for (int i = 0; i < ants.Count; i++)
        {
            if (input.ToLower() == ants[i].GetName().ToLower())
            {
                return ants[i];
            }
        }
        return null;
    }

    /// <summary>
    /// All code is in a while-loop - allows user to try again if catch-block is reached
    /// 
    /// Checks input from user and stores input in string
    /// Converts input to an integer, stored in the int "reallegs" (If conversion from string to int fails, catch-block catches error and writes corresponding error message)
    /// Boolean "found" is assigned false
    /// Iterates through the "anthill" list
    /// Every iteration: checks whether reallegs (input) matches with the number of legs of the item (ant) in the list with that index - in that case - Writes out that item according to the class "Ant":s constructor
    /// The bool "found" is assigned true as (condition specified above)
    /// 
    /// If at this point - "found" is still false - writes out corresponding message
    /// Returns to calling method
    /// </summary>
    private void SearchByLegs()
    {
        while (true)
        {
            Console.WriteLine("Enter a number of legs to search for");
            string input = Console.ReadLine();

            try
            {
                int realLegs = int.Parse(input);
                Console.WriteLine("\nYou found:");
                bool found = false;
                for (int i = 0; i < ants.Count; i++)
                {
                    if (realLegs == ants[i].GetLegs())
                    {
                        Console.WriteLine(ants[i]);
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("No ants with that amount of legs!\n");
                }
                return;
            }
            catch
            {
                Console.WriteLine("Invalid input! (Only integers allowed)");
            }
        }
    }

    private void RemoveAnt()
    {
        if (ants.Count == 0)
        {
            Console.WriteLine("\nThere are no ants in the anthill, and therefore nothing to remove\n");
        }
        Console.WriteLine("Enter an ant's name to remove: ");
        string input = Console.ReadLine();

        for (int i = 0; i < ants.Count; i++)
        {
            if (input.ToLower() == ants[i].GetName().ToLower())
            {

                Console.WriteLine("\nYou found " + ants[i].GetName() + " and removed it!\n");
                ants.RemoveAt(i);
                return;
            }
        }
        Console.WriteLine("There is no ant with that name!");
    }

    private void Clear()
    {
        Console.Clear();
        Intro();
    }

    private void Quit()
    {
        Environment.Exit(0);
    }
}


/*
 * Robin:
 * Jag har inte så mycket att säga förutom bra jobbat! 
 * Du har en konsekvent kodningsstil med tydlig namngivning. Koden är
 * robust vad jag kan se, och jag hitar inga uppenbara logiska fel.
 * 
 * Jag hade kanske tänkt på att hålla kommentarerna lite kortare, 
 * och dubbelkolla så att du inte skriver in någon information som är felaktig. 
 * 
 * Det har varit väldigt kul att se dig arbeta på lektionen. Du kommunicerar 
 * bra och tydligt runt de problem som du stöter på. Det är jättebra att du
 * tar ansvar för ditt eget lärande och föreslår förändringar i klassrummet
 * för att förbättra din lärandesituation!
 * 
 * Fortsätt så här!
 */