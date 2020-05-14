using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace IoRecipes.SlugGenerator
{
    public class Slug
    {
        public const int DEFAULT_MAX_LENGTH = 45; 

        private string GeneratedSlug;
        private List<string> ExistingSlugs;
        private int? CustomMaxLength = null;

        static Slug()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public Slug(string input)
        {
            Setup(input);
        }

        public Slug(string input, int maxLength)
        {
            CustomMaxLength = maxLength;
            Setup(input);
        }

        private void Setup(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (input.Length == 0) throw new ArgumentOutOfRangeException(nameof(input), "Cannot be empty");
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentOutOfRangeException(nameof(input), "Cannot be white space");

            string str = RemoveAccent(input).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= MaxLength ? str.Length : MaxLength).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            GeneratedSlug = Regex.Replace(str, @"-+", "-"); // hyphens   

            ExistingSlugs = new List<string>();
        }

        /// <summary>
        /// Based on the string input in the constructor and any existing slugs that were passed it,
        /// it generates a url and human friendly "slug." 
        /// For example, Jack! Jones! becomes 'jack-jones'
        /// </summary>
        /// <param name="randomGuidOnEmpty">if a slug cannot be generated, return guid string</param>
        /// <returns>url and human friendly string</returns>
        public string GenerateSlug(bool randomGuidOnEmpty = false)
        {
            if (string.IsNullOrEmpty(GeneratedSlug))
            {
                if (randomGuidOnEmpty) return Guid.NewGuid().ToString();
                return null;
            }

            if (!(ExistingSlugs?
                .Any(s => s.Equals(GeneratedSlug, StringComparison.InvariantCultureIgnoreCase)) == true))
                return GeneratedSlug;

            var highestEndingNumber = ExistingSlugs?.Select(s =>
            {
                var lastPart = s.Replace($"{GeneratedSlug}-", "");
                if (int.TryParse(lastPart, out var parsedLastPart))
                    return parsedLastPart;
                return 0;
            }).Max() ?? 0;
                
            var newNumber = highestEndingNumber + 1;
            return $"{GeneratedSlug}-{newNumber}";
        }

        public override string ToString()
            => GenerateSlug();

        public void SetExistingSlugs(IEnumerable<string> existingSlugs)
        {
            if (existingSlugs is null) return; 
            ExistingSlugs = existingSlugs.ToList(); 
        }

        public static bool IsValidSlug(string input)
           => Regex.IsMatch(input, @"^[a-z0-9-]+$");

        static public string RemoveAccent(string input)
        {
            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(input);
            return System.Text.Encoding.UTF8.GetString(tempBytes);
        }

        private int MaxLength => CustomMaxLength ?? DEFAULT_MAX_LENGTH;

    }
}
