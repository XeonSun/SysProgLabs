using Logic.contexts;
using Logic.Model;
using Logic.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SysProg
{
    public class ResRepository : IRepository<Resource>
    {
        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        private ResContext _resContext;

        public ResRepository()
        {
            _resContext = new ResContext();
        }
        /// <summary>
        /// Записи
        /// </summary>
        public IList<Resource> Data { get { return _resContext.Resources.ToList(); } }
        /// <summary>
        /// Добавление экземпляра ресурса
        /// </summary>
        /// <param name="data">Объект Resource</param>
        public void Add(Resource data)
        {
            if (data.Type != null)
            {
                _resContext.Resources.Add(data);
                _resContext.SaveChanges();
            }
            else
                throw new ArgumentException("Не корректный тип доступа.'открытый' или 'закрытый')");
        }

        public void Add(string path)
        {
            throw new Exception("Не доступно.");
        }
        /// <summary>
        /// Изменение записи
        /// </summary>
        /// <param name="index">Индекс изменяемой записи</param>
        /// <param name="data">Заменяющий объект</param>
        public void Edit(int index, Resource data)
        {
            if (data.Type != null)
            {
                Resource updated = Data[index];
                updated.Address = data.Address;
                updated.Type = data.Type;
                updated.Date = data.Date;
                _resContext.SaveChanges();
            }
            else
                throw new ArgumentException("Не корректный тип доступа.'открытый' или 'закрытый')");
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="index">Индекс удаляемой записи</param>
        public void Delete(int index)
        {
            if (index < Data.Count)
            {
                _resContext.Resources.Remove(Data[index]);
                _resContext.SaveChanges();
            }
        }

        private void DeleteAll()
        {
            _resContext.Resources.RemoveRange(Data);
            _resContext.SaveChanges();
        }
        /// <summary>
        /// Добавление группы записей
        /// </summary>
        /// <param name="files">Список записей</param>
        public void AddRange(IEnumerable<Resource> resources)
        {
            _resContext.Resources.AddRange(resources);
            _resContext.SaveChanges();
        }
        /// <summary>
        /// Импорт в csv файл
        /// </summary>
        /// <param name="path">Путь до файла</param>
        public void Import(string path)
        {
            DeleteAll();
            List<Resource> resources = new List<Resource>();
            CsvWorker.ImportResources(path, resources);
            _resContext.Resources.AddRange(resources);
            _resContext.SaveChanges();
        }
        /// <summary>
        /// Экспорт в csv файл
        /// </summary>
        /// <param name="path">Путь до файла</param>
        public void Export(string path)
        {
            CsvWorker.ExportResources(path, Data);
        }
    }
}
