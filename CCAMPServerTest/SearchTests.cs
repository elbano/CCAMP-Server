using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CCAMPServer.Controllers;


namespace CCAMPServerTest
{
    public class SearchTests
    {

        [Fact]
        public void CallSearchAPI()
        {
            var searchController = new SearchController();
            var response = searchController.Search();

            Assert.NotEqual(response, string.Empty);
        }


    }
}

