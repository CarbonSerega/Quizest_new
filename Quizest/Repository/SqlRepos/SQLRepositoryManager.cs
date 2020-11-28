using Contracts;
using Entities;

namespace Repository
{
    public class SQLRepositoryManager : ISQLRepositoryManager
    {
        private readonly RepositoryContext repositoryContext;

        private IUserRepository userRepository;
        private IQuizInfoRepository quizInfoRepository;
        private IAnswerInfoRepository answerInfoRepository;

        public SQLRepositoryManager(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(repositoryContext);

                return userRepository;
            }
        }

        public IQuizInfoRepository QuizInfoRepository
        {
            get
            {
                if (quizInfoRepository == null)
                    quizInfoRepository = new QuizInfoRepository(repositoryContext);

                return quizInfoRepository;
            }
        }

        public IAnswerInfoRepository AnswerInfoRepository
        {
            get
            {
                if (answerInfoRepository == null)
                    answerInfoRepository = new AnswerInfoRepository(repositoryContext);

                return answerInfoRepository;
            }
        }

        public void Save() => repositoryContext.SaveChanges();

    }
}
