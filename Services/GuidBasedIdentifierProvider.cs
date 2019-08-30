﻿using System;
using Interactors;

namespace CardDealer.Services
{
	public class GuidBasedIdentifierProvider : IIdentifierProvider
	{
		private const int identifierLength = 8;

		public string Generate() =>
			Guid.NewGuid().ToString("N").Substring(0, identifierLength).ToUpper();
	}
}
