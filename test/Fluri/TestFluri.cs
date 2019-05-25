using System;
using System.Dynamic;

namespace Test
{
    using Xunit;
    using Fluri;

    public class TestFluri
    {
        [Fact]
        public void TestBuildUri()
        {
            var Fluri = new Fluri("google.com/")
                .Add("q=books+about+OOP&limit=50")
                .Over("q=books+about+tennis&limit=10")
                .Scheme("https")
                .Host("localhost")
                .Port(443);
            Assert.Equal("https://localhost:443/?q=books+about+tennis&limit=10", Fluri.GetUrl());
        }
        
        [Fact]
        public void TestReplacesScheme()
        {
            Assert.Equal("https://google.com/", new Fluri("http://google.com/").Scheme("https").GetUrl());
        }
        
        [Fact]
        public void TestReplacesHost()
        {
            Assert.Equal("http://localhost/", new Fluri("http://google.com/").Host("localhost").GetUrl());
        }

        [Fact]
        public void TestReplacesPort()
        {
            Assert.Equal("http://localhost:443/", new Fluri("http://localhost/").Port(443).GetUrl());
        }
        
        [Fact]
        public void TestReplacesFragment()
        {
            Assert.Equal("http://localhost/a/b#test%20me", new Fluri("http://localhost/a/b#before").Fragment("test me").GetUrl());
            Assert.Equal("http://localhost/#42", new Fluri("http://localhost/").Fragment(42).GetUrl());
        }
        
        [Fact]
        public void TestSetsPath()
        {
            Assert.Equal("http://localhost/hey/you?i=8#test", new Fluri("http://localhost/hey?i=8#test").Path("/hey/you").GetUrl());
        }
        
        [Fact]
        public void TestSetsQuery()
        {
            Assert.Equal("http://localhost/hey?t=1#test", new Fluri("http://localhost/hey?i=8#test").Query("t=1").GetUrl());
        }
        
        [Fact]
        public void TestAddQueryParam()
        {
            var valueOne = "a=1&b=2";
            var valueTwo = "a=3";
            Assert.Equal("http://google/?a=1&a=3&b=2", new Fluri("http://google/").Add(valueOne).Add(valueTwo).GetUrl());
        }
        
        [Fact]
        public void TestFluryCtorWithoutParams()
        {
          
            Assert.Equal("https://localhost/", new Fluri().Scheme("https").GetUrl());
        }
        
        [Fact]
        public void TestQueryAddParamsByExpandoObject()
        {
            dynamic query = new ExpandoObject();
            query.q = "folder";
            query.text = "hi";
            query.id = 356;
            
            Assert.Equal("http://google/?q=folder&text=hi&id=356", new Fluri("http://google/").AddQuery(query).GetUrl());
        }
        
        [Fact]
        public void TestQueryAddParamsByTwoExpandoObject()
        {
            dynamic queryOne = new ExpandoObject();
            queryOne.q = "folder1";
            queryOne.text = "hi1";
            queryOne.id = 3561;
            
            dynamic queryTwo = new ExpandoObject();
            queryTwo.q = "folder2";
            queryTwo.text = "hi2";
            queryTwo.id = 3562;

            Assert.Equal("http://google/?q=folder1&q=folder2&text=hi1&text=hi2&id=3561&id=3562", new Fluri("http://google/").AddQuery(queryOne).AddQuery(queryTwo).GetUrl());
        }
        
        [Fact]
        public void TestRemoveKeyString()
        {
            dynamic query = new ExpandoObject();
            query.q = "testing";
            query.folder = "naming";

            Assert.Equal("http://google/?q=testing", new Fluri("http://google/").AddQuery(query).RemoveQuery("folder").GetUrl());
        }
        
        [Fact]
        public void TestGetUri()
        {
            dynamic query = new ExpandoObject();
            query.q = "testing";
            query.folder = "naming";

            Uri uri = new Fluri("http://google/").AddQuery(query).GetUri();
            Assert.NotNull(uri);
            Assert.Equal("http",uri.Scheme);
            Assert.Equal("google",uri.Host);
            Assert.Equal("?q=testing&folder=naming",uri.Query);

        }
    }
}