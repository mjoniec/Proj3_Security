using Data.Repositories;
using LightInject;

namespace Data.Services
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IGoldRepository, GoldRepository>(new PerRequestLifeTime());
            serviceRegistry.Register<IGoldService, GoldService>(new PerContainerLifetime());
        }
    }
}
