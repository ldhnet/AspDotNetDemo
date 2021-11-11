using Framework.Utility.Extensions;
using Framework.Utility.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Framework.Utility.Data
{
    /// <summary>
    /// 分页查询列表输入DTO Base类
    /// </summary>
    public abstract class BasePaginationInputDto
    {
        /// <summary>
        /// 页索引
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "The min currentPage is 1")]
        public int currentPage { get; set; } = 1;
        /// <summary>
        /// 页大小
        /// </summary>
        public int pageSize { get; set; } = 10;
        /// <summary>
        /// 排序相关
        /// </summary>
        public PaginationSortItem sort { get; set; }
        /// <summary>
        /// 创建人员名称；非ID
        /// </summary>
        public string createBy { get; set; }
        /// <summary>
        /// 创建日期Range时间戳
        /// </summary>
        public IEnumerable<string> dateRange { get; set; }
        /// <summary>
        /// 数据状态 "active","inactive","all"
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 创建日期查询开始日期 ""/null/0代表不查询日期
        /// </summary>
        public long? beginCreateDate => ConvertToDateRang(dateRange).Item1;
        /// <summary>
        /// 创建日期查询结束日期 ""/null/0代表不查询日期
        /// </summary>
        public long? endCreateDate=>ConvertToDateRang(dateRange).Item2;
        /// <summary>
        /// 数据状态 null表示显示所有状态
        /// </summary>
        public bool? inactive
        {
            get
            {
                if (string.IsNullOrWhiteSpace(status) || status.Equals(FilterDataStatus.all.ToString(), StringComparison.OrdinalIgnoreCase))
                    return null;

                return status.Equals(FilterDataStatus.inactive.ToString(), StringComparison.OrdinalIgnoreCase);
            }
        }
        public ListSortDirection sortDirection
        {
            get
            {
                return sort != null && sort.order != "asc" ?
                    ListSortDirection.Descending : ListSortDirection.Ascending;
            }
        }

        /// <summary>
        /// Item1为StartDate,Item2为EndDate
        /// </summary>
        /// <param name="dateRange"></param>
        /// <returns></returns>
        protected Tuple<long?, long?> ConvertToDateRang(IEnumerable<string> dateRange)
        {
            //参数不传值
            if(dateRange==null)
                return new Tuple<long?, long?>(null, null);

            var dateArray = dateRange as string[] ?? dateRange.ToArray();
            if (!dateArray.Any())
                return new Tuple<long?, long?>(null, null);

            long? startDate = dateArray.First().ToLong(), endDate = null;
            if (dateArray.Length > 1)
                endDate = dateArray.Skip(1).First().ToLong();

            if (endDate.HasValue && endDate != 0)
                endDate = endDate + FrameConstant.OneDayToMillisecond;


            return new Tuple<long?, long?>(startDate == 0 ? null : startDate, endDate == 0 ? null : endDate);
        }
    }

    /// <summary>
    /// 排序信息
    /// </summary>
    public class PaginationSortItem
    {
        /// <summary>
        /// 参与排序的列名
        /// </summary>
        public string apiName { get; set; }
        /// <summary>
        /// 升序或降序 枚举类型：asc/desc
        /// </summary>
        public string order { get; set; }
    }

    public static class PaginationSortItemOrder
    {
        public static string ASC { get { return "asc"; } }

        public static string DESC { get { return "desc"; } }
    }
}
