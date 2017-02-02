using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLA.Models;

namespace UnitTests.Models
{
    [TestClass]
    public class RequireExtensionTests
    {
        [TestMethod]
        public void RequireNotNullTests()
        {
            Action action = () => Require.NotNull(new object(), "");
            action.ShouldNotThrow();

            action = () => Require.NotNull(null, null);
            action.ShouldThrow<NullReferenceException>().WithMessage(Require.NotSpecified);

            action = () => Require.NotNull(null, "null");
            action.ShouldThrow<NullReferenceException>().WithMessage("null");
        }

        [TestMethod]
        public void RequireArgumentNullTests()
        {
            Action action = () => Require.ArgumentNotNull(new object(), "");
            action.ShouldNotThrow();

            action = () => Require.ArgumentNotNull(null, null);
            action.ShouldThrow<ArgumentNullException>().WithMessage($"*{Require.NotSpecified}*");

            action = () => Require.ArgumentNotNull(null, "bambi");
            action.ShouldThrow<ArgumentNullException>().WithMessage("*bambi*");
        }

        [TestMethod]
        public void RequireNotNullEmptyTests()
        {
            Action action = () => Require.ArgumentNotNullEmpty("stuff", "");
            action.ShouldNotThrow();

            action = () => Require.ArgumentNotNullEmpty(null, null);
            action.ShouldThrow<ArgumentException>().WithMessage("null*").WithMessage($"*{Require.NotSpecified}*");

            action = () => Require.ArgumentNotNullEmpty("", null);
            action.ShouldThrow<ArgumentException>().WithMessage("empty*").WithMessage($"*{Require.NotSpecified}*");

            action = () => Require.ArgumentNotNullEmpty(null, "bambi");
            action.ShouldThrow<ArgumentException>().WithMessage("null*").WithMessage("*bambi*");

            action = () => Require.ArgumentNotNullEmpty("", "bambi");
            action.ShouldThrow<ArgumentException>().WithMessage("empty*").WithMessage("*bambi*");
        }

        [TestMethod]
        public void RequireIsEmptyTests()
        {
            Action action = () => Require.IsEmpty(new string[0], "bambi");
            action.ShouldNotThrow();

            action = () => Require.IsEmpty(new [] {""}, "bambi");
            action.ShouldThrow<ArgumentException>().WithMessage("not empty*").WithMessage("*bambi*");
        }

        [TestMethod]
        public void RequireIsNotEmptyTests()
        {
            Action action = () => Require.IsNotEmpty(new string[0], "bambi");
            action.ShouldThrow<ArgumentException>().WithMessage("empty*").WithMessage("*bambi*");

            action = () => Require.IsNotEmpty(new [] {""}, "bambi");
            action.ShouldNotThrow();
        }

        [TestMethod]
        public void RequireArgumentInRangeTests()
        {
            Action action = () => Require.ArgumentInRange(0, 0, 100, "bambi");
            action.ShouldNotThrow();

            action = () => Require.ArgumentInRange(0, 1, 100, "bambi");
            action.ShouldThrow<ArgumentOutOfRangeException>().WithMessage("1 < 0 < 100*").WithMessage("*bambi*");

            action = () => Require.ArgumentInRange(1, 0, 0, "bambi");
            action.ShouldThrow<ArgumentOutOfRangeException>().WithMessage("0 < 1 < 0*").WithMessage("*bambi*");

            action = () => Require.ArgumentInRange(1.1, 0, 100, "bambi");
            action.ShouldNotThrow();

            action = () => Require.ArgumentInRange(0.5, 1.0, 100.0, "bambi");
            action.ShouldThrow<ArgumentOutOfRangeException>().WithMessage("1 < 0.5 < 100*").WithMessage("*bambi*");

            action = () => Require.ArgumentInRange(1.1, 0.0, 0.0, "bambi");
            action.ShouldThrow<ArgumentOutOfRangeException>().WithMessage("0 < 1.1 < 0*").WithMessage("*bambi*");
        }

        [TestMethod]
        public void RequireTrueTests()
        {
            Action action = () => Require.True(() => true);
            action.ShouldNotThrow();

            action = () => Require.True(() => false);
            action.ShouldThrow<InvalidProgramException>().WithMessage("Require True condition failed");
        }

        [TestMethod]
        public void RequireFalseTests()
        {
            Action action = () => Require.False(() => false);
            action.ShouldNotThrow();

            action = () => Require.False(() => true);
            action.ShouldThrow<InvalidProgramException>().WithMessage("Require False condition failed");
        }
    }
}