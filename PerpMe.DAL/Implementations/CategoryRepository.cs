﻿using Microsoft.EntityFrameworkCore;
using PrepMe.DAL.Interfaces;
using PrepMe.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.DAL.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(PrepMeDbContext context) : base(context)
        {
        }

        public bool HasCategory(string category)
        {
            bool check = _context.Categories.Any(p => p.Equals(category));
            return check;
        }
    }
}