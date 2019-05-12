using System.Collections;
using System.Collections.Generic;

namespace Test
{
    using Fluri;
    using Xunit;

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
            Assert.Equal("http://google/?a=1&b=2&a=3", new Fluri("http://google/").Add(valueOne).Add(valueTwo).GetUrl());
        }
        
        [Fact]
        public void TestFluryCtorWithoutParams()
        {
          
            Assert.Equal("https://localhost/", new Fluri().Scheme("https").GetUrl());
        }
    }
}