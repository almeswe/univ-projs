using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

using Classes;
using Serialization.XML;
using Serialization.XML.Parser;
using Serialization.XMLSerialization;

namespace Inferface
{
    public partial class InterfaceForm : Form
    {
        private Assembly _assembly;
        private List<Transport> _transports;

        public InterfaceForm()
        {
            this.InitializeComponent();
        }

        private void InterfaceForm_Load(object sender, EventArgs e)
        {
            this._transports = new List<Transport>()
            {
                new Car(),
                new Bus(),
                new Plane(),
                new Helicopter(),
                new Cruise(),
                new SpeedBoat()
            };
            this.ShowTransports();
            this._assembly = typeof(Transport).Assembly;
            this.TransportsListBox.SelectedIndex = 0;
        }

        private void ShowTransports()
        {
            this.TransportsListBox.Items.Clear();
            foreach (var transport in this._transports)
                this.TransportsListBox.Items.Add(transport.GetType().Name);
        }

        private void ShowError(string message) =>
            MessageBox.Show(message, "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);

        private XMLChildObject GetXMLFromTransport(Transport transport) =>
            XMLSerializer.Serialize<Transport>(transport);

        private Transport GetTransportFromXML(Type type, XMLChildObject child) =>
            (Transport)XMLDeserializer.Deserialize(type, child);

        private string AddXMLHeader(string text) =>
            $"<?xml version=\"1.1\" encoding=\"UTF-8\" ?>\r\n{text}";

        private void TransportsSelectedIndexChanged(object sender, EventArgs e)
        {
            var index = this.TransportsListBox.SelectedIndex;
            this.TransportDumpTextBox.Text = this.GetXMLFromTransport(
                this._transports[index]).ToString();
        }

        private XMLDocument ParseXML(string text)
        {
            var tokenizer = new Tokenizer(
                new StringInput(text));
            return new XMLParser().Parse(tokenizer.Tokens);
        }

        private Type GetTypeFromTag(string tag) =>
            this._assembly.GetTypes().First(t => t.Name == tag);

        private void RewriteButtonClick(object sender, EventArgs e)
        {
            try
            {
                var index = this.TransportsListBox.SelectedIndex;
                var document = this.ParseXML(this.AddXMLHeader(this.TransportDumpTextBox.Text));
                var rootChild = document.Root.Childs[0];
                this._transports[index] = this.GetTransportFromXML(
                    this.GetTypeFromTag(rootChild.Tag), rootChild);
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        private void WriteToFileButtonClick(object sender, EventArgs e)
        {
            if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var document = new XMLDocument();
                    foreach (var transport in this._transports)
                        document.Root.Childs.Add(XMLSerializer.Serialize<Transport>(transport));
                    System.IO.File.WriteAllText(this.OpenFileDialog.FileName, document.ToString());
                }
                catch (Exception ex)
                {
                    this.ShowError(ex.Message);
                }
        }
        }

        private void ReadFromFileButtonClick(object sender, EventArgs e)
        {
            if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var document = this.ParseXML(System.IO.File.ReadAllText(
                        this.OpenFileDialog.FileName));
                    this._transports.Clear();
                    foreach (var child in document.Root.Childs)
                        this._transports.Add(this.GetTransportFromXML(
                            this.GetTypeFromTag(child.Tag), child));
                    this.ShowTransports();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex.Message);
                }
            }
        }
    }
}