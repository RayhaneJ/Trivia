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

        public LinkedList<string> Questions { get; set; } = new();

        public Categorie(string name, int[] values)
        {
            this.name = name;
            this.values = values;
        }

        public static Categorie Pop = new("Pop", new[] { 0, 4, 8 });
        public static Categorie Science = new("Science", new[] { 1, 5, 9 });
        public static Categorie Sports = new("Sports", new[] { 2, 6, 10 });
        public static Categorie Rock = new("Rock", new[] { 3, 7, 11 });

        public static Categorie FromValue(int value) => value switch
        {
            int n when (Pop.values.Contains(n)) => Pop,
            int n when (Science.values.Contains(n)) => Science,
            int n when (Sports.values.Contains(n)) => Sports,
            int n when (Rock.values.Contains(n)) => Rock,
            _ => throw new ArgumentOutOfRangeException("Categorie number doesn't exist."),
        };

        public override string ToString() => name;
    }
}
