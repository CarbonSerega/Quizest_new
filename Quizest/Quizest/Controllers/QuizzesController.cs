using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.Repos.Mongo;
using Entities.DTO;
using Entities.Models.SQL;
using Microsoft.AspNetCore.Mvc;

namespace Quizest.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class QuizzesController : ControllerBase
    {
        private readonly ISQLRepositoryManager manager;
        private readonly IMongoService mongo;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public QuizzesController(ISQLRepositoryManager manager, IMongoService mongo, ILoggerManager logger, IMapper mapper) 
        { 
            this.manager = manager;
            this.mongo = mongo;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuizInfos()
        {
            var quizInfos = await manager.Repository<QuizInfo>().FindAll();

            var quizInfosDto = mapper.Map<IEnumerable<QuizInfoDto>>(quizInfos);

            return Ok(quizInfosDto);
        }

        [HttpGet("{id}", Name = "QuizInfoById")]
        public async Task<IActionResult> GetQuizInfoById(Guid id)
        {
            var quizInfo = await manager.Repository<QuizInfo>().FindBy(q => q.Id == id);

            if (quizInfo == null) 
            { 
                logger.LogInfo($"Quiz with id {id} doesn't exist."); 
                return NotFound($"Quiz with id {id} doesn't exist"); 
            }

            var quizInfoDto = mapper.Map<QuizInfoDto>(quizInfo); 

            return Ok(quizInfoDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromBody] QuizInfoForCreationDto inputQuizInfo)
        {
            if(inputQuizInfo == null)
            {
                logger.LogError($"{nameof(QuizInfoForCreationDto)} object sent from client is null.");
                return BadRequest("Quiz data cannot be empty");
            }

            var quizInfoEntity = mapper.Map<QuizInfo>(inputQuizInfo);

            quizInfoEntity.CreatedAt = DateTime.Now;

            var temporaryLink = await manager.Repository<TemporaryLink>().Create(new TemporaryLink
            {
                Link = "http://test-link"
            });

            if(temporaryLink == null)
            {
                logger.LogError($"The field TemporaryLink of {nameof(QuizInfoForCreationDto)} is already exists.");
                return BadRequest("This tamporary link is already exists");
            }

            quizInfoEntity.TemporaryLink = temporaryLink.Entity;

            if(quizInfoEntity.OwnerId == null)
            {
                logger.LogError($"The Owner of {nameof(QuizInfoForCreationDto)} entity is null.");
                return BadRequest("Owner cannot be null");
            }

            quizInfoEntity.Owner = await manager.Repository<User>()
                .FindBy(u => u.Id == quizInfoEntity.OwnerId.Value);

            string mongoId = mongo.Create();

            if(string.IsNullOrEmpty(mongoId))
            {
                logger.LogError($"The Mongo Id of {nameof(QuizInfoForCreationDto)} entity and related quiz is null.");
                return BadRequest("Can not create a Quiz entity in MongoDB");
            }

            quizInfoEntity.QuizId = mongoId;

            await manager.Repository<QuizInfo>().Create(quizInfoEntity);
            manager.Save();

            var result = mapper.Map<QuizInfoForOwnerDto>(quizInfoEntity);

            return CreatedAtRoute("QuizInfoById", new { id = result.Id }, result);
        }
    }
}
