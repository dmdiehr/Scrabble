using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ExtentionMethods
{
    public static class Extensions
    {
        public static List<string> WordFind(this string[] stringArray, string tray, int wordSize = 0, List<Tuple<int, char>> anchorTuples = null)
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


            List<string> stringList = stringArray.WordFindWithWildCards(letters, numberOfBlanks);
            resultHashSet.UnionWith(stringList);


            // vet resultList with wordSize
            if (wordSize > 0)
            {
                HashSet<string> vettedHashSet = new HashSet<string>(resultHashSet);
                foreach (var result in resultHashSet)
                {
                    if (result.Length != wordSize)
                    {
                        vettedHashSet.Remove(result);
                    }
                }
                resultHashSet = new HashSet<string>(vettedHashSet);
            }
            //vett resultList with anchors
            if (anchorTuples != null)
            {
                HashSet<string> vettedHashSet = new HashSet<string>(resultHashSet);
                foreach (var pair in anchorTuples)
                {
                    foreach (var result in resultHashSet)
                    {
                        if (result[pair.Item1] != pair.Item2)
                        {
                            vettedHashSet.Remove(result);
                        }
                    }
                }
                resultHashSet = new HashSet<string>(vettedHashSet);
            }
            return resultHashSet.ToList();
        }

        public static List<string> SimpleWordFind(this string[] stringArray, string letters)
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

        public static List<string> WordFindWithWildCards(this string[] stringArray, string letters, int wildcards = 0)
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

    }
}
