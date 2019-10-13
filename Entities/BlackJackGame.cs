﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	[Serializable]
	public class BlackJackGame
	{
		private readonly List<BlackJackPlayer> players = new List<BlackJackPlayer>();
		private List<Card> deck = new List<Card>();

		public IEnumerable<BlackJackPlayer> Players => players;
		public IEnumerable<Card> Deck => deck;
		public BlackJackPlayer CurrentPlayer { get; set; }
		public GameStatus Status { get; set; } = GameStatus.Waiting;

		public BlackJackGame() { }

		public void AddPlayer(BlackJackPlayer player)
		{
			_ = player ?? throw new ArgumentNullException(nameof(player));
			if (players.Count > BlackJackConstants.MaxPlayerCount)
			{
				throw new InvalidOperationException($"{player.Name} can not join game, The game Status is {Status}.");
			}
			if (!Players.Any())
			{
				CurrentPlayer = player;
			}

			players.Add(player);
			SetInProgressOnMaxPlayers();
		}

		private void SetInProgressOnMaxPlayers() =>
			Status = players.Count >= BlackJackConstants.MaxPlayerCount ?
			GameStatus.InProgress :
			GameStatus.Waiting;

		public void DealHands(IEnumerable<Card> deck)
		{
			this.deck = deck?.ToList() ?? throw new ArgumentNullException(nameof(deck));
			Status = GameStatus.InProgress;
			int twoCardsPerPlayer = players.Count * 2;
			for (int i = 0; i < twoCardsPerPlayer; i++)
			{
				DrawCard();
				SetCurrentPlayerToNext();
			}
		}

		public void PlayerHolds() => SetCurrentPlayerToNext();

		public void PlayerHits() => DrawCard();

		private void DrawCard()
		{
			if (!Deck.Any()) throw new ArgumentOutOfRangeException(nameof(Deck), "Card Deck out of Cards.");
			var card = Deck.First();
			deck.Remove(card);
			CurrentPlayer.Hand.AddCard(new BlackJackCard(card, IsCardFaceDown()));
		}

		private bool IsCardFaceDown() => !CurrentPlayer.Hand.Cards.Any();

		private void SetCurrentPlayerToNext() => CurrentPlayer = GetIsDealerCurrentPlayer() ?
			Players.First() :
			Players.ElementAt(players.IndexOf(CurrentPlayer) + 1);

		private bool GetIsDealerCurrentPlayer() => CurrentPlayer.Equals(Players.Last());
	}
}
