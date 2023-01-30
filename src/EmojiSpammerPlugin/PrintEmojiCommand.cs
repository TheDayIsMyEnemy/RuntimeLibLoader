using PluginBaseLib;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace EmojiSpammerPlugin
{
    public class PrintEmojiCommand : ICommand
    {
        private static readonly Random rnd = new();

        private static readonly FieldInfo[] EmojiProps = typeof(Emoji).GetFields(
            BindingFlags.Public | BindingFlags.Static
        );

        public string Name => "Hello World";

        public CommandResult Execute(params string[] args)
        {
            var ind = rnd.Next(0, Quotes.SmallGods.Length);

            var quote = Quotes.SmallGods[ind];

            var emojiQuote = new List<string>();

            //{"a", "when", "to", "or", "and", "in", "of", "with"}



            foreach (var word in quote.Split(" "))
            {
                var maxC = -1;
                FieldInfo bProp = null;

                if (word.Length > 2)
                {
                    foreach (
                        var prop in EmojiProps.Where(
                            e => e.Name.Contains(word, StringComparison.OrdinalIgnoreCase)
                        )
                    )
                    {
                        var wChars = word.ToLower().ToList();
                        var pChars = prop.Name.ToLower().ToList();

                        wChars.AddRange(pChars);
                        var currC = (word.Length + prop.Name.Length) - wChars.Distinct().Count();

                        if (currC > maxC)
                        {
                            maxC = currC;
                            bProp = prop;
                        }
                    }
                }

                if (bProp != null)
                {
                    emojiQuote.Add(bProp.GetValue(typeof(Emoji)) as string);
                }
                else
                {
                    emojiQuote.Add(word);
                }
            }

            Console.WriteLine($"{string.Join(" ", emojiQuote)}");

            return CommandResult.Success;
        }
    }
}
