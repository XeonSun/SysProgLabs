using LightInject;
using Logic.models;
using SysProg.presenter;
using SysProg.views;
using System;
using System.Windows.Forms;

namespace SysProg
{
    static class Program
    {
        public static readonly ApplicationContext context = new ApplicationContext();
        
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
            container.Register<IStructureAnalysisModel, StuctureAnalysisModel>();
            container.Register<ILowLevelModel, LowLevelModel>();
            container.Register<IRepository<File>, FileRepository>();
            container.Register<IRepository<Resource>, ResRepository>();
            container.Register<IMainView, MainForm>();
            container.Register<IFillView<File>, FileInputForm>();
            container.Register<IFillView<Resource>, ResourceInputForm>();
            container.Register<MainPresenter>();

            Controller controller = new Controller(container);
            controller.Run<MainPresenter>();
        }
    }
}
