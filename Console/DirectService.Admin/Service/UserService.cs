using DH.Models.Dtos;
using DH.Models.Entities;
using DH.Models.param;
using DirectService.Admin.Contracts; 
using Framework.Core.Data;
using Framework.Utility;
using Framework.Utility.Extensions;
using Framework.Utility.Mapping;
using System.Linq.Expressions;

namespace DirectService.Admin.Service
{
    public class UserService : IUserService
    {
        private IRepository<Employee, int> _userRepository;
        public UserService(IRepository<Employee, int> userService)
        {
            this._userRepository = userService;
        }
        private Expression<Func<Employee, bool>> ListFilter(EmployeeParam param)
        {
            var expression = LinqExtensions.True<Employee>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.Name.Contains(param.Name));
                }
                
                if (!string.IsNullOrEmpty(param.EmployeeName))
                {
                    expression = expression.And(t => t.EmployeeName.Contains(param.EmployeeName));
                }
                if (param.Department > -1)
                {
                    expression = expression.And(t => t.Department == param.Department);
                } 
            }
            return expression;
        }
        public async Task<List<Employee>> GetPageList(Pagination pagination)
        { 
            var list = await _userRepository.FindList<Employee>(pagination);
            return list.ToList();
        }

        public async Task<List<Employee>> GetPageList(EmployeeParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await _userRepository.FindList(expression, pagination);
            return list.ToList();
        }

        public bool CheckExistsById(string name)
        {
            var a1= _userRepository.GetFirst(c => c.EmployeeName == name);

            var a2 = _userRepository.GetByKey(1);

            var ret = _userRepository.CheckExists(c => c.EmployeeName == name, a1.Id);
            return ret;
        }
        public bool CheckExists(string name)
        {
            var ret = _userRepository.CheckExists(c=>c.EmployeeName == name);
            return ret;
        }
        public BaseResponse CreateInfo(Employee model)
        {
            var ret = _userRepository.Insert(model);
            return ret > 0 ? new BaseResponse(successCode.Success) : new BaseResponse(successCode.NoChanged);
        }
        public BaseResponse CreateInfo(params EmployeeDto[] dtos)
        {
            var result = _userRepository.Insert(dtos,
                 dto =>
                 {
                
                 },
                 (dto, entity) =>
                 {
                     entity.BankCard = DateTime.Now.ToString(); 
                     return entity;
                 });
            return result;
        }
        public async Task<Employee> FindAsync()
        { 
            return await _userRepository.GetByKeyAsync(1);
        }

        public Employee Find(string employeeSerialNumber)
        { 
            employeeSerialNumber = employeeSerialNumber.Trim();
            return _userRepository.EntitiesAsNoTracking.FirstOrDefault(c => c.EmployeeSerialNumber == employeeSerialNumber)!;
        }
        public BaseResponse UpdateEmployee(params EmployeeDto[] dtos)
        { 
            var result = _userRepository.Update(dtos,
                dto =>
                {
                    dto = _userRepository.GetFirst(c=>c.Id == 1).MapTo(dto);
                },
                (dto, entity) =>
                { 
                    entity.BankCard = DateTime.Now.ToString();

                    return entity;
                });

            return result;
        }

        public BaseResponse UpdateEmployee(Employee Employee)
        { 
            Employee emp = _userRepository.Entities.FirstOrDefault();
            emp.Name = "admin666";
            emp.ExpirationDateUtc = DateTime.Now;

            var result = _userRepository.UnitOfWork.SaveChanges();
            //var result=  _userService.Update(emp);
              
            return result > 0 ? new BaseResponse(successCode.Success) : new BaseResponse(successCode.NoChanged);
        }

        public BaseResponse<Employee> FindEmployee(string employeeSerialNumber)
        {
            employeeSerialNumber = employeeSerialNumber.Trim();
            var employee = _userRepository.EntitiesAsNoTracking.FirstOrDefault(c => c.EmployeeSerialNumber == employeeSerialNumber);
            return new BaseResponse<Employee>(successCode.Success, "", employee ?? new Employee());
        }

        public Employee? GetUserByToken(string token)
        {
            token = token.Trim();
            return _userRepository.EntitiesAsNoTracking.FirstOrDefault(c => c.ApiToken == token);
        }

        public IQueryable<Employee> GetAllList()
        {
            return _userRepository.Entities;
        }
        public IQueryable<Employee> GetAll()
        {
            //var aa= _userService.GetInclude(c => c.EmployeeLogins).Where(c => true);

            var aa3 = _userRepository.GetByKey(1);

            Expression<Func<Employee,object>> expr1 = c => c.EmployeeLogins;

            Expression<Func<Employee, object>> expr2 = c => c.EmployeeDetail;

            var expr = new Expression<Func<Employee, object>>[] { c => c.EmployeeLogins, c => c.EmployeeDetail };


            var aa2 = _userRepository.QueryAsNoTracking(expr);

            var aa = _userRepository.GetIncludes(new string[] { "EmployeeLogins", "EmployeeDetail" });
            return aa;
        }
         
    }
}
