using System.IO;
using System.Threading.Tasks;
using BlazorInputFile;

namespace BookStore_UI.Contracts
{
    public interface IFileUpload
    {
        void UploadFile(IFileListEntry file, MemoryStream msFile, string picName);
        void RemoveFile(string picName);
    }
}
