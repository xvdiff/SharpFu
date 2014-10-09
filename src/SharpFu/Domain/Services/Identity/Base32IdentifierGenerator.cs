﻿#region

using System;
using System.Security.Cryptography;

#endregion

namespace SharpFu.Domain.Services.Identity
{

	/// <summary>
	///		Generates an unique base32 identifier
	/// </summary>
	public class Base32UniqueIdGenerator : IIdentifierGenerator<string>, IDisposable
	{
		private static readonly char[] CharMap =
		{
			// 0, 1, O, and I omitted intentionally giving 32 (2^5) symbols
			'2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
			'Z'
		};

		private readonly RNGCryptoServiceProvider _cryptoProvider
			= new RNGCryptoServiceProvider();

		private readonly int _digits;

		/// <summary>
		///		Creates a new instance of a <see cref="Base32UniqueIdGenerator"/>
		/// </summary>

		public Base32UniqueIdGenerator(int digits = 16)
		{
			_digits = digits;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public string Generate()
		{
			var guid = new GuidGenerator().Generate();
			return GetBase32UniqueId(guid.ToByteArray(), _digits);
		}

		~Base32UniqueIdGenerator()
		{
			Dispose(false);
		}

		/// <summary>
		///		Disposes the underlying cryptoprovider
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
				return;

			_cryptoProvider.Dispose();
		}

		private string GetBase32UniqueId(byte[] basis, int numDigits)
		{
			const int byteCount = 16;
			var randBytes = new byte[byteCount - basis.Length];
			GetNext(randBytes);
			var bytes = new byte[byteCount];
			Array.Copy(basis, 0, bytes, byteCount - basis.Length, basis.Length);
			Array.Copy(randBytes, 0, bytes, 0, randBytes.Length);

			var lo = (((ulong) BitConverter.ToUInt32(bytes, 8)) << 32) | BitConverter.ToUInt32(bytes, 12);
			// BitConverter.ToUInt64(bytes, 8);
			var hi = (((ulong) BitConverter.ToUInt32(bytes, 0)) << 32) | BitConverter.ToUInt32(bytes, 4);
			// BitConverter.ToUInt64(bytes, 0);
			const ulong mask = 0x1F;

			var chars = new char[26];
			var charIdx = 25;

			var work = lo;
			for (var i = 0; i < 26; i++)
			{
				switch (i)
				{
					case 12:
						work = ((hi & 0x01) << 4) & lo;
						break;
					case 13:
						work = hi >> 1;
						break;
				}
				var digit = (byte) (work & mask);
				chars[charIdx] = CharMap[digit];
				charIdx--;
				work = work >> 5;
			}

			var ret = new string(chars, 26 - numDigits, numDigits);
			return ret;
		}

		private void GetNext(byte[] bytes)
		{
			_cryptoProvider.GetBytes(bytes);
		}
	}
}