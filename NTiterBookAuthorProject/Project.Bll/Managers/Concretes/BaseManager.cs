using AutoMapper;
using Project.Bll.Dtos;
using Project.Bll.Managers.Abstracts;
using Project.Dal.Repositories.Abstracts;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Bll.Exceptions;
using Project.Bll.Executors.Abstract;
using Project.Bll.Exceptions.BusinessException;

namespace Project.Bll.Managers.Concretes
{

    public abstract class BaseManager<T,U> : IManager<T,U> where T : class,IDto where U : BaseEntity
    {
        private readonly IRepository<U> _repository;
        protected readonly IMapper _mapper;
        private readonly IManagerExecutor _executor;

        protected BaseManager(IRepository<U> repository, IMapper mapper, IManagerExecutor executor)
        {
            _repository = repository;
            _mapper = mapper;
            _executor = executor;
        }

        //public async Task CreateAsync(T entity)
        //{
        //    U domainEntity = _mapper.Map<U>(entity);

        //    domainEntity.CreatedDate = DateTime.Now;
        //    domainEntity.Status = Entities.Enums.DataStatus.Inserted;


        //    await _repository.CreateAsync(domainEntity);
        //}

        //public List<T> GetActives()
        //{
        //    List<U> values = _repository.Where(x => x.Status != Entities.Enums.DataStatus.Deleted).ToList();

        //    return _mapper.Map<List<T>>(values);
        //}

        //public async Task<List<T>> GetAllAsync()
        //{
        //    List<U> values = await _repository.GetAllAsync();
        //    return _mapper.Map<List<T>>(values);
        //}

        //public async Task<T> GetByIdAsync(int id)
        //{
        //    U value = await _repository.GetByIdAsync(id);
        //    return _mapper.Map<T>(value);
        //}

        //public  List<T> GetPassives()
        //{
        //    List<U> values = _repository.Where(x => x.Status == Entities.Enums.DataStatus.Deleted).ToList();
        //    return _mapper.Map<List<T>>(values);
        //}

        //public List<T> GetUpdateds()
        //{
        //    List<U> values = _repository.Where(x => x.Status == Entities.Enums.DataStatus.Updated).ToList();

        //    return _mapper.Map<List<T>>(values);
        //}

        //public async Task<string> HardDeleteAsync(int id)
        //{
        //    U originalValue = await _repository.GetByIdAsync(id);
        //    if (originalValue == null || originalValue.Status != Entities.Enums.DataStatus.Deleted)
        //         return "Sadece bulunabilen ve pasif veriler silinebilir";
        //    await _repository.DeleteAsync(originalValue);
        //    return $"{id} id'li veri silinmiştir";

        //}

        //public async Task<string> SoftDeleteAsync(int id)
        //{
        //    U originalValue = await _repository.GetByIdAsync(id);
        //    if (originalValue == null || originalValue.Status == Entities.Enums.DataStatus.Deleted)
        //        return "Veri ya zaten pasif ya da bulunamadı";
        //    originalValue.Status = Entities.Enums.DataStatus.Deleted;
        //    originalValue.DeletedDate = DateTime.Now;
        //    await _repository.SaveChangesAsync();
        //    return $"{id} id'li veri pasife cekilmiştir";



        //}

        //public async Task UpdateAsync(T entity)
        //{
        //    U originalValue = await _repository.GetByIdAsync(entity.Id);

        //    U newValue = _mapper.Map<U>(entity);
        //    newValue.UpdatedDate = DateTime.Now;
        //    newValue.Status = Entities.Enums.DataStatus.Updated;
        //    await _repository.UpdateAsync(originalValue, newValue);
        //}


        // CREATE
        public async Task CreateAsync(T entity) // asıl işi bu metot yapmaz. CreateCoreAsync yapar. Güvenlikte hata olursa yakalanır
        {
            await _executor.ExecuteAsync(() => CreateCoreAsync(entity));
        }

