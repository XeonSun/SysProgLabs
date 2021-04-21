using LightInject;

namespace SysProg
{
    public class Controller
    {
        ServiceContainer _container;

        public Controller(ServiceContainer serviceContainer)
        {
            _container = serviceContainer;
            _container.RegisterInstance(this);
        }

        public void Run<TPresenter>() where TPresenter : class, IPresenter
        {
            var presenter = _container.GetInstance<TPresenter>();
            presenter.Run();
        }
    }
}
