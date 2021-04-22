using Logic.models;
using System;
using System.Collections.Generic;

namespace SysProg.views
{
    public interface IMainView : IView
    {
        void LoadFiles(IList<File> files);
        void UpdateFiles(IList<File> files);
        void GetFileIndex(ref int index);
        event Action ExportFiles;
        event Action ImportFiles;
        event Action AddFile;
        event Action UpdateFile;
        event Action DeleteFile;

        void LoadResources(IList<Resource> recources);
        void UpdateResources(IList<Resource> recources);
        void GetRecourceIndex(ref int index);
        event Action AddResource;
        event Action UpdateResource;
        event Action DeleteResource;
        event Action ExportRes;
        event Action ImportRes;

        void GetWhileStruct(ref string structure);
        void SetWhileResult(string result);
        event Action WhileAnalysis;

        void GetForStruct(ref string structure);
        void SetForResult(string result);
        event Action ForAnalysis;

        void GetDivParams(ref string a,ref string b);
        void SetDivResult(string result);
        event Action DivCalculation;

        void GetXorParams(ref string a, ref string b);
        void SetXorResult(string result);
        event Action XorCalculation;
    }
}
