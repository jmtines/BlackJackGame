﻿using System;
using System.Linq;
using Interactors.Providers;

namespace Interactors.Mocks
{
    class RandomProviderMock : IRandomProvider
    {
        private readonly int[] Seed;
        private readonly int CardCount;
        private int CurrentIndex = 0;

        public RandomProviderMock(int cardCount)
        {
            if (cardCount < 1)
            {
                throw new ArgumentOutOfRangeException("Not Enough Cards to be mixed.", nameof(cardCount));
            }
            CardCount = cardCount;
            Seed = Enumerable.Range(0, cardCount).Reverse().ToArray();
        }

        public RandomProviderMock(int[] seed)
        {
            Seed = seed ?? throw new ArgumentNullException(nameof(seed));
            CardCount = Seed.Count();
        }

        public int Next(int minValue, int maxValue)
        {
            if (CurrentIndex >= CardCount) throw new ArgumentOutOfRangeException("Current index can not be greater than Card count.", nameof(CurrentIndex));
            int currentValue = Seed[CurrentIndex];
            CurrentIndex++;
            return currentValue;
        }
    }
}
