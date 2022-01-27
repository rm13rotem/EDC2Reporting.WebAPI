using DataServices.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Providers
{
    public class FromUrlRepository<T> : IRepository<T>
        where T : IPersistentEntity, new()
    {
        private string _url;
        public FromUrlRepository(string url)
        {
            _url = url;
        }
        public void Delete(T entity)
        {
            return;
        }

        public IEnumerable<T> GetAll(bool isForceReload = false)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync(_url + "/" + typeof(T).Name.Split('.').Last()).Result;
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<List<T>>(jsonResult);

            return result;
        }

        public T GetById(int Id)
        {
            return default(T);
        }

        public void Update(T entity)
        {
            return;
        }

        public void UpsertActivation(T entity)
        {
            return;
        }
    }
}
