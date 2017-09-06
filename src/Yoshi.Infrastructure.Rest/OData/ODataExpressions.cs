using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Yoshi.Infrastructure.Rest.OData
{
    public class ODataExpressions
    {
        #region Variables --------------------
        private static string FilterPattern = @"(?<Filter>" +
        "\n" + @"     (?<Resource>.+?)\s+" +
        "\n" + @"     (?<Operator>lk|eq|ne|gt|ge|lt|le|add|sub|mul|div|mod)\s+" +
        "\n" + @"     '?(?<Value>.+?)'?" +
        "\n" + @")" +
        "\n" + @"(?:" +
        "\n" + @"    \s*$" +
        "\n" + @"   |\s+(?:or|and|not)\s+" +
        "\n" + @")" +
        "\n";
        private static Regex FilterRegex = new Regex(FilterPattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        private static string FilterFormat = @"${Resource}§${Operator}§${Value}" + Environment.NewLine;
        #endregion
        #region Public methods ---------------
        public static ODataQuery Parse(string input)
        {
            ODataQuery options = new ODataQuery();

            input = HttpUtility.UrlDecode(input);
            string[] resources = input.Split(new[] { '?' }, 2);
            string filter = resources.Length == 2 ? resources[1] : null;
            if (filter == null)
            {
                return options;
            }

            var parts = filter.Split('&');

            foreach (var item in parts)
            {
                var elementParts = item.Split(new[] { '=' }, 2);

                string key = elementParts[0];
                string value = elementParts.Length == 2 ? elementParts[1] : string.Empty;

                switch (key)
                {
                    case "$filter":
                        options.Filters = ProcessFilters(value);
                        break;
                    case "$pagesize":
                        options.PagingOptions.PageSize = ProcessTop(value);
                        break;
                    case "$pagenumber":
                        options.PagingOptions.PageNumber = ProcessSkip(value);
                        break;
                    default:
                        break;
                }
            }

            return options;
        }
        #endregion
        #region Private methods --------------
        private static Filter[] ProcessFilters(string value)
        {
            List<Filter> result = new List<Filter>();
            var parse = FilterRegex.Replace(value, FilterFormat);
            var reader = new StringReader(parse);
            var item = reader.ReadLine();

            while (!string.IsNullOrWhiteSpace(item))
            {
                var line = item.Split('§');
                var filter = new Filter();
                filter.Attribute = line[0];
                filter.Operator = (FilterOperator)Enum.Parse(typeof(FilterOperator), line[1]);
                filter.Value = line[2];
                result.Add(filter);
                item = reader.ReadLine();
            }

            return result.ToArray();
        }
        private static int ProcessTop(string value)
        {
            return GetPositiveInteger("pageSize", value);
        }
        private static int ProcessSkip(string value)
        {
            return GetPositiveInteger("pageNumber", value);
        }
        private static int GetPositiveInteger(string key, string value)
        {
            int intValue;
            if (!int.TryParse(value, out intValue) || intValue < 0)
            {
                throw new Exception(String.Format(" {1} must be positive", key));
            }
            return intValue;
        }
        #endregion
    }
}
