using System.Text;
using System.Linq;
using System.Collections.Generic;
using Serialization.XML.Parser.Exceptions;

namespace Serialization.XML.Parser
{
    public sealed class XMLParser
    {
        private Token _currentToken;
        private IEnumerable<Token> _tokens;

        public XMLDocument Parse(IEnumerable<Token> tokens)
        {
            this._tokens = tokens;
            return this.ParseXMLDocument();
        }

        private XMLDocument ParseXMLDocument()
        {
            this.GetNextToken();
            // Parsing xml header information
            var document = this.ParseXMLHeader();
            while (!this.Match(TokenKind.TokenEOF))
            {
                this.ExpectWithSkip(TokenKind.TokenLeftAngle);
                document.Root.Childs.Add(this.ParseXMLChild());
            }
            return document;
        }

        private XMLDocument ParseXMLHeader()
        {
            this.ExpectWithSkip(TokenKind.TokenLeftAngle);
            this.ExpectWithSkip(TokenKind.TokenQuestion);
            this.ExpectLexeme("xml"); this.GetNextToken();
            var properties = this.ParseXMLProperties();
            this.ExpectWithSkip(TokenKind.TokenQuestion);
            this.ExpectWithSkip(TokenKind.TokenRightAngle);
            string version = "1.1";
            Encoding encoding = Encoding.UTF8;
            var property = properties.FirstOrDefault(
                p => p.Name == "encoding");
            if (property != null)
                encoding = Encoding.GetEncoding(property.Value);
            property = properties.FirstOrDefault(
                p => p.Name == "version");
            if (property != null)
                version = property.Value;
            return new XMLDocument(version, encoding);
        }

        private XMLValue ParseXMLValue()
        {
            if (this.Match(TokenKind.TokenLeftAngle))
            {
                this.GetNextToken();
                if (this.Match(TokenKind.TokenSlash))
                    return null;
                var childs = new List<XMLChildObject>();
                while (!this.Match(TokenKind.TokenSlash) && 
                    !this.Match(TokenKind.TokenEOF))
                {
                    childs.Add(this.ParseXMLChild());
                    if (this.Match(TokenKind.TokenLeftAngle))
                        this.GetNextToken();
                }
                return new XMLValueChilds(childs.ToArray());
            }
            var value = this._currentToken.Lexeme;
            this.ExpectWithSkip(TokenKind.TokenString);
            this.ExpectWithSkip(TokenKind.TokenLeftAngle);
            return new XMLValueImmediate(value);
        }

        private XMLChildObject ParseXMLChild()
        {
            var tag = this._currentToken.Lexeme;
            this.ExpectWithSkip(TokenKind.TokenIdentifier);
            var properties = this.ParseXMLProperties();
            this.ExpectWithSkip(TokenKind.TokenRightAngle);
            var value = this.ParseXMLValue();
            this.GetNextToken();
            this.ExpectLexeme(tag);
            this.ExpectWithSkip(TokenKind.TokenIdentifier);
            this.ExpectWithSkip(TokenKind.TokenRightAngle);
            return new XMLChildObject(tag, properties, value);
        }

        private XMLProperty ParseXMLProperty()
        {
            var name = this._currentToken.Lexeme;
            this.ExpectWithSkip(TokenKind.TokenIdentifier);
            this.ExpectWithSkip(TokenKind.TokenAssign);
            var value = this._currentToken.Lexeme;
            this.ExpectWithSkip(TokenKind.TokenString);
            return new XMLProperty(name, value);
        }

        private List<XMLProperty> ParseXMLProperties()
        {
            var properties = new List<XMLProperty>();
            while (this.Match(TokenKind.TokenIdentifier) && 
                !this.Match(TokenKind.TokenEOF))
                properties.Add(this.ParseXMLProperty());
            return properties;
        }

        private bool Match(TokenKind kind) =>
            kind == this._currentToken.Kind;

        private bool MatchLexeme(string lexeme) =>
            lexeme == this._currentToken.Lexeme;

        private void Expect(TokenKind kind)
        {
            if (!this.Match(kind))
            {
                throw new XMLParserException($"{kind} expected here, but met " +
                    $"{this._currentToken.Kind}, {this._currentToken.Context}");
            }
        }

        private void ExpectLexeme(string lexeme)
        {
            if (!this.MatchLexeme(lexeme))
            {
                throw new XMLParserException($"{lexeme} expected here, but met " +
                    $"{this._currentToken.Lexeme}, {this._currentToken.Context}");
            }
        }

        private void ExpectWithSkip(TokenKind kind)
        {
            this.Expect(kind);
            this.GetNextToken();
        }

        private Token GetNextToken()
        {
            if (this._currentToken != null)
                if (this._currentToken.Kind == TokenKind.TokenEOF)
                    return this._currentToken;
            var enumerator = this._tokens.GetEnumerator();
            enumerator.MoveNext();
            return this._currentToken = enumerator.Current;
        }
    }
}
