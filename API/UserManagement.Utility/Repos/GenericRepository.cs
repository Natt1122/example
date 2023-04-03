using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManage.DAL.DBContext;
using UserManage.DAL.Repos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserManage.DAL.Repos.Contrato;

namespace UserManage.DAL.Repos
{
    public class GenericRepository<TModel>: Contrato.IGenericRepository<TModel> where TModel : class
    {
        private readonly UserManageContext _dbcontext;
       

        public GenericRepository(UserManageContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<TModel> Get(Expression<Func<TModel, bool>> filter)
        {
            try
            {
                TModel model = await _dbcontext.Set<TModel>().FirstOrDefaultAsync(filter);
                return model;
            }
            catch
            {
                throw;

            }
        }

        public async Task<TModel> Create(TModel model)
        {
            try
            {
                _dbcontext.Set<TModel>().Add(model);
                await _dbcontext.SaveChangesAsync();
                return model;

            }
            catch
            {
                throw;

            }
        }

        public async Task<bool> Update(TModel model)
        {
            try
            {
                _dbcontext.Set<TModel>().Update(model);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;

            }


        }

        public async Task<bool> Delete(TModel model)
        {
            try
            {
                _dbcontext.Set<TModel>().Remove(model);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;

            }
        }

        public async Task<IQueryable<TModel>> Consult(Expression<Func<TModel, bool>> filter=null)
        {
            try
            {
                IQueryable<TModel> queryModel = filter == null? _dbcontext.Set<TModel>() : _dbcontext.Set<TModel>().Where(filter);
                return queryModel;
            }
            catch
            {
                throw;

            }
        }






    }

}
