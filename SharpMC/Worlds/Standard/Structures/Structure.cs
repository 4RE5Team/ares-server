﻿using System;
using SharpMC.Blocks;
using SharpMC.Utils;

namespace SharpMC.Worlds.Standard.Structures
{
	public class Structure
	{
		public virtual string Name
		{
			get { return null; }
		}

		public virtual Block[] Blocks
		{
			get { return null; }
		}

		public virtual int Height
		{
			get { return 0; }
		}

		public virtual void Create(ChunkColumn chunk, int x, int y, int z)
		{
			if (chunk.GetBlock(x, y + Height, z) == 0)
			{
				foreach (var b in Blocks)
				{
					chunk.SetBlock(x + (int) b.Coordinates.X, y + (int) b.Coordinates.Y, z + (int) b.Coordinates.Z, b);
				}
			}
		}

		protected void GenerateVanillaLeaves(ChunkColumn chunk, Vector3 location, int radius, Block block)
		{
			var RadiusOffset = radius;
			for (var YOffset = -radius; YOffset <= radius; YOffset = (YOffset + 1))
			{
				var Y = location.Y + YOffset;
				if (Y > 256)
					continue;
				GenerateVanillaCircle(chunk, new Vector3(location.X, Y, location.Z), RadiusOffset, block);
				if (YOffset != -radius && YOffset%2 == 0)
					RadiusOffset--;
			}
		}

		protected void GenerateVanillaCircle(ChunkColumn chunk, Vector3 location, int radius, Block block, double corner = 0)
		{
			for (var I = -radius; I <= radius; I = (I + 1))
			{
				for (var J = -radius; J <= radius; J = (J + 1))
				{
					var Max = (int) Math.Sqrt((I*I) + (J*J));
					if (Max <= radius)
					{
						if (I.Equals(-radius) && J.Equals(-radius) || I.Equals(-radius) && J.Equals(radius) ||
						    I.Equals(radius) && J.Equals(-radius) || I.Equals(radius) && J.Equals(radius))
						{
							if (corner + radius*0.2 < 0.4 || corner + radius*0.2 > 0.7 || corner.Equals(0))
								continue;
						}
						var X = location.X + I;
						var Z = location.Z + J;
						if (chunk.GetBlock((int) X, (int) location.Y, (int) Z).Equals(0))
						{
							chunk.SetBlock((int) X, (int) location.Y, (int) Z, block);
						}
					}
				}
			}
		}

		public static void GenerateColumn(ChunkColumn chunk, Vector3 location, int height, Block block)
		{
			for (var o = 0; o < height; o++)
			{
				var x = (int) location.X;
				var y = (int) location.Y + o;
				var z = (int) location.Z;
				chunk.SetBlock(x, y, z, block);
			}
		}

		protected void GenerateCircle(ChunkColumn chunk, Vector3 location, int radius, Block block)
		{
			for (var I = -radius; I <= radius; I = (I + 1))
			{
				for (var J = -radius; J <= radius; J = (J + 1))
				{
					var Max = (int) Math.Sqrt((I*I) + (J*J));
					if (Max <= radius)
					{
						var X = location.X + I;
						var Z = location.Z + J;

						if (X < 0 || X >= 16 || Z < 0 || Z >= 256)
							continue;

						var x = (int) X;
						var y = (int) location.Y;
						var z = (int) Z;
						if (chunk.GetBlock(x, y, z).Equals(0))
						{
							chunk.SetBlock(x, y, z, block);
						}
					}
				}
			}
		}
	}
}