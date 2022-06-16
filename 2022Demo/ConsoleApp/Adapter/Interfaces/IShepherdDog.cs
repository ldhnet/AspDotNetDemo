using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Adapter
{
    /// 牧羊犬
    /// </summary>
    internal interface IShepherdDog : IDog
    {       
        /// <summary>
        /// 牧羊
        /// </summary>
        public void Shepherd();
    }
}
