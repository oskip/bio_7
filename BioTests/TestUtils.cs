using System.Collections;
using System.Collections.Generic;
using BIO_Z7;
using QuickGraph;

namespace BioTests
{
    public class TestUtils
    {
        public class SubwordTests
        {
            public static string CorrectSubword = "ACTGGATA";
            public static string CorrectSubwordsPrefix = "ACTGGAT";
            public static string CorrectSubwordsPostfix = "CTGGATA";
            public static string IncorrectSubword = "ACTZGATA";
            public static string CorrectSubwordFiveString = "ACTGG";
            public static string CorrectInputString = "ACT-GAC-TGG-TTA-ACG";

            public static List<Subword> CorrectInputSubwords = new List<Subword>()
            {
                new Subword("ACT"),
                new Subword("GAC"),
                new Subword("TGG"),
                new Subword("TTA"),
                new Subword("ACG")
            };

            public static string InputStringWithOneTooShort = "TAT-GA-GCA-ACT-GCC";
            public static string InputStringWithOneTooLong = "GCA-CTA-AGCG-TTA-ACG";
        }

        public static List<Subword> CorrectSubwords = new List<Subword>()
        {
            new Subword("AAA"),
            new Subword("AAC"),
            new Subword("ACA"),
            new Subword("CAC"),
            new Subword("CAA"),
            new Subword("ACG"),
            new Subword("CGC"),
            new Subword("GCA"),
            new Subword("ACT"),
            new Subword("CTT"),
            new Subword("TTA"),
            new Subword("TAA")
        };

        public static int CorrectSubwordsVerticesCount = 8;

        public static List<TaggedEdge<string, string>> CorrectSubwordsConnections  = new List<TaggedEdge<string, string>>()
        {
            new TaggedEdge<string, string>("AA", "AA", "A"),
            new TaggedEdge<string, string>("AA","AC","C"),
            new TaggedEdge<string, string>("AC","CA", "A"),
            new TaggedEdge<string, string>("CA", "AC", "C"),
            new TaggedEdge<string, string>("CA", "AA", "A"),
            new TaggedEdge<string, string>("AC", "CT", "T"),
            new TaggedEdge<string, string>("AC", "CG", "G"),
            new TaggedEdge<string, string>("CG", "GC", "C"),
            new TaggedEdge<string, string>("GC", "CA", "A"),
            new TaggedEdge<string, string>("CT", "TT", "T"),
            new TaggedEdge<string, string>("TT", "TA", "A"),
            new TaggedEdge<string, string>("TA", "AA", "A")
        };

        public static List<Subword> SubwordsWithEulerianTrail = new List<Subword>()
        {
            new Subword("CGT"),
            new Subword("GTG"),
            new Subword("TGA"),
            new Subword("GAG"),
            new Subword("AGG"),
            new Subword("GGT"),
            new Subword("GTT"),
        };

        public static List<Subword> SubwordsWithTrivialEulerianTrail = new List<Subword>()
        {
            new Subword("ACT"),
            new Subword("CTT"),
            new Subword("TTA"),
            new Subword("TAG")
        };

        public static string TrivialSequence = "TTAG";

        public static List<Subword> SubwordsWithoutEulerianTrail = new List<Subword>()
        {
            new Subword("ACG"),
            new Subword("CGT"),
            new Subword("GTC"),
            new Subword("GTA"),
            new Subword("TCA"),
            new Subword("TAA"),
        };
    }
}