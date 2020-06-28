using System.Threading.Tasks;
using BookStore_UI.Models;

namespace BookStore_UI.Contracts
{
    public interface IAuthenticationRepository
    {
        public Task<bool> Register(RegistrationModel user);
    }
}
