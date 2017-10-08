﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.QueryObjectPattern.Final.Data;
using MasteringEFCore.QueryObjectPattern.Final.Models;
using System.Linq.Expressions;
using MasteringEFCore.QueryObjectPattern.Final.Core.Queries.Posts;
using MasteringEFCore.QueryObjectPattern.Final.Infrastructure.QueriesWithExpressions.Expressions.Posts;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.QueryObjectPattern.Final.Infrastructure.QueriesWithExpressions.Posts
{
    public class GetPaginatedPostByKeywordQuery : QueryBase, IGetPaginatedPostByKeywordQuery<GetPaginatedPostByKeywordQuery>
    {
        public GetPaginatedPostByKeywordQuery(BlogContext context) : base(context)
        {
        }

        public string Keyword { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public bool IncludeData { get; set; }

        public IEnumerable<Post> Handle()
        {
            var expression = new GetPaginatedPostByKeywordQueryExpression
            {
                Keyword = Keyword
            };
            return IncludeData
                ? Context.Posts.Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                    .AsQueryable()
                    .Where(expression.AsExpression())
                    .Skip(PageNumber - 1).Take(PageCount)
                    .ToList()
                : Context.Posts
                    .AsQueryable()
                    .Where(expression.AsExpression())
                    .Skip(PageNumber - 1).Take(PageCount)
                    .ToList();
        }

        public async Task<IEnumerable<Post>> HandleAsync()
        {
            var expression = new GetPaginatedPostByKeywordQueryExpression
            {
                Keyword = Keyword
            };
            return IncludeData
                ? await Context.Posts.Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                    .AsQueryable()
                    .Where(expression.AsExpression())
                    .Skip(PageNumber - 1).Take(PageCount)
                    .ToListAsync()
                : await Context.Posts
                    .AsQueryable()
                    .Where(expression.AsExpression())
                    .Skip(PageNumber - 1).Take(PageCount)
                    .ToListAsync();
        }
    }
}
