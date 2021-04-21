using SysProg.views;
using Logic.contexts;
using Logic.Model;
using Logic.models;
using System;

namespace SysProg.presenter
{
    public class MainPresenter : IPresenter
    {
        private IMainView _view;
        private IFillView<File> _fileView;
        private IFillView<Resource> _resourceView;
        private Controller _controller;
        private FileRepository _fileRepository;
        private ResourceContext _resourceContext;

        public MainPresenter(IMainView view, Controller controller, FileRepository fileRepository, ResourceContext resourceContext, IFillView<File> fileView, IFillView<Resource> resourceView)
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

            _view.UpdateFiles(_fileRepository.Data);
        }

        private void AddFile()
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
        }

        private void UpdateFile()
        {
            int index = 0;
            _view.GetFileIndex(ref index);
            if (index != -1 && index < _fileRepository.Data.Count)
            {
                Console.WriteLine(index);
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
                _fileRepository.Delete(index);
                _view.UpdateFiles(_fileRepository.Data);
            }
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
