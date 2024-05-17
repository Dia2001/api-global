using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Model.Interfaces
{
    public interface IAudit
    {
        DateTime? CreatedAt { get; set; }
        string? CreatedBy { get; set; }
        DateTime? UpdatedAt { get; set; }
        string? UpdatedBy { get; set; }
    }
}
