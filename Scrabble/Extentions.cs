using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtentionMethods
{
    public static class Extensions
    {
        public static List<string> WordFind(this String[] stringArray, string tray, int wordSize = 0, List<Tuple<int, char>> anchorTuples = null)
        {
            string letters = tray;
            if (anchorTuples != null)
            {
                foreach (var anchor in anchorTuples)
                {
                    letters += anchor.Item2;
                }
            }

            List<string> resultList = new List<string> { };
            int[] available = new int[26];

            foreach (char c in letters)
            {
                int index = c - 'a';
                available[index] += 1;
            }
            foreach (string item in stringArray)
            {
                int[] count = new int[26];
                bool ok = true;
                foreach (char c in item.ToCharArray())
                {
                    int index = c - 'a';
                    count[index] += 1;
                    if (count[index] > available[index])
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok)
                {
                    resultList.Add(item);
                }
            }
            // vet resultList with wordSize
            if (wordSize > 0)
            {
                List<string> vettedList = resultList.ToList();
                foreach (var result in resultList)
                {
                    if (result.Length != wordSize)
                    {
                        vettedList.Remove(result);
                    }
                }
                resultList = vettedList.ToList();
            }
            //vett resultList with anchors
            if (anchorTuples != null)
            {
                List<string> vettedList = resultList.ToList();
                foreach (var pair in anchorTuples)
                {
                    foreach (var result in resultList)
                    {
                        if (result[pair.Item1] != pair.Item2)
                        {
                            vettedList.Remove(result);
                        }
                    }
                }
                resultList = vettedList.ToList();
            }
            return resultList;
        }

    }
}
