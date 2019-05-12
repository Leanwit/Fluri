using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Fluri
{
    using System.Collections.Generic;
    using System.Linq;

    public class Fluri
    {
        private UriBuilder _uriObject;
        
        public Fluri(string url)
        {
            this._uriObject = new UriBuilder(url);
            if (this._uriObject.Uri.IsDefaultPort)
            {
                this._uriObject.Port = -1;
            }
        }
        
        public Fluri()
        {
            this._uriObject = new UriBuilder();
            this._uriObject.Port = -1;
        }

        public Fluri Add(string query)
        {
            if (string.IsNullOrWhiteSpace(this._uriObject.Query))
            {
                this._uriObject.Query = query;
                return this;
            }
            this._uriObject.Query = $"{this._uriObject.Query}&{query}";
            return this;
        }

        private static string ReplaceSpaces(string value)
        {
            return value.Replace(" ", "+");
        }

        public Fluri Over(string query)
        {
            this._uriObject.Query = query;
            return this;
        }

        public Fluri Scheme(string value)
        {
            this._uriObject.Scheme = value;
            
            return this;
        }

        public Fluri Host(string value)
        {
            this._uriObject.Host = value;
            return this;
        }

        public Uri GetUri()
        {
            return new Uri(this.GetUrl());
        }

        public Fluri Port(int value)
        {
            this._uriObject.Port = value;
            return this;
        }

        public Fluri Fragment(string value)
        {
            this._uriObject.Fragment = value.Replace(" ", "%20");
            return this;
        }

        public Fluri Fragment(int value)
        {
            this._uriObject.Fragment = value.ToString();
            return this;
        }

        public Fluri Path(string value)
        {
            this._uriObject.Path = value;
            return this;
        }

        public Fluri Query(string value)
        {
            this._uriObject.Query = value;
            return this;
        }

        public string GetUrl()
        {
            return this._uriObject.ToString();
        }
    }
}