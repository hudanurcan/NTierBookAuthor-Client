using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll.Executors.Abstract
{
    public interface IManagerExecutor
    {
        Task ExecuteAsync(Func<Task> action);
        Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> action); // hazır func delegate
    }
}
