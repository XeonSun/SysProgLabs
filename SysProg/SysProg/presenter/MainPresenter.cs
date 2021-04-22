﻿using SysProg.views;
using Logic.contexts;
using Logic.Model;
using Logic.models;
using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using File = Logic.models.File;
using System.Text;

namespace SysProg.presenter
{
    public class MainPresenter : IPresenter
    {
        private IMainView _view;
        private IFillView<File> _fileView;
        private IFillView<Resource> _resourceView;
        private Controller _controller;
        private FileRepository _fileRepository;
        private ResContext _resourceContext;
        private ILogWriter log= new LogWriter();

        public MainPresenter(IMainView view, Controller controller, FileRepository fileRepository, ResContext resourceContext, IFillView<File> fileView, IFillView<Resource> resourceView)
        {
            _view = view;
            _controller = controller;
            _fileRepository = fileRepository;
            _resourceContext = resourceContext;
            _fileView = fileView;
            _resourceView = resourceView;

            _view.WhileAnalysis += WhileAnalysis;
            _view.ForAnalysis += ForAnalysis;
            _view.DivCalculation += DivCalculate;
            _view.XorCalculation += XorCalculate;
            _view.AddFile += AddFile;
            _view.UpdateFile += UpdateFile;
            _view.DeleteFile += DeleteFile;
            _view.ExportFiles += ExportFiles;
            _view.ImportFiles += ImportFiles;

            _view.UpdateFiles(_fileRepository.Data);
        }

        private void AddFile()
        {
            _fileView.Show();
            _fileView.Submit += AddFileToRep;
        }

        private void AddRec()
        {
            _fileView.Show();
            _fileView.Submit += AddFileToRep;
        }

        private void AddFileToRep()
        {
            File file = new File();
            _fileView.GetData(file);
            _fileRepository.Add(file);
            _fileView.Close();
            _fileView = new FileInputForm();
            _view.UpdateFiles(_fileRepository.Data);
            log.WriteToLog("Добавление записи");
        }

        private void UpdateFile()
        {
            int index = 0;
            _view.GetFileIndex(ref index);
            if (index != -1 && index < _fileRepository.Data.Count)
            {
                _fileView.Show();
                _fileView.Submit += UpdateFileInRep;
                _fileView.SetData(_fileRepository.Data[index]);
            }
        }

        private void UpdateFileInRep()
        {
            File file = new File();
            _fileView.GetData(file);
            int index = 0;
            log.WriteToLog("Обновление записи " + index);
            _view.GetFileIndex(ref index);
            _fileRepository.Edit(index, file);
            _fileView.Close();
            _fileView = new FileInputForm();
            _view.UpdateFiles(_fileRepository.Data);
        }

        private void DeleteFile()
        {
            int index = 0;
            _view.GetFileIndex(ref index);
            if (index != -1)
            {
                log.WriteToLog("Удаление записи " + index);
                _fileRepository.Delete(index);
                _view.UpdateFiles(_fileRepository.Data);
            }
        }

        private void ExportFiles()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    List<File> files = new List<File>();
                    _view.LoadFiles(files);
                    using (var streamWriter = new StreamWriter(filePath, false))
                    {
                        foreach (var file in files)
                        {
                            var str = new StringBuilder();
                            str.Append(file.Name).Append(',').Append(file.Version).Append(',').Append(file.Date).Append('\n');
                            streamWriter.Write(str.ToString());
                        }
                    }
                    
                }
            }
        }

        private void ImportFiles()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    List<File> files = new List<File>();
                    using (var streamReader = new StreamReader(filePath))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            string data = streamReader.ReadLine();
                            var file = data.Split(',');
                            files.Add(new File(file[0], file[1], DateTime.Parse(file[2])));
                        }
                    }
                    _fileRepository.DeleteAll();
                    _fileRepository.AddRange(files);
                    _view.UpdateFiles(_fileRepository.Data);
                    //log error // error toje nado dobavit// ya srat
                }
            }
        }


        private void WhileAnalysis()
        {
            log.WriteToLog("Анализ конструкции языка While");
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
                log.WriteToLog("Строка должна быть конструкцией языка C#.");
            }
            _view.SetWhileResult(result);
        }

        private void ForAnalysis()
        {
            log.WriteToLog("Анализ конструкции языка For");
            string structure = "";
            _view.GetForStruct(ref structure);
            string result;
            try
            {
                int analysis = StructureAnalysis.CheckStructFor(structure);
                result = "Конструкция for выполниться " + analysis + " раз.";
                log.WriteToLog("Конструкция for выполниться " + analysis + " раз.");
            }
            catch (ArgumentException ex)
            {
                result = ex.Message;
                log.WriteToLog("Некорректные аргументы "+ex.Message);
            }
            catch (Exception)
            {
                result = "Строка должна быть конструкцией языка C#.";
                log.WriteToLog("Строка должна быть конструкцией языка C#.");
            }
            _view.SetForResult(result);
        }

        private void DivCalculate()
        {
            log.WriteToLog("Вычисление низкоуровневой функции деления");
            string a = "", b = "";
            _view.GetDivParams(ref a, ref b);
            int x = 0, y = 0;
            if (!int.TryParse(a, out x) || !int.TryParse(b, out y))
            {
                _view.SetDivResult("Не корректные входные данные.");
                log.WriteToLog(": Не корректные входные данные.");
            }
            else
            {
                try
                {
                    int result = LowLevelFunctions.LowLevelFunctions.LowLelelDiv(x, y);
                    _view.SetDivResult(result.ToString());
                }
                catch (Exception ex)
                {
                    _view.SetDivResult("Ошибка");
                    log.WriteToLog("Произошла ошибка при вычислении: " + ex.Message);
                }
            }      
        }

        private void XorCalculate()
        {
            log.WriteToLog("Вычисление низкоуровневой функции XOR");
            string a = "", b = "";
            _view.GetXorParams(ref a, ref b);
            int x = 0, y = 0;
            if (!int.TryParse(a, out x) || !int.TryParse(b, out y))
            {
                _view.SetXorResult(DateTime.Now + "Не корректные входные данные.");
                log.WriteToLog("Не корректные входные данные.");
            }
            else
                _view.SetXorResult(LowLevelFunctions.LowLevelFunctions.LowLelelXor(x, y).ToString());
        }

        public void Run()
        {
            _view.Show();
            log.WriteToLog("Приложение запущено");
        }
    }
}
