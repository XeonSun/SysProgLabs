using Logic.contexts;
using Logic.Model;
using Logic.models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SysProg.models
{
    class MainModel : IMainModel
    {

        private FileContext _fileContext;
        private ResContext _resContext;

        public IList<File> Files => _fileContext.Files.ToList();

        public IList<Resource> Resources => _resContext.Resources.ToList();

        public MainModel()
        {
            _fileContext = new FileContext();
            _resContext = new ResContext();
        }

        public void AddFile(File data)
        {
            if (data.Name != null)
            {
                _fileContext.Files.Add(data);
                _fileContext.SaveChanges();
            }
            else
                throw new ArgumentException("Не корректное имя файла. Должен заканчиваться на .exe");
        }

        public void AddFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                var info = new System.IO.FileInfo(path);
                if (info.Extension != "exe")
                {
                    _fileContext.Files.Add(new File(info.Name, System.Diagnostics.FileVersionInfo.GetVersionInfo(path).FileVersion, info.CreationTime));
                    _fileContext.SaveChanges();
                    return;
                }
            }
            throw new ArgumentException("Не корректное имя файла. Должен заканчиваться на .exe");
        }

        public void AddResource(Resource data)
        {
            if (data.Type != null)
            {
                _resContext.Resources.Add(data);
                _resContext.SaveChanges();
            }
            else
                throw new ArgumentException("Не корректный тип доступа.'открытый' или 'закрытый')");
        }

        public string AnalysisFor(string structure)
        {
            try
            {
                int analysis = StructureAnalysis.CheckStructFor(structure);
                return "Конструкция for выполниться " + analysis + " раз.";
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
            catch (Exception)
            {
                return "Строка должна быть конструкцией языка C#.";
            }
        }

        public string AnalysisWhile(string structure)
        {
            try
            {
                bool analysis = StructureAnalysis.CheckStructWhile(structure);
                return analysis ? "Конструкция while выполнится хотя бы 1 раз." : "Конструкция while не выполнится ни разу.";
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
            catch (Exception)
            {
                return "Строка должна быть конструкцией языка C#.";
            }
        }

        public void DeleteFile(int index)
        {
            if (index < Files.Count)
            {
                _fileContext.Files.Remove(Files[index]);
                _fileContext.SaveChanges();
            }
        }

        public void DeleteResource(int index)
        {
            if (index < Resources.Count)
            {
                _resContext.Resources.Remove(Resources[index]);
                _resContext.SaveChanges();
            }
        }

        public string Div(string a, string b)
        {
            int x = 0, y = 0;
            if (!int.TryParse(a, out x) || !int.TryParse(b, out y))
            {
                return "Не корректные входные данные.";
            }
            else
            {
                try
                {
                    int result = LowLevelFunctions.LowLevelFunctions.LowLelelDiv(x, y);
                    return result.ToString();
                }
                catch (Exception ex)
                {
                    return "Произошла ошибка при вычислении: " + ex.Message;
                }
            }
        }

        public void EditFile(int index, File data)
        {
            if (data.Name != null)
            {
                File updated = Files[index];
                updated.Name = data.Name;
                updated.Version = data.Version;
                updated.Date = data.Date;
                _fileContext.SaveChanges();
            }
            else
                throw new ArgumentException("Не корректное имя файла. Должен заканчиваться на .exe");
        }

        public void EditResource(int index, Resource data)
        {
            if (data.Type != null)
            {
                Resource updated = Resources[index];
                updated.Address = data.Address;
                updated.Type = data.Type;
                updated.Date = data.Date;
                _resContext.SaveChanges();
            }
            else
                throw new ArgumentException("Не корректный тип доступа.'открытый' или 'закрытый')");
        }

        public void ExportFiles(string path)
        {
            CsvWorker.ExportFiles(path, Files);
        }

        public void ExportResources(string path)
        {
            CsvWorker.ExportResources(path, Resources);
        }

        public void ImportFiles(string path)
        {
            _fileContext.Files.RemoveRange(Files);
            _fileContext.SaveChanges();
            List<File> files = new List<File>();
            CsvWorker.ImportFiles(path, files);
            _fileContext.Files.AddRange(files);
            _fileContext.SaveChanges();
        }

        public void ImportResources(string path)
        {
            _resContext.Resources.RemoveRange(Resources);
            _resContext.SaveChanges();
            List<Resource> resources = new List<Resource>();
            CsvWorker.ImportResources(path, resources);
            _resContext.Resources.AddRange(resources);
            _resContext.SaveChanges();
        }

        public string Xor(string a, string b)
        {
            int x = 0, y = 0;
            if (!int.TryParse(a, out x) || !int.TryParse(b, out y))
            {
                return "Не корректные входные данные.";
            }
            else
                return LowLevelFunctions.LowLevelFunctions.LowLelelXor(x, y).ToString();
        }
    }
}
