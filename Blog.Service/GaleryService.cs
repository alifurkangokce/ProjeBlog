using Blog.Data;
using Blog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public interface IGaleryService
    {
        void Insert(Galery entity);
        void Update(Galery entity);
        void Delete(Galery entity);
        void Delete(Guid id);
        Galery Find(Guid id);
        IEnumerable<Galery> GetAll();
        IEnumerable<Galery> GetAllByTitle(string title);
        IEnumerable<Galery> Search(string title);
    }

    public class GaleryService : IGaleryService
    {
        private readonly IRepository<Galery> galeryRepository;
        private readonly IUnitOfWork unitOfWork;
        public GaleryService(IUnitOfWork unitOfWork, IRepository<Galery> galeryRepository)
        {
            this.unitOfWork = unitOfWork;
            this.galeryRepository = galeryRepository;
        }

        public void Delete(Galery entity)
        {
            galeryRepository.Delete(entity);
            unitOfWork.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var galery = galeryRepository.Find(id);
            if (galery != null)
            {
                this.Delete(galery);
            }
        }

        public Galery Find(Guid id)
        {
            return galeryRepository.Find(id);
        }

        public IEnumerable<Galery> GetAll()
        {
            return galeryRepository.GetAll();
        }

        public IEnumerable<Galery> GetAllByTitle(string title)
        {
            return galeryRepository.GetAll(w => w.Title.Contains(title));
        }

        public void Insert(Galery entity)
        {
            galeryRepository.Insert(entity);
            unitOfWork.SaveChanges();
        }

        public IEnumerable<Galery> Search(string title)
        {
            return galeryRepository.GetAll(e => e.Title.Contains(title));
        }

        public void Update(Galery entity)
        {
            var galery = galeryRepository.Find(entity.Id);
            galery.Title = entity.Title;
            galery.Photo = entity.Photo;
            galeryRepository.Update(galery);
            unitOfWork.SaveChanges();
        }
    }
}
