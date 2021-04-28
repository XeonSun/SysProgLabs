using Logic.contexts;
using Logic.Model;
using Logic.models;
using SysProg.views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using File = Logic.models.File;

namespace SysProg.presenter
{
    public class MainPresenter : IPresenter
    {
        private IMainView _view;
        private IFillView<File> _fileView;
        private IFillView<Resource> _resourceView;
        private Controller _controller;
        private IRepository<File> _fileRepository;
        private IRepository<Resource> _resRepository;
        private ILowLevelModel _lowLevelModel;
        private IStructureAnalysisModel _analysisModel;
        private ILogWriter log = new LogWriter();

        public MainPresenter(IMainView view, Controller controller, IRepository<File> fileRepository, IRepository<Resource> resRepository, IFillView<File> fileView, IFillView<Resource> resourceView, IStructureAnalysisModel analysisModel, ILowLevelModel lowLevelModel)
        {
            _view = view;
            _controller = controller;
            _fileRepository = fileRepository;
            _fileView = fileView;
            _lowLevelModel = lowLevelModel;
            _analysisModel = analysisModel;
            _resourceView = resourceView;
            _resRepository = resRepository;


            _view.WhileAnalysis += WhileAnalysis;
            _view.ForAnalysis += ForAnalysis;
            _view.DivCalculation += DivCalculate;
            _view.XorCalculation += XorCalculate;
            _view.AddFile += AddFile;
            _view.AddResource += AddRes;
            _view.UpdateFile += UpdateFile;
            _view.UpdateResource += UpdateResourses;

            _view.DeleteFile += DeleteFile;
            _view.DeleteResource += DeleteRes;
            _view.ExportFiles += ExportFiles;
            _view.ImportFiles += ImportFiles;
            _view.ExportRes += ExportResources;
            _view.ImportRes += ImportResources;

            _view.UpdateFiles(_fileRepository.Data);
            _view.UpdateResources(_resRepository.Data);
        }

        private void AddFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "exe files (*.exe)|*.exe";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _fileRepository.Add(openFileDialog.FileName);
                    log.WriteToLog($@"Добавление записи о файле {openFileDialog.FileName}");
                }

            }
        }

        private void AddRes()
        {
            _resourceView.Show();
            _resourceView.Submit += AddResToRep;
        }

        private void AddResToRep()
        {
            Resource resource = new Resource();
            _resourceView.GetData(resource);
            try
            {
                _resRepository.Add(resource);
            _resourceView.Close();
            _resourceView = new ResourceInputForm();
            _view.UpdateResources(_resRepository.Data);
                log.WriteToLog("Добавление записи");
            }
            catch (Exception ex)
            {
                _fileView.SetError(ex.Message);
            }
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

        private void UpdateResourses()
        {
            int index = 0;
            _view.GetRecourceIndex(ref index);
            if (index != -1 && index < _resRepository.Data.Count)
            {
                _resourceView.Show();
                _resourceView.Submit += UpdateResInRep;
                _resourceView.SetData(_resRepository.Data[index]);
            }
        }

        private void UpdateFileInRep()
        {
            File file = new File();
            _fileView.GetData(file);
            try { 
                int index = 0;
                log.WriteToLog("Обновление записи " + index);
                _view.GetFileIndex(ref index);
                _fileRepository.Edit(index, file);
                _fileView.Close();
                _fileView = new FileInputForm();
                _view.UpdateFiles(_fileRepository.Data);
            }
            catch(Exception ex)
            {
                _fileView.SetError(ex.Message);
            }
        }


        private void UpdateResInRep()
        {
            Resource res = new Resource();
            _resourceView.GetData(res);
            try
            {
                int index = 0;
            log.WriteToLog("Обновление записи " + index);
            _view.GetRecourceIndex(ref index);
            _resRepository.Edit(index, res);
            _resourceView.Close();
            _resourceView = new ResourceInputForm();
            _view.UpdateResources(_resRepository.Data);
            }
            catch (Exception ex)
            {
                _fileView.SetError(ex.Message);
            }
        }



        private void DeleteFile()
        {
            int index = 0;
            _view.GetFileIndex(ref index);
            try
            {
                log.WriteToLog("Удаление записи " + index);
                _fileRepository.Delete(index);
                _view.UpdateFiles(_fileRepository.Data);
            }
            catch { }
        }

        private void DeleteRes()
        {
            int index = 0;
            _view.GetRecourceIndex(ref index);
            try
            {
                log.WriteToLog("Удаление записи " + index);
                _resRepository.Delete(index);
                _view.UpdateResources(_resRepository.Data);
            }
            catch { }
        }

        private void ExportFiles()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.CheckFileExists = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _fileRepository.Export(openFileDialog.FileName);
                    log.WriteToLog($@"Успешный экспорт {openFileDialog.FileName}");
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
                    try
                    {
                        List<File> files = new List<File>();
                        _fileRepository.Import(openFileDialog.FileName);
                        _view.UpdateFiles(_fileRepository.Data);
                        log.WriteToLog($@"Успешный импорт {openFileDialog.FileName}");
                    } catch
                    {
                        log.WriteToLog("Не корректное содержимое файла");
                    }
                }
            }
        }

        private void ExportResources()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.CheckFileExists = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _resRepository.Export(openFileDialog.FileName);
                        log.WriteToLog($@"Успешный экспорт {openFileDialog.FileName}");
                    }
                    catch
                    {
                        log.WriteToLog("Не корректное содержимое файла");
                    }
                }
            }
        }

        public void ImportResources()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        List<Resource> resources = new List<Resource>();
                        _resRepository.Import(openFileDialog.FileName);
                        _view.UpdateResources(_resRepository.Data);
                        log.WriteToLog($@"Успешный импорт {openFileDialog.FileName}");
                    }
                    catch
                    {
                        log.WriteToLog("Не корректное содержимое файла");
                    }
                }
            }
        }


        private void WhileAnalysis()
        {
            log.WriteToLog("Анализ конструкции языка While");
            string structure = "";
            _view.GetWhileStruct(ref structure);
            string result = _analysisModel.AnalysisWhile(structure);
            log.WriteToLog(result);
            _view.SetWhileResult(result);
        }

        private void ForAnalysis()
        {
            log.WriteToLog("Анализ конструкции языка For");
            string structure = "";
            _view.GetForStruct(ref structure);
            string result = _analysisModel.AnalysisFor(structure);
            log.WriteToLog(result);
            _view.SetForResult(result);
        }

        private void DivCalculate()
        {
            log.WriteToLog("Вычисление низкоуровневой функции деления");
            string a = "", b = "";
            _view.GetDivParams(ref a, ref b);
            string result = _lowLevelModel.Div(a, b);
            _view.SetDivResult(result);
            log.WriteToLog("Результат: "+result);
        }

        private void XorCalculate()
        {
            log.WriteToLog("Вычисление низкоуровневой функции XOR");
            string a = "", b = "";
            _view.GetXorParams(ref a, ref b);
            string result = _lowLevelModel.Xor(a, b);
            _view.SetXorResult(result);
            log.WriteToLog("Результат: " + result);
        }

        public void Run()
        {
            _view.Show();
            log.WriteToLog("Приложение запущено");
        }
    }
}
