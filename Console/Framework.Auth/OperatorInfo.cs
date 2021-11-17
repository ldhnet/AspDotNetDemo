﻿using Framework.Utility.Mapping;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Auth
{
    public class OperatorInfo
    {
        public long? UserId { get; set; }
        public int? UserStatus { get; set; }
        public int? IsOnline { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string WebToken { get; set; }
        public string ApiToken { get; set; }
        public int? IsSystem { get; set; }
        public string Portrait { get; set; }

        public long? DepartmentId { get; set; }

        [NotMapped]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 岗位Id
        /// </summary>
        [NotMapped]
        public string PositionIds { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        [NotMapped]
        public string RoleIds { get; set; }
    }
    public class RoleInfo
    {
        public long RoleId { get; set; }
    }
    public class Foo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Money { get; set; }
        public string CreateBy { get; set; }
    }
    [AutoInject(sourceType:typeof(Foo),targetType:typeof(FooDto))]
    public class FooDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Money { get; set; }
    }
}