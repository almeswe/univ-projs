using Parser.Interface;

namespace Parser.Core
{
	public delegate void ParseObjectRecord(ReadOnlySpan<char> buffer);

	public sealed class ObjectParser : IObjectParser
	{
		private ObjectFile _objectFile = null;

		public IObjectFile Parse(string path)
		{
			this._objectFile = new ObjectFile(path);
			using (var fs = new StreamReader(path))
			{
				while (!fs.EndOfStream)
				{
					var line = fs.ReadLine()!;
					var parser = GetRecordParser(line);
					if (parser != null)
						parser(line.AsSpan());
				}
			}
			return this._objectFile;
		}

		private ParseObjectRecord GetRecordParser(string line)
		{
			if (line.StartsWith("f"))
				return ParsePolygon;
			if (line.StartsWith("vn"))
				return ParseVector;
			if (line.StartsWith("vt"))
				return ParseTexture;
			if (line.StartsWith("v"))
				return ParseVertex;
			return null!;
		}

		private ReadOnlySpan<char> ParseWithSeparator(ReadOnlySpan<char> buffer, char separator)
		{
			var length = 0;
			for (var i = 0; i < buffer.Length && buffer[i] != separator; i++)
				length++;
			return buffer.Slice(0, length);
		}

		private void ParseVertex(ReadOnlySpan<char> buffer)
		{
			var spanArray = default(Span<float>);
			var spanFloat = default(ReadOnlySpan<char>);
			var spanSlice = MemoryExtensions.TrimStart(buffer.Slice(1));
			unsafe
			{
				#pragma warning disable CS9081
				spanArray = stackalloc float[4];
				spanArray[3] = 1.0f;
			}
			for (var i = 0; i < 4 && spanSlice.Length > 0; i++)
			{
				spanFloat = ParseWithSeparator(spanSlice, ' ');
				spanArray[i] = float.Parse(spanFloat);
				spanSlice = MemoryExtensions.TrimStart(spanSlice.Slice(spanFloat.Length));
			}
			this._objectFile?.Vertices.Add(new Vector4(spanArray));
		}

		private void ParseVector(ReadOnlySpan<char> buffer)
		{
			var spanArray = default(Span<float>);
			var spanFloat = default(ReadOnlySpan<char>);
			var spanSlice = MemoryExtensions.TrimStart(buffer.Slice(2));
			unsafe
			{
				#pragma warning disable CS9081
				spanArray = stackalloc float[3];
			}
			for (var i = 0; i < 3 && spanSlice.Length > 0; i++)
			{
				spanFloat = ParseWithSeparator(spanSlice, ' ');
				spanArray[i] = float.Parse(spanFloat);
				spanSlice = MemoryExtensions.TrimStart(spanSlice.Slice(spanFloat.Length));
			}
			this._objectFile?.Normals.Add(new Vector3(spanArray));
		}

		private void ParseTexture(ReadOnlySpan<char> buffer)
		{
			var spanArray = default(Span<float>);
			var spanFloat = default(ReadOnlySpan<char>);
			var spanSlice = MemoryExtensions.TrimStart(buffer.Slice(2));
			unsafe
			{
				#pragma warning disable CS9081
				spanArray = stackalloc float[3];
				spanArray[1] = 0.0f;
				spanArray[2] = 0.0f;
			}
			for (var i = 0; i < 3 && spanSlice.Length > 0; i++)
			{
				spanFloat = ParseWithSeparator(spanSlice, ' ');
				spanArray[i] = float.Parse(spanFloat);
				spanSlice = MemoryExtensions.TrimStart(spanSlice.Slice(spanFloat.Length));
			}
			this._objectFile?.Textures.Add(new Vector3(spanArray));
		}

		private void ParsePolygon(ReadOnlySpan<char> buffer)
		{
			var spanTuple = default(Span<int>);
			var spanArray = default(Span<ValueTuple<int, int, int>>);
			var spanSlice = MemoryExtensions.TrimStart(buffer.Slice(1));
			unsafe
			{
				#pragma warning disable CS9081
				spanTuple = stackalloc int[3];
				spanArray = stackalloc ValueTuple<int, int, int>[64];
			}
			var count = 0;
			for (var i = 0; i < spanArray.Length; i++, count++)
			{
				var spanWord = ParseWithSeparator(spanSlice, ' ');
				var spanWordLength = spanWord.Length;
				if (spanWordLength == 0)
					break;
				for (var k = 0; k < 3 && spanWord.Length > 0; k++)
				{
					var spanPart = ParseWithSeparator(spanWord, '/');
					var spanPartLength = spanPart.Length;
					if (spanPart.Length != 0)
						spanTuple[k] = int.Parse(spanPart);
					var sliceLength = int.Min(spanPart.Length + 1, spanWord.Length);
					spanWord = spanWord.Slice(sliceLength);
				}
				spanSlice = MemoryExtensions.TrimStart(spanSlice.Slice(spanWordLength));
				spanArray[i] = ValueTuple.Create(spanTuple[0], spanTuple[1], spanTuple[2]);
				spanTuple.Clear();
			}
			spanArray = spanArray.Slice(0, count);
			this._objectFile?.Polygons.Add(new Polygon(spanArray));
		}
	}
}