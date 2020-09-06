using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore_UI.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves a record from the database.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> Get(string url, int id);

        /// <summary>
        /// Retrieves a list of records from the database.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<IList<T>> Get(string url);

        /// <summary>
        /// Creates and adds a record to the database.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<bool> Create(string url, T obj);

        /// <summary>
        /// Updates a record in the database.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<bool> Update(string url, T obj, int id);

        /// <summary>
        /// Removes a record from the database.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(string url, int id);
    }
}
