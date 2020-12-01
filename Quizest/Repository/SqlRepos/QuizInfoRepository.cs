using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models.SQL;

namespace Repository
{
    public class QuizInfoRepository : SQLRepositoryBase<QuizInfo>, IQuizInfoRepository
    {
        public QuizInfoRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<QuizInfo> GetAllQuizInfos(bool trackChanges = true) =>
            FindAll(trackChanges).OrderBy(q => q.CreatedAt);

        public QuizInfo GetQuizInfo(Guid id, bool trackChanges = true) =>
            FindByCondition(q => q.Id == id, trackChanges).SingleOrDefault();
        
    }
}
