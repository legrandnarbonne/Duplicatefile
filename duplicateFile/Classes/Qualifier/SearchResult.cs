
namespace duplicateFile.Classes.Qualifier
{
    class SearchResult
    {
        /// <summary>
        /// Word find
        /// </summary>
        public string Word { get; set; }
        /// <summary>
        /// Is there a number before word
        /// </summary>
        public bool NearNumber { get; set; }
        /// <summary>
        /// Position in text
        /// </summary>
        public int Position { get; set; }
        /// <summary>
        /// set to false if is word
        /// </summary>
        public bool isPartOfWord { get; set; }
    }
}
