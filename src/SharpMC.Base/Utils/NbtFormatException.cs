using System;

namespace SharpMC.Core.Utils
{
	/// <summary>
	///     Exception thrown when a format violation is detected while
	///     parsing or serializing an NBT file.
	/// </summary>
	[Serializable]
	public class NbtFormatException : Exception
	{
		internal NbtFormatException(string message)
			: base(message)
		{
		}
	}
}