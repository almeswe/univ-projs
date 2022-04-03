using FTPLayer.Entity;
using Newtonsoft.Json.Linq;

using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public static async Task<FTPServerApiResponse> GetFile(string path)
        {
            var request = new RestRequest($"/get?path={path}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            var apiResponse = new FTPServerApiResponse(response);

            if (!apiResponse.IsErrored)
                apiResponse.ResponseData = apiResponse.JsonObject
                    .Value<string>("contents");
            return apiResponse;
        }

        public static async Task<FTPServerApiResponse> GetDirectory(string path)
        {
            var request = new RestRequest(path == null ? 
                $"/get" : $"/get?path={path}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            var apiResponse = new FTPServerApiResponse(response);

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

        public static async Task<FTPServerApiResponse> GetRootDirectory() =>
            await GetDirectory(null);

        public static async Task<FTPServerApiResponse> AppendTextToFile(string path, string text)
        {
            var request = new RestRequest($"/post", Method.Post);
            request.AddBody(new Dictionary<string, object>() {
                { "absolutePath", path },
                { "text", text }
            });
            var response = await _client.ExecuteAsync(request);
            return new FTPServerApiResponse(response);
        }

        public static async Task<FTPServerApiResponse> PutTextToFile(string path, string text)
        {
            var request = new RestRequest($"/put", Method.Put);
            request.AddBody(new Dictionary<string, object>() {
                { "absolutePath", path },
                { "text", text }
            });
            var response = await _client.ExecuteAsync(request);
            return new FTPServerApiResponse(response);
        }
    }
}
