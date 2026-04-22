using NUnit.Framework;
using UnityEngine;

public class SesstionTests
{
    [Test]
    public void Session_OverLimit_ReturnsTrue()
    {
        var service = new SessionService();
        service.StartSession(2);

        service.AddDrink();
        service.AddDrink();
        service.AddDrink();

        Assert.IsTrue(service.IsOverLimit());
    }
}
