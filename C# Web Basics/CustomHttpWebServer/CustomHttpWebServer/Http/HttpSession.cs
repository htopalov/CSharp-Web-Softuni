using System.Collections.Generic;
using CustomHttpWebServer.Common;

namespace CustomHttpWebServer.Http
{
    public class HttpSession
    {
        public const string SessionCookieName = "MyWebServerSessionId";
        private Dictionary<string, string> data;

        public HttpSession(string id)
        {
            Guard.AgainstNull(id, nameof(id));
            this.Id = id;
            this.data = new Dictionary<string, string>();
        }

        public string Id { get; init; }

        public bool IsNew { get; set; }

        public string this[string key]
        {
            get => this.data[key];
            set => this.data[key] = value;
        }

        public bool ContainsKey(string key)
        {
            return this.data.ContainsKey(key);
        }

        public void Remove(string key)
        {
            if (this.data.ContainsKey(key))
            {
                this.data.Remove(key);
            }
        }
    }
}
