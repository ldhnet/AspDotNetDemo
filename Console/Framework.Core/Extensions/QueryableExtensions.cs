using Framework.Utility.Data;
using Framework.Utility.Extensions;
using System.Net;

namespace Framework.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static BaseOutputDto<IEnumerable<TEntity>> ToGridData<TEntity>(this IQueryable<TEntity> source, BasePaginationInputDto pageDto)
        {
            var filterResult = source.ToPageData(pageDto, out int total);

            return new BaseOutputDto<IEnumerable<TEntity>>(filterResult, total);
        }

        public static IEnumerable<TEntity> ToPageData<TEntity>(this IQueryable<TEntity> source, BasePaginationInputDto pageDto, out int total)
        {
            source.CheckNotNull("source");
            source.CheckNotNull("pageDto");

            total = source.Count();
            if (pageDto.sort == null || string.IsNullOrWhiteSpace(pageDto.sort.apiName))
            {//默认获取第一个属性倒序排列
                var firstProp = typeof(TEntity).GetProperties().First();
                pageDto.sort = new PaginationSortItem
                {
                    apiName = firstProp.Name,
                    order = "desc"
                };
            }

            var orderStr = $"{pageDto.sort.apiName} {pageDto.sort.order}";
            source = (IQueryable<TEntity>)source.OrderBy(orderStr);

            pageDto.currentPage = pageDto.currentPage < 1 ? 1 : pageDto.currentPage;

            var filterResult = source.Skip(pageDto.currentPage * pageDto.pageSize).Take(pageDto.pageSize).ToList();

            //匿名类无法SetValue，所以此处不应该操作匿名类
            if (typeof(TEntity).IsPublic)
            {
                filterResult
                              .AsParallel()
                              .ForAll(obj =>
                                  obj.GetType()
                                      .GetProperties()
                                      .Where(p => p.PropertyType == typeof(string))
                                      .AsParallel()
                                      .ForAll(p => p.SetValue(obj, WebUtility.HtmlEncode((string)p.GetValue(obj)))));
            }


            return filterResult;
        }
    }
}
