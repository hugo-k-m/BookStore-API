using System.Threading.Tasks;
using BlazorInputFile;

namespace BookStore_UI.Contracts
{
    interface IFileUpload
    {
        public Task UploadFile(IFileListEntry file, string picName);
    }
}
