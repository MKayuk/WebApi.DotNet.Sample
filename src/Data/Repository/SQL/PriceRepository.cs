using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces.Repositories;

namespace WebAPI.Infrastructure.Repositories
{
    public class PriceRepository : BaseRepository<PriceList>, IPriceRepository
    {
        public List<PriceList> GetCustomPriceList()
        {
            List<PriceList> list;
            try
            {
                var query =
                    @"SELECT p.PartNumber, pi.UnitPrice, pi.CurrencyCode, pi.InsertionDate as PriceDate
                      FROM cisco.priceitems pi
                        INNER JOIN cisco.products p
                        ON p.id = pi.productid 
                      ORDER BY pi.id DESC";

                list = Connection.Query<PriceList>(query).ToList();
            }
            catch (OracleException ex)
            {
                throw new System.Exception(ex.Message, ex.InnerException);
            }

            return list;
        }
    }
}
