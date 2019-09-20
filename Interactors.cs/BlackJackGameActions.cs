﻿using Entities;
using System;
using System.Linq;
using Interactors.Providers;

namespace Interactors
{
    public static class BlackJackGameActions
    {
        public static void AddNewPlayer(CardGame game, Player player)
        {
            var Game = game ?? throw new ArgumentNullException(nameof(game));
            var Player = player ?? throw new ArgumentNullException(nameof(game));

            Game.Players.Add(Player); 
        }

        public static void DealHands(CardGame game)
        {
            int twoCardsPerPlayer = game.Players.Count * 2;
            for (int i = 0; i < twoCardsPerPlayer; i++)
            {
                PlayerDrawsCard(game);
                NextPlayer(game);
            }
        }

        public static void PlayerHolds(CardGame game)
        {
            NextPlayer(game);
            if (DealerCurrentPlayer(game))
            {
                DealersFinalTurn(game);
            }
        }

        public static void PlayerDrawsCard(CardGame game)
        {
            Card card = game.Deck.FirstOrDefault();
            game.Deck.Remove(card);
            game.CurrentPlayer.AddCardToHand(new BlackJackCard(card, IsFaceDown(game)));
            game.CurrentPlayer.PointTotal = HandValueProvider.GetValue(game.CurrentPlayer.Hand);
        }

        private static bool IsFaceDown(CardGame game) => game.CurrentPlayer.Hand.Any() ? false : true;

        private static void DealersFinalTurn(CardGame game)
        {
            while (game.Players.Last().PointTotal < BlackJackGameConstants.DealerHoldValue)
            {
                PlayerDrawsCard(game);
            }
        }

        private static void NextPlayer(CardGame game) => game.CurrentPlayer = DealerCurrentPlayer(game) ?
            game.Players.First() : game.Players.ElementAt(game.Players.IndexOf(game.CurrentPlayer) + 1);

        private static bool DealerCurrentPlayer(CardGame game) => game.CurrentPlayer.Equals(game.Players.Last());
    }
}
