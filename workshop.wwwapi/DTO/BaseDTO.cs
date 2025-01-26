using System.Linq.Expressions;
using workshop.wwwapi.DTO.Request.Appointment;
using workshop.wwwapi.DTO.Response.Patient;
using workshop.wwwapi.Models;
using workshop.wwwapi.Models.Interface;
using workshop.wwwapi.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace workshop.wwwapi.DTO
{
    public abstract class BaseDTO<T, Y>
        where T : BaseDTO<T, Y>, new()
        where Y : class, ICustomModel
    {
        protected BaseDTO(){}
        //private IQueryable<Y> query;
        public Func<IQueryable<Y>, IQueryable<Y>> queryLambda;
        //public Func<Y, bool> idLambda;
        public Func<IQueryable<Y>, IQueryable<Y>> idLambda;

        //protected Y instance;
        public static async Task<List<T>>  DTO(IRepository<Y> repo)
        {
            var a = new T();
            //a.query = repo.Table.AsQueryable();

            //var instances = await DefineIncludes_multi(repo)

            //a.defIncl(ref a.query);
            //await a.init(repo);
            a.defIncl(ref a.queryLambda);

            var instances = await a.DefineIncludes_multi(repo);
            var dtos = instances.Select(x => { var n = new T(); n.Instantiate(x); return n; }).ToList();

            return dtos;
        }
        public static async Task<T>  DTO(IRepository<Y> repo, object id)
        {
            var a = new T();
            //a.query = repo.Table.AsQueryable();
            //a.defIncl(ref a.query);
            a.defIncl(ref a.queryLambda);
            a.def_id_Incl(ref a.idLambda, id);
            //await a.init(repo, id);
            var instance = await a.DefineIncludes_single(repo, id);
            a.Instantiate(instance);
            return a;
        }

        //private async Task init(IRepository<Y> repo, object? id = null)
        //{
        //    //var instance = await DefineIncludes(repo, id);
        //    //var instance = await DefineIncludes(repo, id);

        //    if (id == null)
        //    {
        //        var instances = await DefineIncludes_multi(repo);

        //        instances.Select(w => { w });
                
        //    }
        //    else
        //    {
        //        var instance = await DefineIncludes_single(repo, id);
        //        Instantiate(instance);
        //    }

        //}

        //private async Task<Y> DefineIncludes(IRepository<Y> repo, object id)
        private async Task<Y> DefineIncludes_single(IRepository<Y> repo, object id)
        {

            
            return await repo.GetEntry(  idLambda, [queryLambda]);
        }
        private async Task<IEnumerable<Y>> DefineIncludes_multi(IRepository<Y> repo)
        {
            return await repo.GetEntries([queryLambda]);

        }

        //private async Task<Y> DefineIncludes(Func<Task<IEnumerable<Y>>> repoFunc)
        //{
        //    await repoFunc.Invoke();
        //}

        //private async Task<Y> DefineIncludes(Func<Func<IQueryable<Y>, IQueryable<Y>>[], Task<Y>> repoFunc)
        //    //where Z : Task<IEnumerable<T>>,Y, IEnumerable<Y>
        //{
        //    await repoFunc.Invoke(query);
        //}

        //public abstract Task<Y>  DefineIncludes(IRepository<Y> repo, object id);
        //public async Task<Y> DefineIncludes(Func<Func<IQueryable<T>, IQueryable<T>>[], Task<Y>> repoFunc)
        //{

        //    repoFunc.Invoke(query);

        //}
        //public abstract void defIncl(ref IQueryable<Y> query);
        public abstract void defIncl(ref Func<IQueryable<Y>, IQueryable<Y>> queryLambda);
        //public abstract void def_id_Incl(ref IQueryable<Y> id_query, object id);
        //public abstract void def_id_Incl(ref Func<Y, bool> id_query, object id);
        public abstract void def_id_Incl(ref Func<IQueryable<Y>, IQueryable<Y>> id_query, object id);
        public abstract void  Instantiate(Y instance);

        
    }
}
