using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagApp
{
    public interface IDbInitializer
    {
        /// <summary>
        /// Adds some default values to the Db
        /// </summary>
        void SeedData();
    }
}
