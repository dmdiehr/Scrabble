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

        public static List<string> WordFind(this string[] stringArray, string tray, int wordSize = 0, List<Tuple<int, char>> anchorTuples = null, List<Tuple<int, char>> exclusionTuples = null)
        {
            HashSet<string> resultHashSet = new HashSet<string> { };

            string letters = tray.Replace("?", "");
            int numberOfBlanks = tray.Length - letters.Length;

            //Adding anchor letters to the letters string
            if (anchorTuples != null)
            {
                foreach (var anchor in anchorTuples)
                {
                    letters += anchor.Item2;
                }
            }

            letters = letters.ToUpper();

            int[] available = new int[26];
            foreach (char c in letters)
            {
                int index = c - 'A';
                available[index] += 1;
            }

            foreach (string item in stringArray)
            {
                //vet for length
                if ((0 < wordSize) && (item.Length != wordSize))
                    continue;

                //vet for anchors
                if (anchorTuples != null)
                {
                    bool possible = true;
                    foreach (var pair in anchorTuples)
                    {
                        if (item[pair.Item1] != pair.Item2)
                        {
                            possible = false;
                            break;
                        }
                    }
                    if (!possible)
                        continue;
                }
                //vet exclusions yet to be implemented

                int[] tempAvailable = new int[26];
                Array.Copy(available, tempAvailable, 26);

                int wildcardsLeft = numberOfBlanks;
                int[] count = new int[26];
                bool ok = true;
                foreach (char c in item.ToCharArray())
                {
                    int index = c - 'A';
                    count[index] += 1;
                    if (count[index] - tempAvailable[index] > wildcardsLeft)
                    {
                        ok = false;
                        break;
                    }
                    else if (count[index] > tempAvailable[index])
                    {
                        wildcardsLeft--;
                        tempAvailable[index]++;
                    }
                }
                if (ok)
                {
                    resultHashSet.Add(item);
                }
            }
            return resultHashSet.ToList();
        }
    }
}
