using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
	public class Pagination
	{
		public static async Task<List<T>> PaginateAsync<T>(IQueryable<T> source, int currentPage,int take)
		{
			return await source.Skip((currentPage - 1) * take).Take(take).ToListAsync();
		}
		public static int GetTotalPage(int totalCount, int take)
		{
			return (int)Math.Ceiling((decimal)totalCount / take);
		}
	}
}
