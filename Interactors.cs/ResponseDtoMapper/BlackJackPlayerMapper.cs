﻿using Entities;
using Interactors.ResponceDtos;
using System;

namespace Interactors.ResponseDtoMapper
{
	class BlackJackPlayerMapper
	{
		private readonly BlackJackPlayer Player;
		public BlackJackPlayerMapper(BlackJackPlayer player)
		{
			Player = player ?? throw new ArgumentNullException(nameof(player));
		}

		public BlackJackPlayerDto Map(bool showAll)
		{
			return new BlackJackPlayerDto
			{
				Name = Player.Name,
				PlayerIdentifier = Player.PlayerIdentifier,
				Hands = new HandDtoMapper(Player.Hands).Map(showAll),
				Status = Player.Status
			};
		}
	}
}
