using AutoMapper;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DotNet.Sample.Data.Repository.Interface;
using WebApi.DotNet.Sample.Domain.Model;
using WebApi.DotNet.Sample.DTO;
using WebApi.DotNet.Sample.Services.Interface;

namespace WebApi.DotNet.Sample.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<string> Add(CustomerDto dto)
        {
            return await _customerRepository.Add(_mapper.Map<CustomerModel>(dto));
        }

        public async Task<List<CustomerDto>> GetAll()
        {
            return _mapper.Map<List<CustomerDto>>( await _customerRepository.GetAll());
        }

        public async Task<bool> Update(CustomerDto dto, string id)
        {
            return await _customerRepository.Update(_mapper.Map<CustomerModel>(dto), id);
        }

        public async Task<bool> Delete(string id)
        {
            return await _customerRepository.Delete(id);
        }

        public async Task<CustomerDto> GetCustomerByDocument(int document)
        {
            return _mapper.Map<CustomerDto>(await _customerRepository.GetCustomerByDocument(document));
        }
    }
}
