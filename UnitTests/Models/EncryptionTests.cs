using System;
using App.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Models
{
    [TestClass]
    public class EncryptionTests
    {
        [TestMethod]
        public void ComputeHashShouldReturnValidHash()
        {
            var salt = Guid.Parse("9BB96904-0F87-471E-8E10-E112DFF8D75B");
            Encryption.ComputeHash("wonder", salt).Should().Be("59db07466acb38723d5f09147560c2d1");
        }

        [TestMethod]
        public void ComputeHashWithNullOrEmptyTextPukes()
        {
            Action action = () => Encryption.ComputeHash(null, Guid.Empty).Should().Be("59db07466acb38723d5f09147560c2d1");
            action.ShouldThrow<ArgumentException>().WithMessage("*text");
        }

        [TestMethod]
        public void ComputeHashWithInvalidSaltPukes()
        {
            Action action = () => Encryption.ComputeHash("pukemenot", Guid.Empty).Should().Be("59db07466acb38723d5f09147560c2d1");
            action.ShouldThrow<ArgumentException>().WithMessage("*salt*");
        }
    }
}