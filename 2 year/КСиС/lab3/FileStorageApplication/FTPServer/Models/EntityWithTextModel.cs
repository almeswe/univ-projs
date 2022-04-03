namespace FTPServer.Models
{
    public sealed class EntityWithTextModel
    {
        public string AbsolutePath { get; set; }
        public string Text { get; set; }

        public EntityWithTextModel() =>
            this.AbsolutePath = this.Text 
                = string.Empty;

        public EntityWithTextModel(string absolutePath, string textToAppend)
        {
            this.AbsolutePath = absolutePath;
            this.Text = textToAppend;
        }
    }
}
