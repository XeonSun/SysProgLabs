﻿using LightInject;
using Logic.contexts;
using Logic.Model;
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
        public static readonly FileRepository fileRepository = new FileRepository();
        public static readonly ResRepository resRepository = new ResRepository();
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
            container.RegisterInstance(resRepository);
            container.Register<IMainView, MainForm>();
            container.Register<IFillView<File>, FileInputForm>();
            container.Register<IFillView<Resource>, ResourceInputForm>();
            container.Register<MainPresenter>();

            Controller controller = new Controller(container);
            controller.Run<MainPresenter>();
        }
    }
}
