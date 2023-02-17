﻿using PrepMe.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.DAL.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        public bool HasProduct(string product);
    }
}
