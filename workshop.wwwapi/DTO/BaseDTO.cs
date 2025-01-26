using System.Linq.Expressions;
using workshop.wwwapi.DTO.Request.Appointment;
using workshop.wwwapi.DTO.Response.Patient;
using workshop.wwwapi.Models;
using workshop.wwwapi.Models.Interface;
using workshop.wwwapi.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace workshop.wwwapi.DTO
{
    public abstract class BaseDTO<DTO_type, Model_type>
        where DTO_type : BaseDTO<DTO_type, Model_type>, new()
        where Model_type : class, ICustomModel
    {
        protected BaseDTO(){}
        //private IQueryable<Y> query;
        public Func<IQueryable<Model_type>, IQueryable<Model_type>> queryLambda;
        //public Func<Y, bool> idLambda;
        public Func<IQueryable<Model_type>, IQueryable<Model_type>> idLambda;

        //protected Y instance;
        public static async Task<List<DTO_type>>  DTO(IRepository<Model_type> repo)
        {
            var a = new DTO_type();
            //a.query = repo.Table.AsQueryable();

            //var instances = await DefineIncludes_multi(repo)

            //a.defIncl(ref a.query);
            //await a.init(repo);
            a.defIncl(ref a.queryLambda);

            var instances = await a.DefineIncludes_multi(repo);
            var dtos = instances.Select(x => { var n = new DTO_type(); n.Instantiate(x); return n; }).ToList();

            return dtos;
        }
        //public static async Task<DTO_type>  DTO(IRepository<Model_type> repo, object id)
        public static async Task<DTO_type>  DTO(IRepository<Model_type> repo, params object[] id)
        {
            var a = new DTO_type();
            //a.query = repo.Table.AsQueryable();
            //a.defIncl(ref a.query);
            a.defIncl(ref a.queryLambda);
            a.def_id_Incl(ref a.idLambda, id);
            //await a.init(repo, id);
            var instance = await a.DefineIncludes_single(repo, id);
            a.Instantiate(instance);
            return a;
        }

        public static B ToDTO<A,B>(A input)
            where B : BaseDTO<B, A>, new()
            where A : class, ICustomModel
        {
            var b = new B();
            b.Instantiate(input);
            return b;
        }
        public static IEnumerable<B> ToDTOs<A,B>(IEnumerable<A> input)
            where B : BaseDTO<B, A>, new()
            where A : class, ICustomModel
        {
            List<B> dto_list = new();
            foreach (var a in input)
            {
                var b = new B();
                b.Instantiate(a);
                dto_list.Add(b);
            }
            
            return dto_list;
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
        private async Task<Model_type> DefineIncludes_single(IRepository<Model_type> repo, object id)
        {

            
            return await repo.GetEntry(  idLambda, [queryLambda]);
        }
        private async Task<IEnumerable<Model_type>> DefineIncludes_multi(IRepository<Model_type> repo)
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
        public abstract void defIncl(ref Func<IQueryable<Model_type>, IQueryable<Model_type>> queryLambda);
        //public abstract void def_id_Incl(ref IQueryable<Y> id_query, object id);
        //public abstract void def_id_Incl(ref Func<Y, bool> id_query, object id);
        public abstract void def_id_Incl(ref Func<IQueryable<Model_type>, IQueryable<Model_type>> id_query, params object[] id);
        public abstract void  Instantiate(Model_type instance);

        
    }
}
