using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Edc2Reporting.AuthenticationStartup.Areas.PersistentEntities.Models
{
    public class JsonRepository<T> : IRepository<T>
        where T : PersistantEntity
    {
        private string fileName = typeof(T).Name + ".json";
        private List<T> _inMemoryList = new List<T>();
        public async Task<List<T>> GetAllAsync()
        {
            if (_inMemoryList.Count > 0) return _inMemoryList;
            else
            {
                if (File.Exists(fileName))
                {
                    await Task.Run(() => Load(fileName));
                    return _inMemoryList;
                }
                else
                {
                    SaveToFile();
                }
            }
            return _inMemoryList;
        }

        private void SaveToFile()
        {
            try
            {
            string content = JsonConvert.SerializeObject(_inMemoryList);
            File.WriteAllText(fileName, content);
            }
            catch (Exception)
            {
                // todo - in the future write to log
                throw;
            }
        }

        private void Load(string fileName)
        {
            try
            {
                string fileContents = File.ReadAllText(fileName);
                List<T> entities = JsonConvert.DeserializeObject<List<T>>(fileContents);
                if (entities != null && entities.Count > 0)
                    _inMemoryList = entities;
            }
            catch (Exception ex)
            {
                // in the future - log here
                throw;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = _inMemoryList.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                await Task.Run(() => Load(fileName));
                result = _inMemoryList.FirstOrDefault(x => x.Id == id);
            }
            return result;
        }

        public async Task<bool> TryAddAsync(T entity)
        {
            try
            {
                var exist = await Task.Run(()=>_inMemoryList.FirstOrDefault(x => x.Id == entity.Id));
                if (exist == null)
                {
                    _inMemoryList.Add(entity);
                    SaveToFile();
                    return true;
                }
                else
                {
                    return false; // already exists
                }
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        

        public async Task<bool> TryDeleteAsync(int id)
        {
            var exist = await Task.Run(() => _inMemoryList.FirstOrDefault(x => x.Id == id));
            if (exist == null) return false;
            else exist.IsDeleted = true;
            SaveToFile();
            return true;
        }

        public async Task<bool> TryUnDeleteAsync(int id)
        {
            var exist = await Task.Run(() => _inMemoryList.FirstOrDefault(x => x.Id == id));
            if (exist == null) return false;
            else exist.IsDeleted = false;
            SaveToFile();
            return true;
        }

        public async Task<bool> TryUpdateAsync(T entity)
        {
            try
            {
                var exist = await Task.Run(() => _inMemoryList.FirstOrDefault(x => x.Id == entity.Id));
                if (exist == null)
                {
                    return false;
                }
                else
                {
                    _inMemoryList.Remove(exist);
                    _inMemoryList.Add(entity);
                    SaveToFile();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
