using Logic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysProg
{
    public interface IMainModel : IStructureAnalysisModel, ILowLevelModel
    {
        IList<File> Files { get; }
        IList<Resource> Resources { get; }

        void AddFile(File data);
        void AddFile(string path);
        void EditFile(int index, File data);
        void DeleteFile(int index);
        void ImportFiles(string path);
        void ExportFiles(string path);

        void AddResource(Resource data);
        void EditResource(int index, Resource data);
        void DeleteResource(int index);
        void ImportResources(string path);
        void ExportResources(string path);

    }
}
