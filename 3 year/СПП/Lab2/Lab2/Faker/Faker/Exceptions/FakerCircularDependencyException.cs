using Faker.Analyzer.Exceptions;
using System.Text;

namespace Faker.Exceptions
{
    public sealed class FakerCircularDependencyException : FakerException
    {
        private GraphNetCircularDependencyException _base;

        public FakerCircularDependencyException(
            GraphNetCircularDependencyException g) : base(g.Message) 
        {
            this._base = g;
        }

        public override string ToString()
        {
            var indent = new StringBuilder();
            var builder = new StringBuilder();
            foreach (var type in this._base.GetTypeTrace())
            {
                builder.Append($"\r\n{indent}{type}");
                indent.Append("  ");
            }
            return builder.ToString();
        }
    }
}
