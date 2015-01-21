using System.Collections;
using System.Linq;
using BIO_Z7.Exceptions;

namespace BIO_Z7
{
    public class Subword
    {
        private readonly string _inputString;
        public static readonly ArrayList ValidChars = new ArrayList {'A', 'C', 'T', 'G'};

        public Subword(string inputString)
        {
            if (inputString == null || !IsAValidSubword(inputString))
                throw new BadSymbolException();
            _inputString = inputString;
        }

        public int Length
        {
            get { return _inputString.Length; }
        }

        private bool IsAValidSubword(string inputString)
        {
            return inputString.All(c => ValidChars.Contains(c));
        }

        public string GetPrefix()
        {
            return _inputString.Substring(0, _inputString.Length-1);
        }

        public string GetPostfix()
        {
            return _inputString.Substring(1, _inputString.Length-1);
        }

        public override string ToString()
        {
            return _inputString;
        }
    }
}