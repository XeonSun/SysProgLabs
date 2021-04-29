using Logic.models;
using SysProg.views;
using System;
using System.Collections.Generic;
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
        private IMainModel _model;
        private ILogWriter log = new LogWriter();

        public MainPresenter(IMainView view, Controller controller, IFillView<File> fileView, IFillView<Resource> resourceView, IMainModel model)
        {
            _view = view;
            _controller = controller;
            _model = model;
            _fileView = fileView;
            _resourceView = resourceView;


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

            _view.UpdateFiles(_model.Files);
            _view.UpdateResources(_model.Resources);
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
                    _model.AddFile(openFileDialog.FileName);
                    _view.UpdateFiles(_model.Files);
                    log.WriteToLog($@"Добавление записи о файле {openFileDialog.FileName}");
                }

            }
        }

        private void AddRes()
        {
            _resourceView = new ResourceInputForm();
            _resourceView.Show();
            _resourceView.Submit += AddResToRep;
        }

        private void AddResToRep()
        {
            Resource resource = new Resource();
            _resourceView.GetData(resource);
            try
            {
                _model.AddResource(resource);
            _resourceView.Close();        
            _view.UpdateResources(_model.Resources);
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
            if (index != -1 && index < _model.Files.Count)
            {
                _fileView = new FileInputForm();
                _fileView.Show();
                _fileView.Submit += UpdateFileInRep;
                _fileView.SetData(_model.Files[index]);
            }
        }

        private void UpdateResourses()
        {
            int index = 0;
            _view.GetRecourceIndex(ref index);
            if (index != -1 && index < _model.Resources.Count)
            {
                _resourceView = new ResourceInputForm();
                _resourceView.Show();
                _resourceView.Submit += UpdateResInRep;
                _resourceView.SetData(_model.Resources[index]);
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
                _model.EditFile(index, file);
                _fileView.Close();
                _view.UpdateFiles(_model.Files);
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
            _model.EditResource(index, res);
            _resourceView.Close();
            _view.UpdateResources(_model.Resources);
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
                _model.DeleteFile(index);
                _view.UpdateFiles(_model.Files);
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
                _model.DeleteResource(index);
                _view.UpdateResources(_model.Resources);
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
                    _model.ExportFiles(openFileDialog.FileName);
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
                        _model.ImportFiles(openFileDialog.FileName);
                        _view.UpdateFiles(_model.Files);
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
                        _model.ExportResources(openFileDialog.FileName);
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
                        _model.ImportResources(openFileDialog.FileName);
                        _view.UpdateResources(_model.Resources);
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
            string result = _model.AnalysisWhile(structure);
            log.WriteToLog(result);
            _view.SetWhileResult(result);
        }

        private void ForAnalysis()
        {
            log.WriteToLog("Анализ конструкции языка For");
            string structure = "";
            _view.GetForStruct(ref structure);
            string result = _model.AnalysisFor(structure);
            log.WriteToLog(result);
            _view.SetForResult(result);
        }

        private void DivCalculate()
        {
            log.WriteToLog("Вычисление низкоуровневой функции деления");
            string a = "", b = "";
            _view.GetDivParams(ref a, ref b);
            string result = _model.Div(a, b);
            _view.SetDivResult(result);
            log.WriteToLog("Результат: "+result);
        }

        private void XorCalculate()
        {
            log.WriteToLog("Вычисление низкоуровневой функции XOR");
            string a = "", b = "";
            _view.GetXorParams(ref a, ref b);
            string result = _model.Xor(a, b);
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
