using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace ТИ_1
{
    public interface ICryptography
    {
        string Encrypt(string text);
        string Decrypt(string cipthertext);
    }

    public sealed class ColumnMethod : ICryptography
    {
        private string _key;
        private List<int> _vector;

        public ColumnMethod(string key)
        {
            this._key = key;
            if (!this.ValidateKey())
                throw new ArgumentException("Incorrect key passed.");
            this._vector = this.MakeVector();
        }

        public string Encrypt(string text)
        {
            var ciphertext = string.Empty;
            var table = Enumerable.Repeat("",
                this._key.Length).ToList();
            text = this.ProcessTextInput(text);
            var counter = 0;
            foreach (char item in text)
                table[counter++ % this._key.Length] += item;

            for (int i = 1; i < this._key.Length + 1; i++)
                ciphertext += table[this._vector.IndexOf(i)];
            return ciphertext.ToUpper();
        }

        public string Decrypt(string text)
        {
            int textPtr = 0;
            var plaintext = string.Empty;
            text = this.ProcessTextInput(text);
            var table = Enumerable.Repeat("",
                this._key.Length).ToList();

            int level = (text.Length / this._key.Length) + 1;
            int columns = text.Length % this._key.Length;
            for (int i = 1; i <= this._key.Length; i++)
            {
                int munch = level - (this._vector.IndexOf(i) >= columns ? 1 : 0);
                for (int j = textPtr; j < textPtr + munch; j++)
                    table[this._vector.IndexOf(i)] += text[j];
                textPtr += munch;
            }

            for (int i = 0; i < level; i++)
                for (int j = 0; j < this._key.Length; j++)
                    if (table[j].Length > i)
                        plaintext += table[j][i];
            return plaintext.ToUpper();
        }

        private List<int> MakeVector()
        {
            int vectorPriority = 0;
            var vector = Enumerable.Repeat(-1,
                this._key.Length).ToList();
            foreach (char item in string.Concat(this._key.OrderBy(c => c)))
                for (int i = 0; i < this._key.Length; i++)
                    if (item == this._key[i] && vector[i] == -1)
                        vector[i] = ++vectorPriority;
            return vector;
        }

        private string ProcessTextInput(string text)
        {
            var processed = new StringBuilder(string.Empty);
            foreach (char c in text)
                if (this.ValidateChar(c))
                    processed.Append(c);
            return processed.ToString();
        }

        private bool ValidateChar(char c) =>
            (((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')) &&
                char.IsLetter(c));

        private bool ValidateKey() =>
            this._key != string.Empty && 
                this._key.All(this.ValidateChar);
    }

    public sealed class DecimationMethod : ICryptography
    {
        private string _key;
        private string _sample => "abcdefghijklmnopqrstuvwxyz";

        public DecimationMethod(string key)
        {
            this._key = key;
            if (!this.ValidateKey())
                throw new ArgumentException("Incorrect key passed.");
        }

        public string Encrypt(string text)
        {
            var ciphertext = new StringBuilder(string.Empty);
            text = this.ProcessTextInput(text);
            foreach (char item in text)
                if (this._sample.IndexOf(item) >= 0)
                    ciphertext.Append(this._sample[this._sample.IndexOf(item) * int.Parse(this._key) % 26]);
            return ciphertext.ToString();
        }

        public string Decrypt(string cipthertext)
        {
            var text = new StringBuilder(string.Empty);
            cipthertext = this.ProcessTextInput(cipthertext);
            foreach (char item in cipthertext)
                if (this._sample.IndexOf(item) >= 0)
                    text.Append(this._sample[(this._sample.IndexOf(item) + 26) / int.Parse(this._key) % 26]);
            return text.ToString();
        }

        private string ProcessTextInput(string text)
        {
            var processed = new StringBuilder(string.Empty);
            foreach (char c in text)
                if (this.ValidateTextChar(c))
                    processed.Append(c);
            return processed.ToString().ToLower();
        }

        private bool ValidateTextChar(char c) =>
            ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'));

        private bool ValidateKey() =>
            int.TryParse(this._key, out _) && 
                this.IsCoprime(int.Parse(this._key), 26);

        private bool IsCoprime(int a, int b) =>
            a == b ? a == 1 : a > b
                        ? IsCoprime(a - b, b)
                        : IsCoprime(b - a, a);
    }

    public sealed class VigenereMethod : ICryptography
    {
        private string _key;
        private List<string> _table;
        private string _sample => "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        public VigenereMethod(string key)
        {
            this._key = key;
            this._table = this.GenerateTable();
            if (!ValidateKey())
                throw new ArgumentException("Incorrect key passed.");
        }

        public string Encrypt(string text)
        {
            text = this.ProcessTextInput(text);
            var ciphertext = new StringBuilder("");
            var shiftedKey = this.GenerateSpreadedKey(text);

            int count = 0;
            for (int i = 0; i < text.Length; i++)
                ciphertext.Append(this._sample.IndexOf(text[i]) >= 0 ? 
                    this._table[this._sample.IndexOf(text[i])][this._sample.IndexOf(shiftedKey[count++])] : text[i]);
            return ciphertext.ToString().ToUpper();
        }

        public string Decrypt(string ciphertext)
        {
            var text = new StringBuilder("");
            ciphertext = this.ProcessTextInput(ciphertext);
            var shiftedKey = this.GenerateSpreadedKey(ciphertext);

            int count = 0;
            for (int i = 0; i < shiftedKey.Length; i++)
                text.Append(this._sample.IndexOf(ciphertext[i]) >= 0 ?
                    this._sample[this._table[this._sample.IndexOf(shiftedKey[count++])].IndexOf(ciphertext[i])] : ciphertext[i]);
            return text.ToString().ToUpper();
        }

        private string GenerateSpreadedKey(string text)
        {
            if (text.Length <= this._key.Length)
                return this._key;
            var progressive = -1;
            var spreaded = new StringBuilder(string.Empty);

            for (int i = 0; i < text.Length; i++)
            {
                if (i % this._key.Length == 0)
                    progressive++;
                spreaded.Append(this._sample[(this._sample.IndexOf(
                    this._key[i % this._key.Length]) + progressive) % 33]);
            }
            return spreaded.ToString();
        }

        private List<string> GenerateTable()
        {
            var table = new List<string>();

            for (int i = 0; i < 33; i++)
            {
                var shifted = new StringBuilder(string.Empty);
                for (int j = 0; j < 33; j++)
                    shifted.Append(this._sample[(j+i) % 33]);
                table.Add(shifted.ToString());
            }
            return table;
        }

        private string ProcessTextInput(string text)
        {
            var processed = new StringBuilder(string.Empty);
            foreach (char c in text)
                if (this.ValidateTextChar(c))
                    processed.Append(c);
            return processed.ToString().ToLower();
        }

        private bool ValidateKeyChar(char c) =>
            (c >= 'а' && c <= 'я') || (c >= 'А' && c <= 'Я');

        private bool ValidateTextChar(char c) =>
            !((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'));

        private bool ValidateKey() =>
            this._key.All(this.ValidateKeyChar);
    }
}