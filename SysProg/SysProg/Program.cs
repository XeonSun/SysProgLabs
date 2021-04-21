using LightInject;
using Logic.contexts;
using Logic.Model;
using SysProg.presenter;
using SysProg.views;
using System;
using System.Windows.Forms;

namespace SysProg
{
    static class Program
    {
        public static readonly ApplicationContext context = new ApplicationContext();
        public static readonly FileRepository fileRepository = new FileRepository();
        public static readonly ResourceContext resourceContext = new ResourceContext();

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ServiceContainer container = new ServiceContainer();
            container.RegisterInstance(context);
            container.RegisterInstance(fileRepository);
            container.RegisterInstance(resourceContext);
            container.Register<IMainView, MainForm>();
            container.Register<MainPresenter>();

            Controller controller = new Controller(container);
            controller.Run<MainPresenter>();
        }
    }
}
