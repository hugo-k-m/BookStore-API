using System.Threading.Tasks;
using BlazorInputFile;

namespace BookStore_UI.Contracts
{
    public interface IFileUpload
    {
        Task UploadFile(IFileListEntry file, string picName);
        void RemoveFile(string picName);
    }
}
