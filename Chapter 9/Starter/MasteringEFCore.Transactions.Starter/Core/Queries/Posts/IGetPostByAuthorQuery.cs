﻿using MasteringEFCore.Transactions.Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Starter.Core.Queries.Posts
{
    public interface IGetPostByAuthorQuery<T> : 
        IQueryHandler<IEnumerable<Post>>, IQueryHandlerAsync<IEnumerable<Post>>
    {
        string Author { get; set; }
    }
}
