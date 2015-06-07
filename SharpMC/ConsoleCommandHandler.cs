﻿using System;

namespace SharpMC
{
	internal class ConsoleCommandHandler
	{
		public void WaitForCommand()
		{
			while (true)
			{
				string input = Console.ReadLine();
				if (!String.IsNullOrEmpty(input))
				{
					Globals.PluginManager.HandleCommand(input, Globals.ConsolePlayer);
				}
			}
		}
	}
}
