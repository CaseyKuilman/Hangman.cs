using System;
using System.Net;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            //set variables
            int guesses = 0;
            var previousGuesses = "";
            int i = 0;


            //Get a word from the website wordgenerator.net
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            
            string hangmanWordList = client.DownloadString("https://www.wordgenerator.net/application/p.php?id=dictionary_words&type=1&spaceflag=false").ToLower();
            string[] hangmanWordListSplit = hangmanWordList.Split(',');
            string wordToUse = hangmanWordListSplit[0];
            if (wordToUse.Contains(" ")) { Console.WriteLine("An error occured, restart the application."); }


            //DEBUG: TELL WORD

            var displayThisAtStart = "";
            foreach (char e in wordToUse)
            {
                displayThisAtStart = displayThisAtStart + " _ ";
            }
            Console.WriteLine("So far, the word is:\n" + displayThisAtStart + "\n");


            //Let the user guess
            do
            {
                Console.WriteLine("Guess a letter");
                var guessedLetter = Console.ReadLine().ToLower();

                int guessLength = guessedLetter.Length;
                if (guessLength == 1 && i < wordToUse.Length)
                {
                    if (wordToUse.Contains(guessedLetter))
                    {
                        foreach (var c in guessedLetter)
                        {
                            if (previousGuesses.Contains(c))
                            {
                                Console.Write("You already guessed that letter!\n");
                            }
                            else
                            {
                                Console.WriteLine("That is a solid guess! The word contains the letter " + c + ".\n");
                                previousGuesses = previousGuesses + c;
                                var guessSep = "";
                                foreach (char d in previousGuesses)
                                {
                                    guessSep = guessSep + " " + d + " ";
                                }
                                Console.WriteLine("Your previous guesses: [" + guessSep + "].");
                                i++;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sadly, that letter isn't in the word :(.\n");
                        var guessSep = "";
                        foreach (char d in guessedLetter)
                        {
                            previousGuesses = previousGuesses + d;
                            guessSep = guessSep + " " + d + " ";
                        }
                        Console.WriteLine("Your previous guesses: [" + guessSep + "].");
                        guesses = guesses + 1;
                    }
                    if (CheckCompletion(previousGuesses, wordToUse) == true)
                    {
                        break;
                    }
                }
                else if (guessLength != 1)
                {
                    Console.WriteLine("You can not guess more than 1 letter at a time yet.");
                }
                if (guesses >= 13)
                {
                    Console.WriteLine("You have guessed wrong too many times! The word was " + wordToUse + ".\n");
                }
            } while (guesses < 13);

            static bool CheckCompletion(string previousGuesses, string wordToUse)
            {
                bool WinCondition = false;
                bool noFlaw = true;
                var soFar = "";
                var displayedWord = "";
                var guessThisOrWhat = wordToUse;
                foreach (char c in guessThisOrWhat)
                {
                    if (noFlaw == true)
                    {
                        if (previousGuesses.ToLower().Contains(c))
                        {
                            soFar = soFar + c;
                            displayedWord = displayedWord + " " + c + " ";
                            if (soFar == wordToUse)
                            {
                                Console.WriteLine("You have won!");
                                WinCondition = true;
                                break;

                            }
                        }
                        else
                        {
                            displayedWord = displayedWord + " _ ";
                        }
                    }
                    else
                    {
                        Console.WriteLine("BREAK, NOFLAW != TRUE");
                        break;
                    }
                }
                Console.WriteLine("So far, the word is:\n" + displayedWord + "\n");
                if (WinCondition)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}