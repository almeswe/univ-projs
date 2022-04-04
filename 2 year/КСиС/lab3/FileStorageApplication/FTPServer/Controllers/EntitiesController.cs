using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using FTPLayer;
using FTPServer.Models;

namespace FTPServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntitiesController : ControllerBase
    {
        private readonly string _rootPath = Program.RootPath;
        private IFTPLayer _layer = new WindowsFTPLayer();

        [HttpGet("{get}")]
        public Dictionary<string, object> Get(string path)
        {
            try
            {
                // if no path is specified, return contents of RootPath
                if (path == null || path == string.Empty)
                    return this.HttpResponseSimple("entities",
                        this._layer.GetDirectory(this._rootPath));

                // if path is alread has root pattern, no need for Path.Combine 
                if (!System.IO.Path.IsPathRooted(path))
                    path = System.IO.Path.Combine(
                        this._rootPath, path);

                // check needed to ensure that the specified path is located
                // in the scope of specified root path
                if (!path.Contains(this._rootPath))
                    return this.HttpResponseError("Access denied.");

                if (System.IO.File.Exists(path))
                    return this.HttpResponseSimple("contents", 
                        this._layer.GetFile(path));
                if (System.IO.Directory.Exists(path))
                    return this.HttpResponseSimple("entities", 
                        this._layer.GetDirectory(path));
                return this.HttpResponseError("Cannot find any entity for specified path.");
            }
            catch (Exception e)
            {
                return this.HttpResponseError(e.Message);
            }
        }
    
        [HttpPost("{post}")]
        public Dictionary<string, object> Post([FromBody] EntityWithTextModel model)
        {
            try
            {
                if (model.AbsolutePath == null || model.AbsolutePath == string.Empty)
                    return this.HttpResponseError("No path specified.");
                this._layer.AppendToFile(model.Text,
                    model.AbsolutePath);
                return this.HttpResponseSuccess();
            }
            catch (Exception e)
            {
                return this.HttpResponseError(e.Message);
            }
        }

        [HttpPut("{put}")]
        public Dictionary<string, object> Put([FromBody] EntityWithTextModel model)
        {
            try
            {
                if (model.AbsolutePath == null || model.AbsolutePath == string.Empty)
                    return this.HttpResponseError("No path specified.");
                this._layer.PutToFile(model.Text,
                    model.AbsolutePath);
                return this.HttpResponseSuccess();
            }
            catch (Exception e)
            {
                return this.HttpResponseError(e.Message);
            }
        }
        
        [AcceptVerbs("COPY")]
        [Route("{copy}")]
        public Dictionary<string, object> Copy([FromBody] TwoWayPathModel model)
        {
            try
            {
                if (model.SourcePath == null || model.DestinationPath == null)
                    return this.HttpResponseError("No source or destination path specified.");
                this._layer.CopyFile(model.SourcePath,
                    model.DestinationPath);
                return this.HttpResponseSuccess();
            }
            catch (Exception e)
            {
                return this.HttpResponseError(e.Message);
            }
        }

        [AcceptVerbs("MERGE")]
        [Route("{move}")]
        public Dictionary<string, object> Move([FromBody] TwoWayPathModel model)
        {
            try
            {
                if (model.SourcePath == null || model.DestinationPath == null)
                    return this.HttpResponseError("No source or destination path specified.");
                this._layer.MoveFile(model.SourcePath,
                    model.DestinationPath);
                return this.HttpResponseSuccess();
            }
            catch (Exception e)
            {
                return this.HttpResponseError(e.Message);
            }
        }

        [AcceptVerbs("DELETE")]
        [Route("{delete}")]
        public Dictionary<string, object> Delete(string path)
        {
            try
            {
                if (path == null || path == string.Empty)
                    return this.HttpResponseError("Path is empty.");
                this._layer.DeleteFile(path);
                return this.HttpResponseSuccess();
            }
            catch (Exception e)
            {
                return this.HttpResponseError(e.Message);
            }
        }

        private Dictionary<string, object> HttpResponseError(string message) =>
            new Dictionary<string, object>() { { "error", message } };

        private Dictionary<string, object> HttpResponseSuccess() =>
            new Dictionary<string, object>() { { "error", null } };

        private Dictionary<string, object> HttpResponseSimple(string key, object value) =>
            new Dictionary<string, object>() { { "error", null }, { key, value } };
    }
}