﻿using CardDealer.Tests.Entities.Mocks;
using Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardDealer.Tests.Entities.Tests
{
	class CardGameTests
	{
		private const string playerName = "Sam";
		private const string playerName2 = "Tom";
		private readonly List<Card> deck = new CardDeckProviderMock().Deck;

		[Test]
		public void NewGame_NullDeck_ArgumentNullException()
		{
			List<string> players = new List<string>() {
				playerName,
				playerName2
			};
			Assert.Throws<ArgumentNullException>(() => new CardGame(null, players, ""));
		}

		public void NewGame_NullDeck_PlayerArgumentNullException()
		{
			List<string> players = new List<string>() {
				playerName,
				playerName2,
				null
			};
			Assert.Throws<ArgumentNullException>(() => new CardGame(null, players, ""));
		}

		[Test]
		public void NewGame_TooManyPlayers_ArgumentOutOfRangeException()
		{
			List<string> players = new List<string>() {
				playerName,
				playerName2,
				playerName,
				playerName2,
				playerName2
			};
			Assert.Throws<ArgumentOutOfRangeException>(() => new CardGame(deck, players, ""));
		}

		[Test]
		public void NewGame_NotEnoughPlayers_ArgumentOutOfRangeException()
		{
			List<string> players = new List<string>() { };
			Assert.Throws<ArgumentOutOfRangeException>(() => new CardGame(deck, players, ""));
		}

		[Test]
		public void NewGame_NullPlayerList_ArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new CardGame(deck, null, ""));
		}
	}
}