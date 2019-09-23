﻿using System.Threading.Tasks;
using Entities;

namespace Interactors
{
    public interface IGameRepository
    {
        Task CreateAsync(string identifier, BlackJackGame game);
        Task<BlackJackGame> ReadAsync(string identifier);
        Task UpdateAsync(string identifier, BlackJackGame game);
        Task<string> AddPlayerToGameAsync(Player player);
    }
}
