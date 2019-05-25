# Fluri
A different way to create Uri Objects using Fluent Builder approach.

[![Build Status](https://travis-ci.org/Leanwit/Fluri.svg?branch=master)](https://travis-ci.org/Leanwit/Fluri)

## How to install
dotnet add package FluentUri<br>
See more in https://www.nuget.org/packages/FluentUri/

## How to use
We can modified any part of URI.

Example : Result -> https://localhost:443/?q=books+about+tennis&limit=10
```
new Fluri("google.com/")
  .Add("q=books+about+OOP&limit=50")
  .Over("q=books+about+tennis&limit=10")
  .Scheme("https")
  .Host("localhost")
  .Port(443);
```

Available methods:
```
.Add("q=books+about+OOP&limit=50")
.Over("q=books+about+tennis&limit=10")
.Scheme("https")
.Host("localhost")
.Port(443);
.Fragment(42) or Fragment("test me")
.Path("/hey/you")
.Query("t=1")
.Remove("q")
.AddQuery(expandoObject)
```

Using Expando objects: Result -> http://google/?q=search&text=house&id=356
```
dynamic query = new ExpandoObject();
query.q = "search";
query.text = "house";
query.id = 356;

new Fluri("http://google/").AddQuery(query).GetUrl());
```

You can see all uses in https://github.com/Leanwit/Fluri/blob/master/test/Fluri/TestFluri.cs

## Next steps
Requirements to v1.0.0
https://github.com/Leanwit/Fluri/milestone/1
