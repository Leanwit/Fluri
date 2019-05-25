namespace Fluri
{
    using System;
    using System.Collections.Specialized;
    using System.Dynamic;
    using System.Text;
    using System.Web;

    public class Fluri
    {
        private UriBuilder _uriObject;
        private NameValueCollection _queryParameters;

        public Fluri(string url)
        {
            this._uriObject = new UriBuilder(url);
            this._queryParameters = new NameValueCollection();

            //Prevent default port
            if (this._uriObject.Uri.IsDefaultPort)
            {
                this._uriObject.Port = -1;
            }

            this._queryParameters.Add(HttpUtility.ParseQueryString(this._uriObject.Query));
        }

        public Fluri()
        {
            this._uriObject = new UriBuilder();
            this._uriObject.Port = -1;
            this._queryParameters = new NameValueCollection();
        }

        public Fluri Add(string query)
        {
            this._queryParameters.Add(HttpUtility.ParseQueryString(query));
            return this;
        }

        public Fluri Over(string query)
        {
            this._queryParameters = new NameValueCollection();
            this._queryParameters.Add(HttpUtility.ParseQueryString(query));
            return this;
        }

        public Fluri Scheme(string scheme)
        {
            this._uriObject.Scheme = scheme;
            return this;
        }

        public Fluri Host(string host)
        {
            this._uriObject.Host = host;
            return this;
        }
        
        public Fluri Port(int port)
        {
            this._uriObject.Port = port;
            return this;
        }

        public Fluri Fragment(string fragment)
        {
            this._uriObject.Fragment = fragment.Replace(" ", "%20");
            return this;
        }

        public Fluri Fragment(int fragment)
        {
            this._uriObject.Fragment = fragment.ToString();
            return this;
        }

        public Fluri Path(string path)
        {
            this._uriObject.Path = path;
            return this;
        }

        public Fluri Query(string query)
        {
            this._uriObject.Query = query;
            this._queryParameters = new NameValueCollection();
            this._queryParameters.Add(HttpUtility.ParseQueryString(this._uriObject.Query));
            return this;
        }

        public string GetUrl()
        {
            this._uriObject.Query = String.Join("&", this.ToQueryString(this._queryParameters));
            return this._uriObject.ToString();
        }
        
        public Uri GetUri()
        {
            this._uriObject.Query = String.Join("&", this.ToQueryString(this._queryParameters));
            return this._uriObject.Uri;
        }

        public Fluri AddQuery(ExpandoObject query)
        {
            foreach (var keyValuePair in query)
            {
                this._queryParameters.Add(keyValuePair.Key,keyValuePair.Value.ToString());   
            }

            return this;
        }
        
        public Fluri RemoveQuery(string key)
        {
            this._queryParameters.Remove(key);
            return this;
        }

        private string ToQueryString(NameValueCollection nvc)
        {
            if (nvc == null) return string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (string key in nvc.Keys)
            {
                if (string.IsNullOrWhiteSpace(key)) continue;

                string[] values = nvc.GetValues(key);
                if (values == null) continue;

                foreach (string value in values)
                {
                    sb.Append(sb.Length == 0 ? "?" : "&");
                    sb.AppendFormat("{0}={1}", key, value.Replace(" ", "+"));
                }
            }

            return sb.ToString();
        }
    }
}