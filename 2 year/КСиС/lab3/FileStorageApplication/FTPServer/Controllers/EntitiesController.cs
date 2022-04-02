using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using FTPLayer;
using FTPLayer.Entity;

namespace FTPServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntitiesController : ControllerBase
    {
        private IFTPLayer _layer = new DefaultFTPLayer();

        [HttpGet("{get}")]
        public Dictionary<string, object> GetDirectory(string path)
        {
            try
            {
                // if no path is specified, return contents of RootPath
                if (path == null || path == string.Empty)
                    return this.HttpResponseSimple("entities",
                        this._layer.GetDirectory(FileSystemEntity.RootPath));

                // if path is alread has root pattern, no need for Path.Combine 
                if (!System.IO.Path.IsPathRooted(path))
                    path = System.IO.Path.Combine(
                        FileSystemEntity.RootPath, path);

                // check needed to ensure that the specified path is located
                // in the scope of specified root path
                if (!path.Contains(FileSystemEntity.RootPath))
                    return this.HttpResponseError("Access denied.");

                if (System.IO.File.Exists(path))
                    return this.HttpResponseSimple("contents", 
                        this._layer.GetFile(path));

                return this.HttpResponseSimple("entities", 
                    this._layer.GetDirectory(path));
            }
            catch (Exception e)
            {
                return this.HttpResponseError(e.Message);
            }
        }
    
        private Dictionary<string, object> HttpResponseError(string message) =>
            new Dictionary<string, object>() { { "error", message } };

        private Dictionary<string, object> HttpResponseSimple(string key, object value) =>
            new Dictionary<string, object>() { { "error", null }, { key, value } };
    }
}
