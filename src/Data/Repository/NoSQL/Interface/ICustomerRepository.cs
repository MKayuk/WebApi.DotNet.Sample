
namespace WebApi.DotNet.Sample.Data.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<string> Add(object model);
        Task<List<object>> GetAll();
        Task<bool> Update(object model, string id);
        Task<bool> Delete(string id);
        Task<object> GetCustomerByDocument(int document);
    }
}
