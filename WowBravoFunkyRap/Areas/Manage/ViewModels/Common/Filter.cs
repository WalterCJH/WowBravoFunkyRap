using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace WowBravoFunkyRap.Areas.Manage.ViewModels.Common
{
    public class Filter<T>
    {
        [Display(Name = "關鍵字")]
        public string? Keyword { get; set; }

        [RegularExpression(@"(DisplaySeq)")]
        public virtual string SortBy { get; set; } = "DisplaySeq";

        [RegularExpression(@"(ASC|DESC)")]
        public string SortDirection { get; set; } = "DESC";

        [Display(Name = "頁數")]
        public int PageNo { get; set; } = 1;

        [Display(Name = "數量")]
        public int PageSize { get; set; } = 10;

        public IPagedList<T> Results { get; set; }

        public string GetSortBy(string columnName)
        {
            if (SortBy == columnName)
            {
                if (SortDirection == "DESC")
                {
                    return "ASC";
                }
                return "DESC";
            }

            return "ASC";
        }
        public string GetSortByClass(string columnName)
        {
            if (SortBy == columnName)
            {
                if (SortDirection == "ASC")
                {
                    return "sorting_asc";
                }
                else
                {
                    return "sorting_desc";
                }
            }

            return "";
        }
        public object SetRouteValue(string sortBy, string sortDirection, int? pageNo = null)
        {
            var value = new { SortBy = sortBy, SortDirection = sortDirection, PageNo = pageNo, Keyword };
            return value;
        }
    }
}
