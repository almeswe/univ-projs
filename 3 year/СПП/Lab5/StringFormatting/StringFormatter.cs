using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace StringFormatting
{
    public sealed class StringFormatter : IStringFormatter
    {
        private readonly int _defaultArgumentSize = 32;
        public static readonly IStringFormatter Shared = new StringFormatter();

        public string Format(string formatString, object parameter)
        {
            this.ValidateFormatString(formatString);
            return this.Format(formatString, 
                this.GetFormatterArguments(parameter));
        }

        private string Format(string formatString, 
            Dictionary<string, StringFormatterArgument> arguments)
        {
            int i = -1;
            var builder = new StringBuilder(formatString.Length+
                arguments.Count*this._defaultArgumentSize);
            while (++i < formatString.Length)
            {
                if (formatString[i] != '{' && formatString[i] != '}')
                    builder.Append(formatString[i]);
                else if (formatString[i] == '}')
                {
                    if (i+1 < formatString.Length && formatString[i+1] == '}')
                        builder.Append(formatString[i++]);
                }
                else if (formatString[i] == '{')
                {
                    if (i+1 < formatString.Length)
                    {
                        if (formatString[i+1] == '{')
                            builder.Append(formatString[i++]);
                        else
                        {
                            i++;
                            int argumentLength = 0;
                            var argument = new StringBuilder(this._defaultArgumentSize);
                            while (i < formatString.Length && formatString[i] != '}')
                            {
                                argumentLength++;
                                argument.Append(formatString[i]);
                                i++;
                            }
                            builder.Append(this.GetArgumentValue(
                                argument.ToString(), arguments));
                        }
                    }
                }
            }
            return builder.ToString();
        }

        private void ValidateFormatString(string formatString)
        {
            int counter = 0;
            int lastObraceIndex = 0, lastCbraceIndex = 0;
            for (int i = 0; i < formatString.Length; i++)
            {
                if (formatString[i] == '{')
                {
                    if (lastObraceIndex == i-1)
                        counter -= 1;
                    else
                    {
                        counter += 1;
                        lastObraceIndex = i;
                    }
                }
                if (formatString[i] == '}')
                {
                    if (lastCbraceIndex == i-1)
                        counter += 1;
                    else
                    {
                        counter -= 1;
                        lastCbraceIndex = i;
                        if (i+1 < formatString.Length && formatString[i+1] == '}')
                            continue;
                        if (counter < 0)
                            throw new InvalidStringFormatterException(
                                $"Closing brace is detected before openeing brace (index: {i}).");
                        if (i == lastObraceIndex + 1)
                            throw new InvalidStringFormatterException(
                                $"Argument name is empty (index: {i - 1}).");
                    }
                }
            }
            if (counter != 0)
                throw new InvalidStringFormatterException(
                    "Count of opening/closing braces is not equal.");
        }

        private string GetArgumentValue(string argument, 
            Dictionary<string, StringFormatterArgument> arguments)
        {
            if (argument.Contains('['))
                return this.GetArrayAccessorArgumentValue(argument, arguments);
            if (!arguments.ContainsKey(argument))
                throw new FormatterException($"Specified argument is not listed in passed object.\n" +
                    $"Argument name: {argument}");
            return arguments[argument].ToString();
        }

        private string GetArrayAccessorArgumentValue(string argument,
            Dictionary<string, StringFormatterArgument> arguments)
        {
            var pureArgument = argument.Split('[')[0];
            if (!arguments.ContainsKey(pureArgument))
                throw new FormatterException($"Specified argument is not listed in passed object.\n" +
                    $"Argument name: {pureArgument}");
            var i = 0;
            var indexes = new List<int>();
            try
            {
                while (i < argument.Length)
                {
                    if (argument[i] == '[')
                    {
                        var index = new StringBuilder(this._defaultArgumentSize);
                        while (argument[++i] != ']')
                            index.Append(argument[i]);
                        indexes.Add(Convert.ToInt32(index.ToString()));
                    }
                    i++;
                }
                var arrObj = new StringFormatterArrayAccessorArgument(
                    arguments[pureArgument], indexes.ToArray());
                return arrObj.ToString();
            }
            catch (Exception e)
            {
                throw new FormatterException(
                    $"Unhandled exception occured while processing array indexing\n{e.Message}");
            }
        }

        private Dictionary<string, StringFormatterArgument> GetFormatterArguments(object obj)
        {
            if (obj == null)
                throw new FormatterException("\'parameter\' cannot be null.");
            var objType = obj.GetType();
            var arguments = new Dictionary<string, StringFormatterArgument>();
            foreach (var property in objType.GetProperties().Where(p => p.CanRead))
                arguments[property.Name] = new StringFormatterArgument(obj, property);
            foreach (var field in objType.GetFields().Where(f => f.IsPublic))
                arguments[field.Name] = new StringFormatterArgument(obj, field);
            return arguments;
        }
    }
}