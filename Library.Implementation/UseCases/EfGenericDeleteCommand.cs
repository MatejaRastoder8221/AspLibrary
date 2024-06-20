//using Library.Application.Exceptions;
//using Library.application.UseCases;
//using Library.domain;
//using Library.Implementation.UseCases;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Library.DataAccess;

//namespace Implementation.UseCases
//{
//    public abstract class EfGenericDeleteCommand<TEntity> : EfUseCase, ICommand<int>
//        where TEntity : Entity
//    {
//        protected EfGenericDeleteCommand(AspContext context)
//            : base(context)
//        {
//        }

//        public abstract int Id { get; }

//        public abstract string Name { get; }

//        public string Description => throw new NotImplementedException();

//        public void Execute(int data)
//        {
//            TEntity item = Context.Set<TEntity>().Find(data) ?? throw new EntityNotFoundException(typeof(TEntity).Name, data);
//            if (item.IsActive == false)
//                throw new EntityNotFoundException(nameof(TEntity), data);
//            item.IsActive = false;
//            Context.SaveChanges();
//        }
//    }
//}
