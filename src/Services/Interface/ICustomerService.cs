using WebApi.DotNet.Sample.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DotNet.Sample.DTO;

namespace WebApi.DotNet.Sample.Services.Interface
{
    public interface ICustomerService
    {
        Task<string> Add(CustomerDto dto);
        Task<List<CustomerDto>> GetAll();
        Task<bool> Update(CustomerDto dto, string id);
        Task<bool> Delete(string id);
        Task<CustomerDto> GetCustomerByDocument(int document);
    }
}
