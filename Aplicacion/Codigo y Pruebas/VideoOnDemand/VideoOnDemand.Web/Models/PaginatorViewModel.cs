using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class PaginatorViewModel<T> where T : class
    {
        public int TotalPages { get; set; }
        public int TotalRows { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}