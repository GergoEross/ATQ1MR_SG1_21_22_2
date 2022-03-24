using ATQ1MR_HFT_2021221.Data;
using ATQ1MR_HFT_2021221.Repository.Interfaces;
using System.Linq;

namespace ATQ1MR_HFT_2021221.Repository
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        protected PcPartsDbContext Context;
        public RepositoryBase(PcPartsDbContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> ReadAll()
        {
            return Context.Set<TEntity>();
        }
        public abstract TEntity Read(TKey id);

        public TEntity Create(TEntity entity)
        {
            var result = Context.Add(entity);
            Context.SaveChanges();
            return result.Entity;
        }
        public TEntity Update(TEntity entity)
        {
            var result = Context.Update(entity);
            Context.SaveChanges();
            return result.Entity;
        }

        public void Delete(TKey id)
        {
            Context.Remove(Read(id));
            Context.SaveChanges();
        }



    }
}
