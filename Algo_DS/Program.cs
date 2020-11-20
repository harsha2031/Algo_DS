using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algo_DS.Models;

namespace Algo_DS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("World!");
            Console.WriteLine("Palindrome Checking: " + PalindromeCheck("madam"));
            Console.WriteLine("CeasarCypherEncrypter: " + CeasarCypherEncryptor("abc", 3));
            Console.WriteLine("Longest Pallindromic Substring: " + LongestPallidromicSubstring("xyzzyxyur"));
            Console.WriteLine("Anagrams: " + GroupAnagrams(new List<string>() { "foo", "ofo", "tac", "cat" }).Count);
            Console.WriteLine("LongestSubstringWithoutDuplicate: " + LongestSubstringWithoutDuplicate("clementisacap"));
            Console.WriteLine("UnderScorify : " + UnderscorifySubstring("testthis is a testtest to see if testtesttest is works", "test"));
            Console.WriteLine("Two Sum :" + SumOfTwo(new int[] { 10, 15, 3, 7 }, 17));

            Console.WriteLine("ProductExceptItSelf" + ProductOfOtherThanCurrent(new int[] { 1, 2, 3, 4, 5 }));
            Console.ReadLine();
        }

        public static bool PalindromeCheck(string str)
        {
            int left = 0;
            int right = str.Length - 1;
            if (str.Length == 0)
                return true;
            while(left <= right)
            {
                if (str[left] != str[right])
                    return false;
                left++;
                right--;
            }
            return true;
        }
        public static string CeasarCypherEncryptor(string str,int key)
        {
            int newkey = key % 26;
            char[] newLetters = new char[str.Length];
            string alphabets = "abcdefghijklmnopqrstuvwxyz";
            for(int i = 0;i<= str.Length - 1; i++)
            {
                newLetters[i] = GetNewLetter(str[i], newkey, alphabets);
            }
            return new string(newLetters);
        }

        public static char GetNewLetter(char letter, int key, string alphabets)
        {
            int newLetterCode = alphabets.IndexOf(letter) + key;
            return alphabets[newLetterCode % 26];
        }

        public static string LongestPallidromicSubstring(string str)
        {
            if(str.Length == 0 || str == null)
            {
                return str;
            }
            int start = 0;
            int end = 0;
            for(int i = 0;i< str.Length; i++)
            {
                int len = Math.Max(ExpandFromMiddle(str, i, i), ExpandFromMiddle(str, i, i + 1));
                if(len > end - start)
                {
                    start = i - (len - 1) / 2;
                    end = i + len / 2;
                }
            }
            return str.Substring(start, end - start + 1);
        }
        public static int ExpandFromMiddle(string s, int low, int high)
        {
            if (low > high || s == null)
                return 0;
            while( low >= 0 && high < s.Length && s[low] == s[high])
            {
                low--;
                high++;
            }
            return high - low - 1;
        }

        public static List<List<string>> GroupAnagrams(List<string> words)
        {
            Dictionary<string, List<string>> anagrams = new Dictionary<string, List<string>>();
            foreach(string word in words)
            {
                char[] charArray = word.ToCharArray();
                Array.Sort(charArray);
                string sortedWord = new string(charArray);
                if (anagrams.ContainsKey(sortedWord))
                    anagrams[sortedWord].Add(word);
                else
                    anagrams[sortedWord] = new List<string>() { word };
            }
            return anagrams.Values.ToList();
        }

        public static string LongestSubstringWithoutDuplicate(string str)
        {
            Dictionary<char, int> lastseen = new Dictionary<char, int>();
            int[] longest = { 0, 1 };
            int startindex = 0;
            for(int i = 0;i< str.Length; i++)
            {
                char c = str[i];
                if (lastseen.ContainsKey(c))
                {
                    startindex = Math.Max(startindex, lastseen[c] + 1);
                }
                if(longest[1] - longest[0] < i + 1 - startindex)
                {
                    longest = new int[] { startindex, i + 1 };
                }
                lastseen[c] = i;
            }
            string result = str.Substring(longest[0], longest[1] - longest[0]);
            return result;
        }

        public static string UnderscorifySubstring(string str, string substring)
        {
            List<int[]> locations = Collapse(GetLocations(str, substring));
            return Underscorify(str, locations);
        }
        public static List<int[]> GetLocations(string str, string substring)
        {
            List<int[]> locations = new List<int[]>();
            int startindex = 0;
            while(startindex < str.Length)
            {
                int nextindex = str.IndexOf(substring, startindex);
                if(nextindex != -1)
                {
                    locations.Add(new int[] { nextindex, nextindex + substring.Length });
                    startindex = nextindex + 1;
                }
                else
                {
                    break;
                }
            }
            return locations;
        }
        public static List<int[]> Collapse(List<int[]> locations)
        {
            if(locations.Count == 0)
            {
                return locations;
            }
            List<int[]> newLocations = new List<int[]>();
            newLocations.Add(locations[0]);
            int[] previous = newLocations[0];
            for(int i = 1; i < locations.Count; i++)
            {
                int[] current = locations[i];
                if(current[0] <= previous[1])
                {
                    previous[1] = current[1];
                }
                else
                {
                    newLocations.Add(current);
                    previous = current;
                }
            }
            return newLocations;
        }

        public static string Underscorify(string str, List<int[]> locations)
        {
            int locationsIndex = 0;
            int stringIndex = 0;
            bool inBetweenUnderscores = false;
            List<string> finalChars = new List<string>();
            int i = 0;
            while (stringIndex < str.Length && locationsIndex < locations.Count)
            {
                if (stringIndex == locations[locationsIndex][i])
                {
                    finalChars.Add("_");
                    inBetweenUnderscores = !inBetweenUnderscores;
                    if (!inBetweenUnderscores)
                    {
                        locationsIndex++;
                    }
                    i = i == 1 ? 0 : 1;
                }
                finalChars.Add(str[stringIndex].ToString());
                stringIndex += 1;
            }

            if (locationsIndex < locations.Count)
                finalChars.Add("_");
            else if (stringIndex < str.Length)
                finalChars.Add(str.Substring(stringIndex));
            return String.Join("", finalChars);
        }

        #region Daily Coding Problem

       // Given a list of numbers and a number k, return whether any two numbers from the list add up to k.

       // For example, given[10, 15, 3, 7] and k of 17, return true since 10 + 7 is 17.

        public static bool SumOfTwo(int[] input, int k)
        {
            HashSet<int> set = new HashSet<int>();
            foreach(int n in input)
            {
                int comp = k - n;
                if(set.Contains(n))
                {
                    return true;
                }
                else
                {
                    set.Add(comp);
                }
            }

            return false;
        }

      //  Given an array of integers, return a new array such that each element at index i of the new array is the product of all the numbers in the original array except the one at i.

      //  For example, if our input was[1, 2, 3, 4, 5], the expected output would be[120, 60, 40, 30, 24]. If our input was [3, 2, 1], the expected output would be[2, 3, 6].

        public static int[] ProductOfOtherThanCurrent(int[] input)
        {
            int[] result = new int[input.Length];
            result[0] = 1;
            for(int i = 1;i<input.Length;i++)
            {
                result[i] = input[i - 1] * result[i - 1];
            }
            int r = 1;
            for(int i = input.Length - 1;i>=0;i--)
            {
                result[i] = r * result[i];
                r = r * input[i];
            }
            return result;
        }

       // Given the root to a binary tree, implement serialize(root), which serializes the tree into a string,
       // and deserialize(s), which deserializes the string back into the tree.

        public static string Serialize(TreeNode tree)
        {
            StringBuilder sb = new StringBuilder();
            PreOrderTree(tree, sb);
            return sb.ToString().Trim(',');
        }

        public static void PreOrderTree(TreeNode tree, StringBuilder sb)
        {
            if(tree == null)
            {
                sb.Append("#,");
                return;
            }
            sb.Append(tree.value);
            sb.Append(',');
            PreOrderTree(tree.left, sb);
            PreOrderTree(tree.right, sb);
        }

        public static TreeNode Deserialize(string sb)
        {
            string[] nodes = sb.Split(',');
            int position = 0;
            return DeserializeFromArray(nodes, ref position);
        }

        public static TreeNode DeserializeFromArray(string[] nodes, ref int position)
        {
            if(nodes[position] == "#")
            {
                position++;
                return null;
            }
            var node = new TreeNode(int.Parse(nodes[position++]));
            node.left = DeserializeFromArray(nodes, ref position);
            node.right = DeserializeFromArray(nodes, ref position);
            return node;
        }


        #endregion

    }
}
