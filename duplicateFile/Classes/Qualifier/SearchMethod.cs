using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace duplicateFile.Classes.Qualifier
{
    static class SearchMethod
    {
        public static int Search1(string txt, string[] refs)
        {
            int score = 0;

            Parallel.ForEach(refs, (reference) =>
            {
                int pos = txt.IndexOf(reference, StringComparison.CurrentCultureIgnoreCase);

                while (pos > -1)
                {
                    score++;
                    pos = txt.IndexOf(reference, pos + 1, StringComparison.CurrentCultureIgnoreCase);
                }
            });

            return score;
        }

        public static int Search2(string txt, string[] refs)
        {
            int score = 0;
            txt = txt.ToLower();

            Parallel.ForEach(refs, (reference) =>
            {
                int pos = txt.IndexOf(reference);

                while (pos > -1)
                {
                    score++;
                    pos = txt.IndexOf(reference, pos + 1);
                }
            });

            return score;
        }

        public static int Search3(string txt, string[][] refs, int length)
        {
            int score = 0;

            Parallel.ForEach(refs, (refgroup) =>
            {
                var pref = refgroup[0].Substring(0, length);
                int pos = txt.IndexOf(pref);

                while (pos > -1)
                {
                    foreach (string s in refgroup)
                        if (pos + s.Length <= txt.Length)
                            score += string.Equals(txt.Substring(pos, s.Length), s, StringComparison.CurrentCultureIgnoreCase) ? 1 : 0;

                    pos = txt.IndexOf(pref, pos + 1);
                }
            });

            return score;
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
        public static int Search4(string txt, string[][] refs, bool allowPartofWord, int length)
        {
            if (txt == null) return 0;
            var note = 0;

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
                                (pos + s.Length >= txt.Length || SearchTools.isWordSep(txt[pos + s.Length])));
                            if (isWord || allowPartofWord)
                                note += s.Length;
                            break;
                        }
                    }

                    if (pos + 1 >= txt.Length) break;
                    pos = txt.IndexOf(pref, pos + 1, StringComparison.InvariantCultureIgnoreCase);
                }
            });

            return note;
        }

        /// <summary>
        /// Search for a list of string in a text
        /// Faster but full word only
        /// Sample 14,496 for 20000 iteration
        /// </summary>
        /// <param name="txt">text</param>
        /// <param name="refs">array of string to search (grouped by first letter)</param>
        /// <param name="length">number of character to use in group</param>
        /// <returns></returns>
        public static void SearchWord(string txt, string[][] refs, int length, List<SearchResult> result)
        {
            //var refgroup = refs[2];
            Parallel.ForEach(refs, (refgroup) =>
            {
                var pref = refgroup[0].Substring(0, length);
                int pos = 0;
                while (pos < txt.Length)
                {
                    if (refgroup[0][0] == txt[pos] & (pos == 0 || SearchTools.isWordSep(txt[pos - 1])))//new word start and first char equal group letter

                    {
                        var txtLength = txt.Length;
                        var co = 1;
                        string word = txt[pos].ToString();

                        while ((pos + co < txt.Length && !SearchTools.isWordSep(txt[pos + co])))//search end of current word extract word
                        {
                            word += txt[pos + co];
                            co++;
                        }


                        foreach (string s in refgroup)
                        {
                            if (word.Length == s.Length && string.Equals(word, s, StringComparison.CurrentCultureIgnoreCase))
                            {
                                result.Add(new SearchResult
                                {
                                    Word = s,
                                    NearNumber = pos > 2 && SearchTools.isNumber(txt[pos - 2]),
                                    Position = pos
                                });

                                pos += word.Length;
                                break;
                            }
                        }
                    }

                    pos++;
                }
            });
        }
        /// <summary>
        /// Return number of occurence
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static int SearchRegExp(string txt, string pattern)
        {
            Regex rgx = new Regex(pattern);

            return rgx.Matches(txt).Count;
        }


    }
}
