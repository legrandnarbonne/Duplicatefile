using duplicateFile.Classes.Qualifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace duplicateFile.Classes.Qualifier
{
    class SearchTools
    {
        public static void Init()
        {
            if (accentArray != null) return;

            accentArray = buildAccentArray();
        }
        /// <summary>
        /// Search for a list of string in a text
        /// sample test 16.6763 second
        /// </summary>
        /// <param name="txt">text</param>
        /// <param name="refs">array of string to search (grouped by first letter)</param>
        /// <param name="allowPartofWord">if set allow word or part of word match</param>
        /// <param name="length">number of character to use in group</param>
        /// <param name="result">result list</param>
        /// <returns></returns>
        public static void Search4(string txt, string[][] refs, bool allowPartofWord, int length, List<SearchResult> result)
        {

            Parallel.ForEach(refs, (refgroup) =>
            {
                var pref = refgroup[0].Substring(0, length);
                int pos = txt.IndexOf(pref, StringComparison.InvariantCultureIgnoreCase);

                while (pos > -1)
                {
                    foreach (string s in refgroup)
                    {
                        var co = length;

                        while (pos + co < txt.Length &&
                                co < s.Length && (
                                txt[pos + co] == s[co] ||//same caracter
                                txt[pos + co] + 32 == s[co] ||
                                SearchTools.includeAccent(txt[pos + co], s[co])))//include different casse
                            co++;

                        if (co == s.Length)
                        {
                            bool isWord =
                                (pos == 0 || SearchTools.isWordSep(txt[pos - 1]) &&
                                (pos + s.Length > txt.Length || SearchTools.isWordSep(txt[pos + s.Length])));
                            if (isWord || allowPartofWord)
                                result.Add(new SearchResult
                                {
                                    Word = s,
                                    NearNumber = pos > 2 && SearchTools.isNumber(txt[pos - 2]),
                                    isPartOfWord = !isWord,
                                    Position = pos
                                });

                            break;
                        }
                    }

                    if (pos + 1 >= txt.Length) break;
                    pos = txt.IndexOf(pref, pos + 1, StringComparison.InvariantCultureIgnoreCase);
                }
            });

        }
        public static int[][] accentArray;

        public static string XmlToText(string xmlTxt)
        {
            return Regex.Replace(xmlTxt, @"<.*?>", " ");
        }

        public static bool isNumber(char v)
        {
            return v < 57 & v > 48;
        }

        public static bool isWordSep(char c)
        {
            return c < 65 || (c < 97 && c > 90) || (c < 122 && c > 192);
        }

        public static bool includeAccent(char txtChar, char refChar)
        {
            if (txtChar < 192) return false;//no accent

            if (accentArray[refChar] == null) return false;//no char for this caracter

            var co = 0;

            while (co < accentArray[refChar].Length && accentArray[refChar][co] != txtChar)
                co++;

            return co != accentArray[refChar].Length;
        }

        public static int[][] buildAccentArray()
        {
            var result = new int[255][];

            result['e'] = new int[] { 'é', 'è', 'ê', 'ë' };
            result['o'] = new int[] { 'ò', 'ó', 'õ', 'ô', 'ö' };
            result['a'] = new int[] { 'à', 'â', 'á', 'ã', 'å', 'ä' };
            result['i'] = new int[] { 'ì', 'ï', 'í', 'î' };
            result['u'] = new int[] { 'ù', 'ü', 'ú', 'û' };
            result['c'] = new int[] { 'ç' };

            return result;
        }

        public static string[][] buildArray(string[] references, int length)
        {
            var refs = references.ToList<string>();

            List<string[]> result = new List<string[]>();

            while (refs.Count > 0)
            {
                List<string> currentList = new List<string>();

                currentList.Add(refs[0]);
                var pref = refs[0].Substring(0, length);
                refs.RemoveAt(0);

                var off = 0;

                while (refs.Count > off)
                {
                    var currentRef = refs[off];
                    if (currentRef.Substring(0, length) == pref)
                    {
                        currentList.Add(currentRef);
                        refs.RemoveAt(off);
                        off--;
                    }

                    off++;
                }

                result.Add(currentList.ToArray());
            }

            return result.ToArray();
        }

        public static string[] ArrayToLower(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i].ToLower();
            }

            return array;
        }
    }
}
