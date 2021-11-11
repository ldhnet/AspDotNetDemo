using System;
using System.Collections.Generic;
using System.Text;

namespace DH.Models
{
    public interface IMultConnectionStringProvider
    {
        string ConnectionString { get; }
    }
}
