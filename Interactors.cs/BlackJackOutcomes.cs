﻿using Entities;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Interactors
{
    public class BlackJackOutcomes
    {
		private readonly BlackJackGame Game;

		public BlackJackOutcomes(BlackJackGame game)
		{
			Game = game ?? throw new ArgumentNullException(nameof(game));
		}
        public void UpdateStatus()
        {
            if (HasBlackjack(Game.Players.Last()))
            {
                Game.Players.ForEach(p => p.Status = HasBlackjack(p) ? PlayerStatus.Push : PlayerStatus.PlayerLoses);
				Game.Status = GameStatus.Complete;
            }
            else if (BustHand(Game.Players.Last()))
            {
                Game.Players.ForEach(p => p.Status = BustHand(p) ? PlayerStatus.PlayerLoses : PlayerStatus.PlayerWins);
				Game.Status = GameStatus.Complete;
			}
            else
            {
                foreach (var player in Game.Players.Where(p => !p.Equals(Game.Players.Last())))
                {
                    if (BustHand(player))
                    {
                        player.Status = PlayerStatus.PlayerLoses;
                    }
                    else if (PlayerPointsLessThanDealer(player))
                    {
                        player.Status = PlayerStatus.PlayerLoses;
                    }
                    else if (PlayerPointsEqualsDealer(player))
                    {
                        player.Status = PlayerStatus.Push;
                    }
                    else
                    {
                        player.Status = PlayerStatus.PlayerWins;
                    }
                }
            }
        }

        private bool HasBlackjack(Player player) => player.PointTotal == BlackJackGameConstants.BlackJack;
        private bool BustHand(Player player) => player.PointTotal > BlackJackGameConstants.BlackJack;
        private bool PlayerPointsLessThanDealer(Player player) => player.PointTotal < Game.Players.Last().PointTotal;
        private bool PlayerPointsEqualsDealer(Player player) => player.PointTotal == Game.Players.Last().PointTotal;
    }
}
