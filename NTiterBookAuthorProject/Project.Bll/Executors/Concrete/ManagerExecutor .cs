using Project.Bll.Exceptions.BusinessException;
using Project.Bll.Executors.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll.Executors.Concrete
{
    public class ManagerExecutor : IManagerExecutor
    {
        public async Task ExecuteAsync(Func<Task> action) // void. Func<Task> action = verilen işi çalıştırır, action : delegate
        {
            try
            {
                await action(); // metotlar çalışır
            }
            catch (BusinessException)
            {
                throw; // business hatasıysa dokunmaz
            }
            catch (Exception ex)
            {
                throw new BusinessException("Manager işlemi sırasında hata: " + ex.Message);
            }
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> action)
        {
            try
            {
                return await action();
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Manager işlemi sırasında hata: " + ex.Message);
            }
        }
    }
}