        protected virtual async Task CreateCoreAsync(T entity) // işi yapan metot
        {
            U domainEntity = _mapper.Map<U>(entity); // dışarıdan gelen DTO yu (T) domain entity e (U) çevirir

            domainEntity.CreatedDate = DateTime.Now;
            domainEntity.Status = Entities.Enums.DataStatus.Inserted; // enum güncellemesi

            await _repository.CreateAsync(domainEntity);
        }

        // GET ALL 
        public async Task<List<T>> GetAllAsync()
        {
            return await _executor.ExecuteAsync(() => GetAllCoreAsync());
        }

        protected virtual async Task<List<T>> GetAllCoreAsync()
        {
            List<U> values = await _repository.GetAllAsync();
            return _mapper.Map<List<T>>(values);
        }

        // GET BY ID
        public async Task<T> GetByIdAsync(int id)
        {
            return await _executor.ExecuteAsync(() => GetByIdCoreAsync(id));
        }

        protected virtual async Task<T> GetByIdCoreAsync(int id)
        {
            U value = await _repository.GetByIdAsync(id);
            return _mapper.Map<T>(value);
        }

        // UPDATE
        public async Task UpdateAsync(T entity)
        {
            await _executor.ExecuteAsync(() => UpdateCoreAsync(entity));
        }

        protected virtual async Task UpdateCoreAsync(T entity)
        {
            U originalValue = await _repository.GetByIdAsync(entity.Id);
            if (originalValue == null)
                throw new BusinessException("Güncellenecek veri bulunamadı");

            U newValue = _mapper.Map<U>(entity);
            newValue.UpdatedDate = DateTime.Now;
            newValue.Status = Entities.Enums.DataStatus.Updated;

            await _repository.UpdateAsync(originalValue, newValue);
        }

        // SOFT DELETE
        public async Task<string> SoftDeleteAsync(int id)
        {
            return await _executor.ExecuteAsync(() => SoftDeleteCoreAsync(id));
        }

        protected virtual async Task<string> SoftDeleteCoreAsync(int id)
        {
            U originalValue = await _repository.GetByIdAsync(id);

            if (originalValue == null)
                throw new BusinessException("Veri bulunamadı");

            if (originalValue.Status == Entities.Enums.DataStatus.Deleted)
                throw new BusinessException("Veri zaten pasif");

            originalValue.Status = Entities.Enums.DataStatus.Deleted;
            originalValue.DeletedDate = DateTime.Now;

            await _repository.SaveChangesAsync();
            return $"{id} id'li veri pasife çekilmiştir";
        }

        // HARD DELETE
        public async Task<string> HardDeleteAsync(int id)
        {
            return await _executor.ExecuteAsync(() => HardDeleteCoreAsync(id));
        }

        protected virtual async Task<string> HardDeleteCoreAsync(int id)
        {
            U originalValue = await _repository.GetByIdAsync(id);

            if (originalValue == null)
                throw new BusinessException("Veri bulunamadı");

            if (originalValue.Status != Entities.Enums.DataStatus.Deleted)
                throw new BusinessException("Sadece pasif veriler silinebilir");

            await _repository.DeleteAsync(originalValue);
            return $"{id} id'li veri silinmiştir";
        }

        // SYNC LISTS
        public List<T> GetActives()
        {
            List<U> values = _repository.Where(x => x.Status != Entities.Enums.DataStatus.Deleted).ToList();
            return _mapper.Map<List<T>>(values);
        }

        public List<T> GetPassives()
        {
            List<U> values = _repository.Where(x => x.Status == Entities.Enums.DataStatus.Deleted).ToList();
            return _mapper.Map<List<T>>(values);
        }

        public List<T> GetUpdateds()
        {
            List<U> values = _repository.Where(x => x.Status == Entities.Enums.DataStatus.Updated).ToList();
            return _mapper.Map<List<T>>(values);
        }
    }
}
