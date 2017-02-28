using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ExtentionMethods
{
    public static class Extensions
    {
        //public static List<string> WordFind(this string[] stringArray, string tray, int wordSize = 0, List<Tuple<int, char>> anchorTuples = null)
        //{
        //    HashSet<string> resultHashSet = new HashSet<string> { };

        //    string letters = tray.Replace("?", "");
        //    int numberOfBlanks = tray.Length - letters.Length;

        //    //Adding anchor letters to the letters string
        //    if (anchorTuples != null)
        //    {
        //        foreach (var anchor in anchorTuples)
        //        {
        //            letters += anchor.Item2;
        //        }
        //    }

        //    letters = letters.ToUpper();
  
        //    List<string> stringList = stringArray.WordFindWithWildCards(letters, numberOfBlanks);
        //    resultHashSet.UnionWith(stringList);


        //    // vet resultList with wordSize
        //    if (wordSize > 0)
        //    {
        //        HashSet<string> vettedHashSet = new HashSet<string>(resultHashSet);
        //        foreach (var result in resultHashSet)
        //        {
        //            if (result.Length != wordSize)
        //            {
        //                vettedHashSet.Remove(result);
        //            }
        //        }
        //        resultHashSet = new HashSet<string>(vettedHashSet);
        //    }
        //    //vett resultList with anchors
        //    if (anchorTuples != null)
        //    {
        //        HashSet<string> vettedHashSet = new HashSet<string>(resultHashSet);
        //        foreach (var pair in anchorTuples)
        //        {
        //            foreach (var result in resultHashSet)
        //            {
        //                if (result[pair.Item1] != pair.Item2)
        //                {
        //                    vettedHashSet.Remove(result);
        //                }
        //            }
        //        }
        //        resultHashSet = new HashSet<string>(vettedHashSet);
        //    }
        //    return resultHashSet.ToList();
        //}

        private static List<string> SimpleWordFind(this string[] stringArray, string letters)
        {
            List<string> returnList = new List<string>();

            //Convert the letters string into an integer array
            //where the integer at each position in the array represents
            //the number of times the corresponding letter at that position
            //in the alphabet is in the array           
            int[] available = new int[26];
            foreach (char c in letters)
            {
                int index = c - 'A';
                available[index] += 1;
            }

            //Searches through the dictionary one time
            //converts each entry into an int array
            //add the entry into the result list if the letters array
            //has enough of each letter
            foreach (string item in stringArray)
            {
                int[] count = new int[26];
                bool ok = true;
                foreach (char c in item.ToCharArray())
                {
                    int index = c - 'A';
                    count[index] += 1;
                    if (count[index] > available[index])
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok)
                {
                    returnList.Add(item);
                }
            }

            return returnList;
        }

        private static List<string> WordFindWithWildCards(this string[] stringArray, string letters, int wildcards = 0)
        {
            List<string> returnList = new List<string>();
       
            int[] available = new int[26];
            foreach (char c in letters)
            {
                int index = c - 'A';
                available[index] += 1;
            }

            foreach (string item in stringArray)
            {                
                int[] tempAvailable = new int[26];             
                Array.Copy(available, tempAvailable, 26);

                int wildcardsLeft = wildcards;
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
                    returnList.Add(item);
                }
            }

            return returnList;
        }

        public static List<string> WildCardCombinations(this string thisString, string availableLetters, List<string> currentStubs = null )
        {
            //returns all the ways (without duplicates) that the availableLetters (where '?' is a wildcard) can be arranged to match the base string

            #region //Custom exceptions for input validation
            if (!availableLetters.All(c => Char.IsLetter(c) || c == '?'))
                throw new ArgumentException("letters parameter must only contain English letters or a wild card indicator (?)");
            if (!thisString.All(Char.IsLetter))
                throw new InvalidOperationException("WildCardMatches method should only be called on strings composed entirely of English letters");
            if (thisString.Length > availableLetters.Length)
                throw new ArgumentException("letters parameter must be as long or longer than this string");
            if (currentStubs != null && !currentStubs.All(x => x.Length == currentStubs[0].Length))
                throw new Exception("You messed up, the strings in your stubs are not all the same length");
            #endregion

            if (currentStubs == null)
            {
                currentStubs = new List<string> {""};
            }

            List<string> returnList = new List<string>();


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
                    returnList.Add(currentStub + thisString[currentStub.Length]);
                }
                if(remainingLetters.Contains('?'))
                {
                    returnList.Add(currentStub + '?');
                }
            }

            //if the strings in the return list are equal to the target string return the returnlist
            //if not, use the return list as the currentstubs argument for the next recursion
            if (returnList[0].Length == thisString.Length)
                return returnList.Distinct().ToList();
            else
                return WildCardCombinations(thisString, availableLetters, returnList);
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
