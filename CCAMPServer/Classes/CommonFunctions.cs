using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CCAMPServer.Classes
{
    public static class CommonFunctions
    {
        public static string GenerateQueryString<T>(T sourceObject)
        {
            Type type = sourceObject.GetType();
            var strToReturn = new StringBuilder();
            var arrProperties = type.GetProperties();

            foreach (PropertyInfo prop in arrProperties)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    CCAMAttribute customAttribute = attr as CCAMAttribute;
                    if (customAttribute != null)
                    {
                        string customName = customAttribute.GetName();
                        bool isOptional = customAttribute.IsOPtional();

                        var currValue = prop.GetValue(sourceObject);

                        if (currValue != null)
                        {
                            strToReturn.Append( $"&{customName}={currValue}");
                        }
                    }
                }

            }

            return strToReturn.ToString();
        }

        public static string FormatSearchQuery(string query)
        {
            var search = string.Format(Constants.API.YOUTUBE_SEARCH, query);
            var searchPlusKey = string.Format(Constants.API.YOUTUBE_KEY_PARAM, search, Constants.API.YOUTUBE_API_KEY);
            var request = string.Format(Constants.API.YOUTUBE_BASE_REQUEST, searchPlusKey);

            return request;
        }
    }
}
