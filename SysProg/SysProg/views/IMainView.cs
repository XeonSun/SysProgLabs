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

        //void LoadRecources(IList<File> recources);
        //void UpdateRecources(IList<File> recources);
        //void GetRecourceIndex(ref int index);
        //event Action AddRecource;
        //event Action UpdateRecource;
        //event Action DeleteRecource;

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
