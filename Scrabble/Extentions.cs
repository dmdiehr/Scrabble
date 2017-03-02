using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Scrabble;

namespace ExtentionMethods
{
    public static class Extensions
    {
        public static List<string> WildCardCombinations(this string thisString, string availableLetters, ICollection<string> currentStubs = null )
        {
            //returns all the ways (without duplicates) that the availableLetters (where '?' is a wildcard) can be arranged to match the base string

            #region //Custom exceptions for input validation
            if (!availableLetters.All(c => Char.IsLetter(c) || c == '?'))
                throw new ArgumentException("letters parameter must only contain English letters or a wild card indicator (?)");
            if (!thisString.All(Char.IsLetter))
                throw new InvalidOperationException("WildCardMatches method should only be called on strings composed entirely of English letters");
            if (thisString.Length > availableLetters.Length)
                throw new ArgumentException("letters parameter must be as long or longer than this string");
            if (currentStubs != null && !currentStubs.All(x => x.Length == currentStubs.First().Length))
                throw new Exception("You messed up, the strings in your stubs are not all the same length");
            #endregion

            if (currentStubs == null)
            {
                currentStubs = new HashSet<string> {""};
            }

            HashSet<string> returnHashSet = new HashSet<string>();


            foreach (string currentStub in currentStubs)
            {
                //subtract the current stub from availableLetters
                List<char> remainingLetters = availableLetters.ToList();
                foreach (char letter in currentStub)
                {
                    remainingLetters.Remove(letter);
                }

                //check to see if remaining letters have the next letter and/or a ? to add to the stub
                //if so, add these to the return list
                if(remainingLetters.Contains(thisString[currentStub.Length]))
                {
                    returnHashSet.Add(currentStub + thisString[currentStub.Length]);
                }
                if(remainingLetters.Contains('?'))
                {
                    returnHashSet.Add(currentStub + '?');
                }
            }

            //if the strings in the return list are equal to the target string return the returnlist
            //if not, use the return list as the currentstubs argument for the next recursion
            if (returnHashSet.First().Length == thisString.Length)
                return returnHashSet.ToList();
            else
                return WildCardCombinations(thisString, availableLetters, returnHashSet);
        }        
    }
}
