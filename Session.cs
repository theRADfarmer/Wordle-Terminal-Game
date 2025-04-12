using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wordle
{
    class Session
    {
        public Session()
        {
            Word = GetRandomWord();
        }

        public void PlayGame()
        {
            Console.WriteLine("Welcome to Wordle!");
            Console.WriteLine("You have 6 chances to guess the 5-letter word");
            Console.WriteLine();

            for (int i = MaxGuesses - 1; i >= 0; i--)
            {
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine();


                Guess = GetWord();

                PrintWord();
                UpdateGuessedLetters();
                PrintGameState();

                if (IsGuessCorrect())
                {
                    if (i == 1)
                    {
                        Console.WriteLine($"\nYou guessed the word in {MaxGuesses - i} guess!");
                    }
                    else
                    {
                        Console.WriteLine($"\nYou guessed the word in {MaxGuesses - i} guesses!");
                    }
                    break;
                }

                if (i == 0)
                {
                    Console.WriteLine("\nYou have no guesses left");
                    Console.WriteLine($"The word was {Word}");
                    break;
                }
                else if (i == 1)
                {
                    Console.WriteLine($"\nYou have {i} guess left\n");
                }
                else
                {
                    Console.WriteLine($"\nYou have {i} guesses left\n");
                }

            }
        }

        private const int MaxGuesses = 6;

        public string Word = null!;

        private string Guess;

        private List<char> GuessedLetters = [];

        private string[] Words = File.ReadAllLines("Words.txt");

        private readonly char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
            's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        private string GetRandomWord()
        {
            var random = new Random();
            int index = random.Next(Words.Length);
            return Words[index];
        }

        private string GetWord()
        {
            Console.WriteLine("Enter a 5-letter word:");
            string? word = Console.ReadLine();
            if (word != null)
            {
                if (word.Length != 5)
                {
                    Console.WriteLine("Word must be 5 letters");
                    return GetWord();
                }
                else if (!Regex.IsMatch(word, @"^[a-zA-Z]+$"))
                {
                    Console.WriteLine("Word must be letters only");
                    return GetWord();
                }
                else
                {
                    return word.ToLower();
                }
            }
            else
            {
                return GetWord();
            }
        }

        /// <summary>Prints the word with the letters that are in the word in green, 
        /// letters that are not in the word in red, and 
        /// letters that are in the word but not in the correct position in yellow</summary>
        private void PrintWord()
        {
            Console.WriteLine();

            foreach (char letter in Guess)
            {
                if (!Word.Contains(letter))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(letter);
                    Console.ResetColor();
                }
                else if (Word.IndexOf(letter) == Guess.IndexOf(letter))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(letter);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(letter);
                    Console.ResetColor();
                }
            }

            Console.WriteLine();
        }

        private void UpdateGuessedLetters()
        {
            foreach (char letter in Guess)
            {
                if (!GuessedLetters.Contains(letter))
                {
                    GuessedLetters.Add(letter);
                }
            }
        }

        private void PrintGuessedLetters()
        {
            foreach (char letter in GuessedLetters)
            {
                Console.Write(letter);
            }
        }

        private void PrintGameState()
        {
            Console.WriteLine("\nGame State: ");

            foreach (char letter in alphabet)
            {
                if (!GuessedLetters.Contains(letter))
                {
                    Console.Write(letter);
                }
                else
                {
                    if (Word.Contains(letter))
                    {
                        if (Word.IndexOf(letter) == Guess.IndexOf(letter))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(letter);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(letter);
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(letter);
                        Console.ResetColor();
                    }
                }
            }

            Console.WriteLine();
        }

        private bool IsGuessCorrect()
        {
            if (Guess == Word)
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
