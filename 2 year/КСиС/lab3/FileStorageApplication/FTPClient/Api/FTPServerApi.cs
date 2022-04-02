using FTPLayer.Entity;
using Newtonsoft.Json.Linq;

using RestSharp;
using System.Collections.Generic;

namespace FTPClient.ServerApi
{
    public static class FTPServerApi
    {
        public static string ApiBaseUrl 
        {
            get
            {
                return _apiBaseUrl;
            }
            set
            {
                _client = new RestClient(
                    _apiBaseUrl = value);
            }
        }

        private static string _apiBaseUrl;
        private static RestClient _client;

        public static FTPServerApiResponse GetFile(string path)
        {
            var request = new RestRequest($"/get?path={path}");
            var response = _client.GetAsync(request).Result;

            var apiResponse = new FTPServerApiResponse(response) {
                ResponseType = typeof(string)
            };
            if (!apiResponse.IsErrored)
                apiResponse.ResponseData = apiResponse.JsonObject
                    .Value<string>("contents");
            return apiResponse;
        }

        public static FTPServerApiResponse GetDirectory(string path)
        {
            var request = new RestRequest(path == null ? 
                $"/get" : $"/get?path={path}");
            var response = _client.GetAsync(request).Result;

            var apiResponse = new FTPServerApiResponse(response) {
                ResponseType = typeof(IEnumerable<FileSystemEntity>)
            };
            if (!apiResponse.IsErrored)
            {
                //todo: make this more convenient
                var tokens = apiResponse.JsonObject
                    .Value<JArray>("entities");
                var entities = new List<FileSystemEntity>();
                foreach (var token in tokens)
                {
                    var absolutePath = token.Value<string>("absolutePath");
                    if (token.Value<int>("type") == 
                            (int)FileSystemEntityType.File)
                        entities.Add(new File(absolutePath));
                    else
                        entities.Add(new Directory(absolutePath));
                }
                apiResponse.ResponseData = entities;
            }
            return apiResponse;
        }

        public static FTPServerApiResponse GetRootDirectory() =>
            GetDirectory(null);
    }
}
