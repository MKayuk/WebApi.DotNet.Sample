using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DotNet.Sample.Data.Repository.Interface;
using WebApi.DotNet.Sample.Domain.Model;

namespace WebApi.DotNet.Sample.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private IMongoDatabase Database { get; }
        private readonly IMongoCollection<CustomerModel> _customersCollection;

        public CustomerRepository(IConfiguration configuration)
        {
            if (configuration is null)
                throw new ArgumentException($"Invalid argument configuration {nameof(configuration)}");

            var settings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("MongoDbConnection"));
            settings.ClusterConfigurator = builder => builder.Subscribe(new MongoEventSubscriber());

            var client = new MongoClient(settings);

            Database = client.GetDatabase("customer");
            _customersCollection = Database.GetCollection<CustomerModel>("customer");
        }

        public async Task<string> Add(CustomerModel model)
        {
            var optionsCode = new CreateIndexOptions { Name = "_code_" };
            var indexCode = new IndexKeysDefinitionBuilder<CustomerModel>().Ascending(c => c.Code);
            var indexCodeModel = new CreateIndexModel<CustomerModel>(indexCode, optionsCode);
            await _customersCollection.Indexes.CreateOneAsync(indexCodeModel);
            await _customersCollection.InsertOneAsync(model);
            return model.Id.ToString();
        }

        public async Task<List<CustomerModel>> GetAll()
        {
            var result = await _customersCollection.Find(Builders<CustomerModel>.Filter.Empty).ToListAsync();

            return result;
        }

        public async Task<bool> Update(CustomerModel model, string id)
        {
            var selected = await GetCustomerById(id);
            if (selected is null)
                return false;

            selected.Document = model.Document;

            var filter = Builders<CustomerModel>.Filter.Eq("_id", ObjectId.Parse(id));
            await _customersCollection.ReplaceOneAsync(filter, selected);

            return true;
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<CustomerModel>.Filter.Eq("_id", ObjectId.Parse(id));
            await _customersCollection.DeleteOneAsync(filter);
            return true;
        }

        public async Task<CustomerModel> GetCustomerByDocument(int document)
        {
            var filter = Builders<CustomerModel>.Filter.Eq("document", document);
            var result = await _customersCollection
                .Find(filter)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<CustomerModel> GetCustomerById(string id)
        {
            var filter = Builders<CustomerModel>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await _customersCollection
                .Find(filter)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
