using DataServices.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataServices.Providers
{
    public class RepositoryOptions
    {
        public const string RepositorySettings = "RepositorySettings";
        private int repositoryTypeId;
        public int RepositoryTypeId { get => repositoryTypeId; set => repositoryTypeId = value; }
        public RepositoryType RepositoryType { get => (RepositoryType)repositoryTypeId; set => repositoryTypeId = (int)value; }

        public bool IsOverwritingOtherRepositories { get; set; }
    }
}
