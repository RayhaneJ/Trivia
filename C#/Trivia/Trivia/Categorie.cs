using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia
{
    public sealed class Categorie
    {
        private readonly int[] values;
        private readonly string name;

        public LinkedList<string> Questions { get; set; } = new LinkedList<string>();

        public static Categorie pop = new ("Pop", new[] { 0, 4, 8 });
        public static Categorie science = new ("Science", new[] { 1, 5, 9 });
        public static Categorie sports = new("Sports", new[] { 2, 6, 10 });
        public static Categorie rock = new("Rock", new[] { 3, 7, 11 });

        public static void InitQuestions()
        {
            for (var i = 0; i < 50; i++)
            {
                pop.Questions.AddLast($"{pop.name} Question" + i);
                science.Questions.AddLast($"{science.name} Question" + i);
                sports.Questions.AddLast($"{sports.name} Question" + i);
                rock.Questions.AddLast($"{rock.name} Question" + i);
            }
        }

        public Categorie(string name, int[] values)
        {
            this.name = name;
            this.values = values;
        }

        public static Categorie FromValue(int value) => value switch
        {
            int n when (pop.values.Contains(n)) => pop,
            int n when (science.values.Contains(n)) => science,
            int n when (sports.values.Contains(n)) => sports,
            int n when (rock.values.Contains(n)) => rock,
            _ => throw new ArgumentOutOfRangeException("Categorie number doesn't exist."),
        };

        public override string ToString() => name;
    }
}
