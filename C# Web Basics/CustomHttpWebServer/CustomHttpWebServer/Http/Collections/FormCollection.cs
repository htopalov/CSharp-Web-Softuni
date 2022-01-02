using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomHttpWebServer.Http.Collections
{
    public class FormCollection : IEnumerable<string>
    {
        private readonly Dictionary<string, string> form;

        public FormCollection()
        {
            this.form = new(StringComparer.InvariantCultureIgnoreCase);
        }

        public string this[string name]
            => this.form[name];

        public void Add(string name, string value)
        {
            this.form[name] = value;
        }

        public bool Contains(string name)
        {
            return this.form.ContainsKey(name);
        }

        public string GetValueOrDefault(string key)
        {
            return this.form.GetValueOrDefault(key);
        }
        public IEnumerator<string> GetEnumerator()
        {
            return this.form.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
