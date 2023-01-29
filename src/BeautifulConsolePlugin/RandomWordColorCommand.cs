using System;
using System.Collections.Generic;
using PluginBaseLib;
using Spectre.Console;

namespace RandomWordColorPrinter
{
    public class RandomWordColorCommand : ICommand
    {
        private static readonly Random rnd = new();

        public string Name => "Hello World";

        public CommandResult Execute(params string[] args)
        {
            var coloredSentences = new List<string>();

            foreach (var sentence in args)
            {
                var coloredSentence = new List<string>();

                foreach (var word in sentence.Split(" "))
                {
                    var color = (ConsoleColor)rnd.Next(256);

                    var coloredWord = $"[{color.ToString().ToLower()}]{word}[/]";

                    coloredSentence.Add(coloredWord);
                }

                coloredSentences.Add(string.Join(" ", coloredSentence));
            }

            foreach (var coloredSentence in coloredSentences)
            {
                AnsiConsole.Markup($":alien_monster: {coloredSentence} :alien_monster:");
            }

            return CommandResult.Success;
        }
    }
}
