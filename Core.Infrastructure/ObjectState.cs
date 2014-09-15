using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    /// <summary>
    /// ObjectState
    /// </summary>
    public enum ObjectState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }
}
