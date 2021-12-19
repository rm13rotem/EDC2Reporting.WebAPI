using Components;
using DataServices.Interfaces;
using DataServices.Providers;
using System.Collections.Generic;

namespace MainStaticMaintainableEntities.ModuleAssembly
{
    public class Module : IPersistentEntity
    {
        public List<AbstractComponent> Components { get; set; }


        public int Id { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }
        public string JsonValue { get; set; }
        public string Name { get; set; }

        public bool TryLoadComponentsFromDb(List<int> CompoentPrimaryKeys_Ids)
        {
            try
            {
                IRepositoryLocator<AbstractComponent> repositoryLocator = new RepositoryLocator<AbstractComponent>();
                var repository = repositoryLocator.GetRepository(RepositoryType.FromJsonRepository);//, null, false);

                return true;
            }
            catch (System.Exception)
            {
                return false;
                //throw;
            }
        }

        public override string ToString()
        {
            var result = base.ToString();
            if (Components != null && Components.Count > 0)
                result += $" class - contains {Components.Count} components.";
            else result += " class. NotLoaded";
            return result;
        }
    }
}
