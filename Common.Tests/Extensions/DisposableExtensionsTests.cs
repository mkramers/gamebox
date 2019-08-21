using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using NUnit.Framework;

namespace Common.Tests.Extensions
{
    [TestFixture]
    public class DisposableExtensionsTests
    {
        private class TestDisposable : IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        [Test]
        public void DisposesAllAndClearsCorrectly()
        {
            TestDisposable[] allDisposables = { new TestDisposable(), new TestDisposable() };

            List<TestDisposable> disposables = new List<TestDisposable>(allDisposables);
            disposables.DisposeAllAndClear();

            Assert.Multiple(() =>
            {
                Assert.That(disposables.Count == 0);
                Assert.That(allDisposables.All(_disposable => _disposable.IsDisposed), Is.True, "Not all items were disposed");
            });
        }
    }
}