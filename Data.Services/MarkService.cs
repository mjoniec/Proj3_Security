using Data.Access.Model;
using Data.Access.Repositories;
using System.Collections.Generic;

namespace Data.Services
{
    internal class MarkService : IMarkService
    {
        IMarkRepository _markRepository;

        public MarkService(IMarkRepository markRepository)
        {
            _markRepository = markRepository;
        }

        public MarkModel GetById(string id)
        {
            return _markRepository.GetById(id);
        }

        public IEnumerable<MarkModel> GetAll()
        {
            return _markRepository.GetAll();
        }
    }
}
