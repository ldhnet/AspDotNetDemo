using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DongHui.OAuth.Core.Interfaces
{
    public interface IUserInfoModel : IUserInfoErrorModel
    {
        string Name { get; set; }

        string Avatar { get; set; }
    }
}
