﻿using Entities.Enums;
using Entities.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Entities
{
	public class Hand
	{
		private readonly List<IBlackJackCard> cards = new List<IBlackJackCard>();
		private List<HandActionTypes> actions;

		public IEnumerable<HandActionTypes> Actions => actions;
		public IEnumerable<IBlackJackCard> Cards => cards;
		public int PointValue { get; private set; } = 0;
		public HandStatusTypes Status { get; private set; } = HandStatusTypes.InProgress;

		public Hand() => SetHandActions();

		public void AddCard(ICard card)
		{
			cards.Add(new BlackJackCard(card, !Cards.Any()));
			SetPointValue();
			SetStatus(HandStatusTypes.InProgress);
			SetHandActions();
		}

		public void AddCardRange(IEnumerable<ICard> cards)
		{
			_ = cards ?? throw new ArgumentNullException(nameof(cards));
			cards.ToList().ForEach(c => AddCard(c));
		}

		public void SetStatus(HandStatusTypes status) => Status = BustHand() ? 
			HandStatusTypes.Bust : status;

		private void SetHandActions()
		{
			actions = new List<HandActionTypes>();
			if (PointValue >= BlackJackConstants.BlackJack)
			{
				actions.Add(HandActionTypes.Pass);
			}
			else
			{
				if (AllowSplit())
				{
					actions.Add(HandActionTypes.Split);
				}
				actions.Add(HandActionTypes.Draw);
				actions.Add(HandActionTypes.Hold);
			}
		}

		private bool AllowSplit() => cards.Count == 2 && 
			cards.All(c => c.Value == cards.First().Value);

		private void SetPointValue()
		{
			PointValue = cards.Sum(c => c.Value);
			var aceCount = cards.Count(c => c.Rank.Equals(CardRank.Ace));
			for (int i = 0; i < aceCount; i++)
			{
				PointValue = BustHand() ? PointValue - BlackJackConstants.DefaultCardValue : PointValue;
			}
		}

		private bool BustHand() => PointValue > BlackJackConstants.BlackJack;
	}
}
