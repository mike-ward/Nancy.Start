using System;
using App.Models.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Models
{
    [TestClass]
    public class RequireExtensionTests
    {
        [TestMethod]
        public void RequireNotNullTests()
        {
            Action action = () => Require.NotNull(new object(), "");
            action.Should().NotThrow();

            action = () => Require.NotNull(null, null);
            action.Should().Throw<NullReferenceException>().WithMessage(Require.NotSpecified);

            action = () => Require.NotNull(null, "null");
            action.Should().Throw<NullReferenceException>().WithMessage("null");
        }

        [TestMethod]
        public void RequireArgumentNullTests()
        {
            Action action = () => Require.ArgumentNotNull(new object(), "");
            action.Should().NotThrow();

            action = () => Require.ArgumentNotNull(null, null);
            action.Should().Throw<ArgumentNullException>().WithMessage($"*{Require.NotSpecified}*");

            action = () => Require.ArgumentNotNull(null, "bambi");
            action.Should().Throw<ArgumentNullException>().WithMessage("*bambi*");
        }

        [TestMethod]
        public void RequireNotNullEmptyTests()
        {
            Action action = () => Require.ArgumentNotNullEmpty("stuff", "");
            action.Should().NotThrow();

            action = () => Require.ArgumentNotNullEmpty(null, null);
            action.Should().Throw<ArgumentException>().WithMessage("null*").WithMessage($"*{Require.NotSpecified}*");

            action = () => Require.ArgumentNotNullEmpty("", null);
            action.Should().Throw<ArgumentException>().WithMessage("empty*").WithMessage($"*{Require.NotSpecified}*");

            action = () => Require.ArgumentNotNullEmpty(null, "bambi");
            action.Should().Throw<ArgumentException>().WithMessage("null*").WithMessage("*bambi*");

            action = () => Require.ArgumentNotNullEmpty("", "bambi");
            action.Should().Throw<ArgumentException>().WithMessage("empty*").WithMessage("*bambi*");
        }

        [TestMethod]
        public void RequireIsEmptyTests()
        {
            Action action = () => Require.IsEmpty(new string[0], "bambi");
            action.Should().NotThrow();

            action = () => Require.IsEmpty(new [] {""}, "bambi");
            action.Should().Throw<ArgumentException>().WithMessage("not empty*").WithMessage("*bambi*");
        }

        [TestMethod]
        public void RequireIsNotEmptyTests()
        {
            Action action = () => Require.IsNotEmpty(new string[0], "bambi");
            action.Should().Throw<ArgumentException>().WithMessage("empty*").WithMessage("*bambi*");

            action = () => Require.IsNotEmpty(new [] {""}, "bambi");
            action.Should().NotThrow();
        }

        [TestMethod]
        public void RequireArgumentInRangeTests()
        {
            Action action = () => Require.ArgumentInRange(0, 0, 100, "bambi");
            action.Should().NotThrow();

            action = () => Require.ArgumentInRange(0, 1, 100, "bambi");
            action.Should().Throw<ArgumentOutOfRangeException>().WithMessage("1 < 0 < 100*").WithMessage("*bambi*");

            action = () => Require.ArgumentInRange(1, 0, 0, "bambi");
            action.Should().Throw<ArgumentOutOfRangeException>().WithMessage("0 < 1 < 0*").WithMessage("*bambi*");

            action = () => Require.ArgumentInRange(1.1, 0, 100, "bambi");
            action.Should().NotThrow();

            action = () => Require.ArgumentInRange(0.5, 1.0, 100.0, "bambi");
            action.Should().Throw<ArgumentOutOfRangeException>().WithMessage("1 < 0.5 < 100*").WithMessage("*bambi*");

            action = () => Require.ArgumentInRange(1.1, 0.0, 0.0, "bambi");
            action.Should().Throw<ArgumentOutOfRangeException>().WithMessage("0 < 1.1 < 0*").WithMessage("*bambi*");
        }

        [TestMethod]
        public void RequireTrueTests()
        {
            Action action = () => Require.True(() => true);
            action.Should().NotThrow();

            action = () => Require.True(() => false);
            action.Should().Throw<InvalidProgramException>().WithMessage("Require True condition failed");
        }

        [TestMethod]
        public void RequireFalseTests()
        {
            Action action = () => Require.False(() => false);
            action.Should().NotThrow();

            action = () => Require.False(() => true);
            action.Should().Throw<InvalidProgramException>().WithMessage("Require False condition failed");
        }
    }
}