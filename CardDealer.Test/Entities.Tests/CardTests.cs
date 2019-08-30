﻿using NUnit.Framework;
using System;

namespace Entities.Tests
{
	public class CardTests
  {
    [Test]
    public void NewCard_NullDisplayValue_ArgumentNullException()
    {
      Assert.Throws<ArgumentNullException>(() => new Card(Suit.Clubs, null, "2", 2));
    }

    [Test]
    public void NewCard_NullDescription_ArgumentNullException()
    {
      Assert.Throws<ArgumentNullException>(() => new Card(Suit.Clubs, "2", null, 2));
    }

    [Test]
    public void NewCard_ValueLessThanOne_ArgumentNullException()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new Card(Suit.Clubs, "2", "2", 0));
    }
  }
}
