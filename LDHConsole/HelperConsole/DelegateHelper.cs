using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperConsole
{ 
    public class DelegateHelper
    {
        private  List<Contest> GetProductList()
        {
            var products = new List<Contest>
                               {
                                   new Contest {Id = 1, Category = "Electronics", Value = 15.0},
                                   new Contest {Id = 2, Category = "Groceries", Value = 40.0},
                                   new Contest {Id = 3, Category = "Garden", Value = 210.3},
                                   new Contest {Id = 4, Category = "Pets", Value = 2.1},
                                   new Contest {Id = 5, Category = "Electronics", Value = 19.95},
                                   new Contest {Id = 6, Category = "Pets", Value = 21.25},
                                   new Contest {Id = 7, Category = "Pets", Value = 5.50},
                                   new Contest {Id = 8, Category = "Garden", Value = 13.0},
                                   new Contest {Id = 9, Category = "Automotive", Value = 10.0},
                                   new Contest {Id = 10, Category = "Electronics", Value = 250.0},
                               };
            return products;
        }
        public void UpdateProduct_1()
        {
            UpdateProductMoted(2, contest => { 
                if (contest.Id == 10)
                {
                    contest.Id =2;
                }
                else
                {
                    contest.Id = 3;
                }
            }); 
        }
        public void UpdateProductMoted(int contestID, Action<Contest> contestFunc = null)
        {
            var info = GetProductList().FirstOrDefault(c=>c.Id== contestID);
            if (contestFunc != null) contestFunc(info);

            var aa = info;
        }

      
        public void UpdateProduct_2()
        {
            WorkFlow dto = new WorkFlow { Id = 2, OptUser = "tom" };
            UpdateProductMoted_2<Contest>(dto, comDto => {
                if (dto.Id == 2)
                {
                    return new Contest { Id = 8, Category = "Garden", Value = 13.0 };
                }
                return new Contest { Id = 2, Category = "Garden", Value = 13.0 };
            },
            entity => {
                entity.Category = "testttttt";
            });
        }
        public void UpdateProductMoted_2<TEntity>(WorkFlow dto, Func<WorkFlow, TEntity> getFunc, Action<TEntity> contestFunc = null) where TEntity : class, ICommDto
        {
            var entity = default(TEntity);

            if (getFunc != null)
            {
                entity = getFunc(dto);
            }
            Debug.Assert(entity != null, "entity != null");
            var aaa = entity.ToString();
            var Id = entity.Id;

            var info = GetProductList().FirstOrDefault(c => c.Id == Id);

            if (contestFunc != null) contestFunc.Invoke(entity);

            var ttt = entity;
        }

        private static int FindApplicantId(ICommDto doc)
        {
            if (doc is Contest)
                return doc.Id;
            return 1;

        } 
    }
    public interface ICommDto
    {
        public int Id { get; set; } 
        public double Value { get; set; }

        Sys_WorkFlowAction WorkFlowAction { get; set; }
    }
    public class Contest: ICommDto
    { 
        public int Id { get; set; }
        public double Value { get; set; }
        public string Category { get; set; }
        public Sys_WorkFlowAction WorkFlowAction { get; set; } 

        public override string ToString()
        {
            return string.Format("[{0}: {1} - {2}]", Id, Category, Value);
        }
    }
    public class Sys_WorkFlowAction
    {
        public int Id { get; set; }
        public string Index { get; set; } 

    }
    public class WorkFlow 
    {
        public int Id { get; set; }
        public string OptUser { get; set; }
        public string OptTime { get; set; }
 
    }
}
