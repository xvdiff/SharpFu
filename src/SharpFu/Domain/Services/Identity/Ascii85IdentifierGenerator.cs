#region

using System;
using System.Text;

#endregion

namespace SharpFu.Domain.Services.Identity
{

	/// <summary>
	///		Generates an unique ascii85 identifier
	/// </summary>
	public class Ascii85IdGenerator : IIdentifierGenerator<string>
	{
		private const string PrefixMark = "<~";
		private const string SuffixMark = "~>";

		private readonly byte[] _decodedBlock = new byte[4];
		private readonly byte[] _encodedBlock = new byte[5];
		private readonly char _zMark = Convert.ToChar("z");

		private UInt32 _tuple;

		public string Generate()
		{
			var guidArray = new GuidGenerator()
				.Generate().ToByteArray();
			return Encode(guidArray);
		}

		private static void AppendString(StringBuilder sb, string s)
		{
			sb.Append(s);
		}

		private static void AppendChar(StringBuilder sb, char c)
		{
			sb.Append(c);
		}

		private void EncodeBlock(StringBuilder sb)
		{
			EncodeBlock(_encodedBlock.Length, sb);
		}

		/// <summary>
		///		Encodes a byte array to Ascii85
		/// </summary>
		public string Encode(byte[] ba)
		{
			var sb = new StringBuilder(Convert.ToInt32(ba.Length*(_encodedBlock.Length/_decodedBlock.Length)));

			AppendString(sb, PrefixMark);

			var count = 0;
			_tuple = 0;
			foreach (var b in ba)
			{
				if (count >= _decodedBlock.Length - 1)
				{
					_tuple = _tuple | b;
					if (_tuple == 0)
					{
						AppendChar(sb, _zMark);
					}
					else
					{
						EncodeBlock(sb);
					}
					_tuple = 0;
					count = 0;
				}
				else
				{
					_tuple = _tuple | (Convert.ToUInt32(b) << 24 - (count*8));
					count += 1;
				}
			}

			// if we have some bytes left over at the end..
			if (count > 0)
			{
				EncodeBlock(count + 1, sb);
			}

			AppendString(sb, SuffixMark);

			return sb.ToString();
		}

		private void EncodeBlock(int count, StringBuilder sb)
		{
			for (var i = _encodedBlock.Length - 1; i >= 0; i += -1)
			{
				_encodedBlock[i] = Convert.ToByte(((_tuple%85) + 0x21));
				_tuple = Convert.ToUInt32(_tuple/85);
			}

			for (var i = 0; i <= count - 1; i++)
			{
				AppendChar(sb, (char) _encodedBlock[i]);
			}
		}
	}
}