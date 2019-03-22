using Blog.Data;
using Blog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public interface IPostFileService
    {
        void Insert(PostFile entity);
        void Update(PostFile entity);
        void Delete(PostFile entity);
        void Delete(Guid id);
        PostFile Find(Guid id);
        IEnumerable<PostFile> GetAll();
    }
    public class PostFileService : IPostFileService
    {
        private readonly IRepository<PostFile> postfileRepository;
        private readonly IUnitOfWork unitOfWork;
        public PostFileService(IUnitOfWork unitOfWork, IRepository<PostFile> postfileRepository)
        {
            this.unitOfWork = unitOfWork;
            this.postfileRepository = postfileRepository;
        }
        public void Delete(PostFile entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public PostFile Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostFile> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(PostFile entity)
        {

            postfileRepository.Insert(entity);
            unitOfWork.SaveChanges();
        }

        public void Update(PostFile entity)
        {
            throw new NotImplementedException();
        }
    }
}
