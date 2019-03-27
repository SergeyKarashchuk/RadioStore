﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.DataAccessLayer.Abstracts
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}
