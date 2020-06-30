using Conexia.Domain.Shared.Facades;

namespace Conexia.Tests.Repositories
{
    public class FakeTemperatureService : ITemperatureFacade
    {
        object ITemperatureFacade.GetTemperatureCity(string City)
        {
            return 289.54;
        }
    }
}
