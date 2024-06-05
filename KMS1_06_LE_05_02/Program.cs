using System;

/// Analyse Programm
class Program
{   
    static void Main(string[] args)
    {
        try
        {
            Analyse.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Beim Programmstart ist ein Fehler aufgetreten");
            Environment.Exit(0);
        }
    }
}