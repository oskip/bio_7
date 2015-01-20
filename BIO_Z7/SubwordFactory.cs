using System;
using System.Collections.Generic;
using System.Linq;
using BIO_Z7.Exceptions;

namespace BIO_Z7
{
    public class SubwordFactory
    {
        public static string Alphabet = "ACTG";
        public static readonly char Separator = '-';

        public static IEnumerable<Subword> CreateFromInput(string inputString)
        {
            var k = inputString.IndexOf(Separator);
            var collection = inputString.Split(Separator);
            if (collection.Any(e => e.Length != k)) 
                throw new InconsistentSubwordLengthException();
            return collection.Select(s => new Subword(s));
        }

        public static IEnumerable<Subword> GenerateAllPossible(int size)
        {
            var allPossible = Alphabet.Select(x => x.ToString());
            for (int i = 1; i < size; i++)
            {
                allPossible = allPossible.SelectMany(x => Alphabet, (x, y) => x + y);
            }
            foreach (var str in allPossible)
            {
                yield return new Subword(str);
            }
        }
    }
}