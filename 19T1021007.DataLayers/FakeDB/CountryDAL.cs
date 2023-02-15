using _19T1021007.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021007.DataLayers.FakeDB
{
    /// <summary>
    /// 
    /// </summary>
    public class CountryDAL : ICountryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<Country> List()
        {
            List<Country> data = new List<Country>();
            data.Add(new Country()
            {
                CountryName = "Việt Nam"
            });
            data.Add(new Country()
            {
                CountryName = "Trung Quốc"
            });
            data.Add(new Country()
            {
                CountryName = "Lào"
            });
            return data;
        }
    }
}
