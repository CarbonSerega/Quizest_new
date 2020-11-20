namespace Contracts
{
    public interface ISQLRepositoryManager
    {
        IUserRepository UserRepository { get; }
        IQuizInfoRepository QuizInfoRepository { get; }
        IAnswerInfoRepository AnswerInfoRepository { get; }
        void Save();
    }
}
