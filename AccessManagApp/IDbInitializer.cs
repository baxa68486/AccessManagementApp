﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagApp
{
    public interface IDbInitializer
    {
        void SeedData();
    }
}
