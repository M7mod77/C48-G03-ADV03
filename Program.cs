using System;
using System.Collections.Generic;
using System.Linq;

namespace C48_G03_ADV03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Exercise1();
            Exercise2();
            Exercise3();
            Exercise4();
        }

        
        static void PrintHeading(string title)
        {
            Console.WriteLine();
            Console.WriteLine("==============================");
            Console.WriteLine(title);
            Console.WriteLine("==============================");
        }

       
        static void Exercise1()
        {
            PrintHeading("Exercise 1 - Student Grades");

            List<int> grades = new List<int> { 85, 92, 78, 95, 88, 70, 100, 65 };

            
            Console.WriteLine("1. All grades: " + string.Join(", ", grades));

            Console.WriteLine("2. Count: " + grades.Count);

            Console.WriteLine("3. First grade: " + grades[0]);
            Console.WriteLine("4. Last grade: " + grades[grades.Count - 1]);

            grades.Sort();
            Console.WriteLine("5. Sorted ascending: " + string.Join(", ", grades));

            int firstAbove90 = grades.Find(grade => grade > 90);
            Console.WriteLine("6. First grade above 90: " + firstAbove90);

            List<int> failingGrades = grades.FindAll(grade => grade < 75);
            Console.WriteLine("7. Grades below 75: " + string.Join(", ", failingGrades));

            int removedCount = grades.RemoveAll(grade => grade < 75);
            Console.WriteLine("8. Removed " + removedCount + " failing grade(s).");

            Console.WriteLine("9. After removing failing grades: " + string.Join(", ", grades));

            bool hasPerfectScore = grades.Exists(grade => grade == 100);
            Console.WriteLine("10. Any grade equal to 100? " + hasPerfectScore
                              + " (Contains(100) = " + grades.Contains(100) + ")");

            List<string> gradeLabels = grades.ConvertAll(grade => "Grade: " + grade);

            Console.WriteLine("11 & 12. Grades as text:");
            foreach (string label in gradeLabels)
            {
                Console.WriteLine("   " + label);
            }
        }

        
        static void Exercise2()
        {
            PrintHeading("Exercise 2 - Leaderboard");

           
            SortedList<int, string> leaderboard = new SortedList<int, string>();
            leaderboard.Add(500, "Ahmed");
            leaderboard.Add(200, "Sara");
            leaderboard.Add(800, "Ali");
            leaderboard.Add(350, "Mona");

            Console.WriteLine("1. All entries (added in the order 500, 200, 800, 350):");
            foreach (KeyValuePair<int, string> entry in leaderboard)
            {
                Console.WriteLine("   Score " + entry.Key + " -> " + entry.Value);
            }
            Console.WriteLine("2. Notice they came out sorted by score ascending - SortedList did that by itself.");

            Console.WriteLine("3. First key (lowest score): " + leaderboard.Keys[0]);
            Console.WriteLine("4. First value (player with the lowest score): " + leaderboard.Values[0]);

            Console.WriteLine("5. Does score 500 exist? " + leaderboard.ContainsKey(500));

            if (leaderboard.TryGetValue(999, out string? playerWith999))
            {
                Console.WriteLine("6. Player with score 999: " + playerWith999);
            }
            else
            {
                Console.WriteLine("6 & 7. No player has score 999 - nothing was thrown, TryGetValue just returned false.");
            }

            leaderboard.Remove(200);

            Console.WriteLine("8 & 9. Leaderboard after removing score 200:");
            foreach (KeyValuePair<int, string> entry in leaderboard)
            {
                Console.WriteLine("   Score " + entry.Key + " -> " + entry.Value);
            }

            Console.WriteLine("Why SortedList here? A leaderboard must always be in score order.");
            Console.WriteLine("SortedList sorts on every insert, so we never call Sort() ourselves,");
            Console.WriteLine("and we can still read Keys[0] / Values[0] by index like a list.");
        }

      
        static void Exercise3()
        {
            PrintHeading("Exercise 3 - Phone Book");

            Dictionary<string, string> phoneBook = new Dictionary<string, string>
            {
                { "Ahmed",   "01001234567" },
                { "Sara",    "01112345678" },
                { "Mona",    "01223456789" },
                { "Youssef", "01554321098" }
            };

            Console.WriteLine("1. Original phone book:");
            foreach (KeyValuePair<string, string> contact in phoneBook)
            {
                Console.WriteLine("   " + contact.Key + " -> " + contact.Value);
            }

            phoneBook["Nour"] = "01098765432";   
            phoneBook["Sara"] = "01119998877";   
            Console.WriteLine();
            Console.WriteLine("2. After using [] syntax:");
            Console.WriteLine("   Nour was ADDED   -> " + phoneBook["Nour"]);
            Console.WriteLine("   Sara was UPDATED -> " + phoneBook["Sara"]);
            Console.WriteLine("   So [] never throws: it inserts a new key OR overwrites an existing one.");

            Console.WriteLine();
            try
            {
                phoneBook.Add("Ahmed", "01000000000");
                Console.WriteLine("3. Ahmed was added.");
            }
            catch (ArgumentException error)
            {
                Console.WriteLine("3. Could not add \"Ahmed\" - that name is already in the phone book.");
                Console.WriteLine("   (Friendly message instead of a crash. Details: " + error.Message + ")");
            }

            bool wasAdded = phoneBook.TryAdd("Ahmed", "01000000000");
            Console.WriteLine("4. TryAdd(\"Ahmed\", ...) succeeded? " + wasAdded + "  (false = key already existed, no exception)");

            Console.WriteLine();
            if (phoneBook.TryGetValue("Khaled", out string? khaledNumber))
            {
                Console.WriteLine("5. Khaled's number: " + khaledNumber);
            }
            else
            {
                Console.WriteLine("5 & 6. \"Khaled\" is not in the phone book.");
            }

            string missingNumber = phoneBook.GetValueOrDefault("Khaled", "Not Found");
            Console.WriteLine("7. GetValueOrDefault(\"Khaled\", \"Not Found\") = " + missingNumber);

            Console.WriteLine();
            Console.WriteLine("8. All names:   " + string.Join(", ", phoneBook.Keys));
            Console.WriteLine("9. All numbers: " + string.Join(", ", phoneBook.Values));
        }

       
        static void Exercise4()
        {
            PrintHeading("Exercise 4 - Unique Emails & Set Operations");

            HashSet<string> emails = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            emails.Add("ahmed@test.com");
            emails.Add("AHMED@test.com");   
            emails.Add("sara@test.com");
            emails.Add("Sara@Test.Com");    

            Console.WriteLine("1. Stored emails: " + string.Join(", ", emails));
            Console.WriteLine("2. Count: " + emails.Count);

       
            Console.WriteLine("3. Why only 2? A HashSet never stores duplicates, and we gave it");
            Console.WriteLine("   StringComparer.OrdinalIgnoreCase, so casing is ignored when comparing.");
            Console.WriteLine("   \"AHMED@test.com\" is considered equal to \"ahmed@test.com\", so Add() returned false.");

            
            HashSet<int> setA = new HashSet<int> { 1, 2, 3, 4, 5 };
            HashSet<int> setB = new HashSet<int> { 4, 5, 6, 7, 8 };

            Console.WriteLine();
            Console.WriteLine("Set A = { " + string.Join(", ", setA) + " }");
            Console.WriteLine("Set B = { " + string.Join(", ", setB) + " }");
            Console.WriteLine();

            
            HashSet<int> union = new HashSet<int>(setA);
            union.UnionWith(setB);            
            Console.WriteLine("UnionWith     -> { " + string.Join(", ", union) + " }   (items in A OR B)");

            HashSet<int> intersection = new HashSet<int>(setA);
            intersection.IntersectWith(setB);
            Console.WriteLine("IntersectWith -> { " + string.Join(", ", intersection) + " }   (items in A AND B)");

            HashSet<int> except = new HashSet<int>(setA);
            except.ExceptWith(setB);          
            Console.WriteLine("ExceptWith    -> { " + string.Join(", ", except) + " }   (items in A but NOT in B)");

            Console.WriteLine();
            Console.WriteLine("Set A is still unchanged: { " + string.Join(", ", setA) + " }");

            HashSet<int> smallSet = new HashSet<int> { 1, 2 };
            Console.WriteLine("Is { " + string.Join(", ", smallSet) + " } a subset of Set A? " + smallSet.IsSubsetOf(setA));
        }
    }
}
