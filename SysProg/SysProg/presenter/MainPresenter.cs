﻿using SysProg.views;
using Logic.contexts;
using Logic.Model;
using Logic.models;
using System;

namespace SysProg.presenter
{
    public class MainPresenter : IPresenter
    {
        private IMainView _view;
        private Controller _controller;
        private FileRepository _fileRepository;
        private ResourceContext _resourceContext;

        public MainPresenter(IMainView view, Controller controller, FileRepository fileRepository, ResourceContext resourceContext)
        {
            _view = view;
            _controller = controller;
            _fileRepository = fileRepository;
            _resourceContext = resourceContext;

            _view.WhileAnalysis += WhileAnalysis;
            _view.ForAnalysis += ForAnalysis;
            _view.DivCalculation += DivCalculate;
            _view.XorCalculation += XorCalculate;
            _view.AddFile += AddFile;

            _view.UpdateFiles(_fileRepository.Data);
        }

        private void AddFile()
        {
            _fileRepository.Add(new File("Steam.exe", "v1.0.1", DateTime.Now));
            _view.UpdateFiles(_fileRepository.Data);
        }

        private void WhileAnalysis()
        {
            string structure = "";
            _view.GetWhileStruct(ref structure);
            string result;
            try
            {
                bool analysis = StructureAnalysis.CheckStructWhile(structure);
                result = analysis ? "Конструкция while выполнится хотя бы 1 раз." : "Конструкция while не выполнится ни разу.";
            }
            catch(ArgumentException ex)
            {
                result = ex.Message;
            }
            catch(Exception)
            {
                result = "Строка должна быть конструкцией языка C#.";
            }
            _view.SetWhileResult(result);
        }

        private void ForAnalysis()
        {
            string structure = "";
            _view.GetForStruct(ref structure);
            string result;
            try
            {
                int analysis = StructureAnalysis.CheckStructFor(structure);
                result = "Конструкция for выполниться " + analysis + " раз.";
            }
            catch (ArgumentException ex)
            {
                result = ex.Message;
            }
            catch (Exception)
            {
                result = "Строка должна быть конструкцией языка C#.";
            }
            _view.SetForResult(result);
        }

        private void DivCalculate()
        {
            string a = "", b = "";
            _view.GetDivParams(ref a, ref b);
            int x = 0, y = 0;
            if (!int.TryParse(a, out x) || !int.TryParse(b, out y))
                _view.SetDivResult("Не корректные входные данные.");
            else {
                try
                {
                    int result = LowLevelFunctions.LowLevelFunctions.LowLelelDiv(x, y);
                    _view.SetDivResult(result.ToString());
                }
                catch(Exception ex)
                {
                    _view.SetDivResult(ex.Message);
                }
        }      
        }

        private void XorCalculate()
        {
            string a = "", b = "";
            _view.GetXorParams(ref a, ref b);
            int x = 0, y = 0;
            if (!int.TryParse(a, out x) || !int.TryParse(b, out y))
                _view.SetXorResult("Не корректные входные данные.");
            else
                _view.SetXorResult(LowLevelFunctions.LowLevelFunctions.LowLelelXor(x, y).ToString());
        }

        public void Run()
        {
            _view.Show();
        }
    }
}