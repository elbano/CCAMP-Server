using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCAMPServerModel.Extensions
{
    public static class StringExtension
    {
        public static bool ContainsAny(this String str, IEnumerable<string> searchTerms)
        {
            return searchTerms.Any(searchTerm => str.ToLower().Contains(searchTerm.ToLower()));
        }

        public static bool ContainsAll(this String str, IEnumerable<string> searchTerms)
        {
            return searchTerms.All(searchTerm => str.ToLower().Contains(searchTerm.ToLower()));
        }
    }
}
