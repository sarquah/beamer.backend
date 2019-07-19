using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Models
{
    public enum EStatus
    {
        NotStarted = 1,
        InProgress = 2,
        OnHold = 3,
        Completed = 4,
        Terminated = 5
    }
}
