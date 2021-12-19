﻿using System.Collections;
using System.Collections.Generic;

namespace CustomHttpWebServer.Server.Http
{
    public class HttpHeaderCollection : IEnumerable<HttpHeader>
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public int Count => this.headers.Count;

        public void Add(string name, string value)
        {
            var header = new HttpHeader(name, value);
            this.headers.Add(name,header);
        }

        public IEnumerator<HttpHeader> GetEnumerator()
        {
            return this.headers.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
