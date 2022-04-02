using System;
using System.Net;

using RestSharp;
using Newtonsoft.Json.Linq;

namespace FTPClient.ServerApi
{
    public sealed class FTPServerApiResponse
    {
        public bool IsErrored { get; private set; }
        public string ErrorMessage { get; private set; }

        public Type ResponseType { get; set; }
        public object ResponseData { get; set; }
    
        public string Contents { get; private set; }
        public JObject JsonObject { get; private set; }

        public FTPServerApiResponse(RestResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                this.IsErrored = true;
                this.ErrorMessage = $"{response.StatusCode}";
            }
            else if ((this.JsonObject = JObject.Parse(
                    this.Contents = response.Content)) != null)
                this.CheckForError();
        }

        private void CheckForError() =>
            this.IsErrored = ((this.ErrorMessage = 
                this.JsonObject.Value<string>("error")) != null);
    }
}
