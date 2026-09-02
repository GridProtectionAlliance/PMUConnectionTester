using ConnectionTester.Api.Engine.Live;
using ConnectionTester.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PMUConnectionTester.Api.Tests.Engine.Live;

[TestClass]
public class LiveSessionStoreTests
{
    [TestMethod]
    public void Add_ThenTryGet_ReturnsTheSameSession()
    {
        LiveSessionStore store = new();
        LiveSession session = new();

        store.Add(session);

        Assert.IsTrue(store.TryGet(session.SessionId, out LiveSession found));
        Assert.AreSame(session, found);
    }

    [TestMethod]
    public void Remove_KnownSessionId_RemovesAndReturnsIt()
    {
        LiveSessionStore store = new();
        LiveSession session = new();
        store.Add(session);

        Assert.IsTrue(store.Remove(session.SessionId, out LiveSession removed));
        Assert.AreSame(session, removed);
        Assert.IsFalse(store.TryGet(session.SessionId, out _));
    }

    [TestMethod]
    public void Sweep_InProgressSession_IsNeverRemoved()
    {
        LiveSessionStore store = new();
        LiveSession session = new();
        store.Add(session);

        store.Sweep();

        Assert.IsTrue(store.TryGet(session.SessionId, out _));
    }

    [TestMethod]
    public void Sweep_RecentlyFinishedSession_IsNotRemoved()
    {
        LiveSessionStore store = new();
        LiveSession session = new();
        session.Fail(LiveSessionStep.Conectando, "boom");
        store.Add(session);

        store.Sweep();

        Assert.IsTrue(store.TryGet(session.SessionId, out _));
    }

    [TestMethod]
    public void TryGet_UnknownSessionId_ReturnsFalse()
    {
        LiveSessionStore store = new();

        Assert.IsFalse(store.TryGet(System.Guid.NewGuid(), out _));
    }
}