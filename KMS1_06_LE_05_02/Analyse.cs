using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Klasse Analyse, Counter und Top5 Counts aus einer Textdatei die der Benutzer eingibt
/// </summary>
public static class Analyse
{   /// <summary>
    /// Analyse Start
    /// </summary>
    public static void Start()
    {
        Analyseprogram();
    }
    /// <summary>
    /// Analysiert die Menge an Wörter und wie oft diese vorkommen aus einer Textdatei die der User eingibt und 
    /// das Ergebnis wird in eine neue Datei in einem beliebigen Verzeichnis gespeichert
    /// </summary>
    public static void Analyseprogram()
    {
        string wantedDirectory = "";
        string currentDirectory = "";

        string localFileContent = ""; 
        int wordCount = 0; 

        // Pfadeingabe + Exceptionhandling
        while (true)
        {
            try
            {
                Console.WriteLine("Geben Sie den Pfad zur Datei ein:");
                wantedDirectory = Console.ReadLine(); // Get the input from the user
                localFileContent = File.ReadAllText(wantedDirectory);
                break;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Die Datei konnte nicht gefunden werden.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Sie haben keine Berechtigung zum Lesen dieser Datei.");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine("Das Pfadformat wird nicht unterstützt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ungültiger Pfad. Versuchen Sie es erneut.");
            }
        }
        // Einlesen der Datei
        try
        {
            localFileContent = File.ReadAllText(wantedDirectory);

            // Text in Wörter zerlegen
            string[] words = localFileContent.Split(new char[] { ' ', '\n', '\r', '\t', ',', '.', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            // Wortzählung
            wordCount = words.Length;

            // Zählung Top 5
            var mostFrequentWords = TextAnalyzer.FindMostFrequentWords(localFileContent, 5);
            Console.WriteLine("Die Top 5 häufigsten Wörter in der Datei sind:");
            foreach (var word in mostFrequentWords)
            {
                Console.WriteLine($"{word.Key}: {word.Value} mal");
            }

            Console.WriteLine("Die Anzahl der Wörter in der Datei beträgt: " + wordCount);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Bei der Zählung ist ein Fehler aufgetreten.");
        }

        string newFileName;
        string newText = ""; 

        // Eingabe + Überprüfung des neuen Dateiname
        while (true)
        {
            try
            {
                Console.Write("Geben Sie den neuen Dateinamen ein, mit .txt-Endung: ");
                newFileName = Console.ReadLine();

                if (newFileName.Length >= 5 && newFileName.EndsWith(".txt") && newFileName.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_' || c == '.'))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Geben Sie einen gültigen Dateinamen ein.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Achten Sie auf eine gültige Eingabe. zB: beispiel.txt");
            }
        }
        // Eingabe wo das Ergebnis aus der neuen Datei gespeichert werden soll, falls gleich wird überschrieben
        while (true)
        {
            try
            {
                Console.WriteLine("Geben Sie ihr gewünschtes Verzeichnis an wo die Datei gespeichert werden soll:");
                currentDirectory = Console.ReadLine();

                string newFilePath = Path.Combine(currentDirectory, newFileName);
                if (!File.Exists(newFilePath))
                {
                    newText = "insgesamte Wörteranzahl: " + wordCount + " Wörter"; 
                    File.WriteAllText(newFilePath, newText);
                    Console.WriteLine("Neue Datei erfolgreich gespeichert.");
                    break;
                }
                else
                {
                    newText = "insgesamte Wörteranzahl: " + wordCount + " Wörter"; 
                    File.WriteAllText(newFilePath, newText);
                    Console.WriteLine("Sie haben die Datei überschrieben.");
                    break;
                }
            }
            
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Sie haben keine Berechtigung in diesem Pfad zu speichern.");
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Dieses Verzeichnis existiert nicht.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        Console.WriteLine("Drücken Sie die Eingabetaste, um das Programm zu beenden.");
        Console.ReadLine();
    }
}
