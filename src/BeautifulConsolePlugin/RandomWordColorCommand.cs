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
            var rndIndex = rnd.Next(0, Quotes.SmallGods.Length);
            var quote = Quotes.SmallGods[rndIndex];

            var coloredSentence = new List<string>();

            try
            {
                foreach (var word in quote.Split(" "))
                {
                    var color = (ConsoleColor)rnd.Next(256);

                    var coloredWord = $"[{color.ToString().ToLower()}]{word}[/]";

                    coloredSentence.Add(coloredWord);
                }

                AnsiConsole.Markup($":books: {string.Join(" ", coloredSentence)}");
            }
            catch
            {
                AnsiConsole.Markup($":books: {quote}");

                return CommandResult.Error;
            }

            return CommandResult.Success;
        }
    }
}
