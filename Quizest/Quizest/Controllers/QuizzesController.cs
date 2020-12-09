using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.Repos.Mongo;
using Entities.DTO;
using Entities.Models.SQL;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace Quizest.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class QuizzesController : ControllerBase
    {
        private readonly ISQLRepositoryManager manager;
        private readonly IMongoService mongo;
        private readonly IMapper mapper;

        public QuizzesController(ISQLRepositoryManager manager, IMongoService mongo, IMapper mapper) 
        { 
            this.manager = manager;
            this.mongo = mongo;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllQuizInfos()
        {
            var quizInfos = manager.Repository<QuizInfo>().FindAll();

            var quizInfosDto = mapper.Map<IEnumerable<QuizInfoDto>>(quizInfos);

            return Ok(quizInfosDto);
        }

        [HttpGet("{id}", Name = "QuizInfoById")]
        public IActionResult GetQuizInfoById(Guid id)
        {
            var quizInfo = manager.Repository<QuizInfo>().FindBy(q => q.Id == id).SingleOrDefault();

            if (quizInfo == null)
            {
                return NotFound(Constants.QuizDoesNotExist(id));
            }

            var quizInfoDto = mapper.Map<QuizInfoDto>(quizInfo);

            return Ok(quizInfoDto);
        }

        [HttpGet("generated/{temporaryLinkParam}")]
        public IActionResult GetQuizByTemporaryLink(string temporaryLinkParam)
        {
            var quizInfo = manager.Repository<QuizInfo>().FindBy(q => q.TemporaryLink.Equals(temporaryLinkParam)).SingleOrDefault();

            if(quizInfo == null)
            {
                return NotFound(Constants.QuizUnderLinkDoesNotExist(temporaryLinkParam));
            }

            var quizInfoDto = mapper.Map<QuizInfoDto>(quizInfo);

            return Ok(quizInfoDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromForm] QuizInfoForCreationDto inputQuizInfo)
        {
            if (inputQuizInfo == null)
            {
                return BadRequest(Constants.QuizDataEmpty);
            }

            var quizInfoEntity = mapper.Map<QuizInfo>(inputQuizInfo);

            if (inputQuizInfo.PreviewImage != null)
            {
                if (!FileUtils.IsPreviewValid(inputQuizInfo.PreviewImage))
                {
                    return BadRequest(Constants.InvalidImage);
                }

                quizInfoEntity.PreviewPath = await FileUtils.SaveAsync(DirType.Previews, inputQuizInfo.PreviewImage);
            }

            string queryParam = RandomGenerator.GenerateHexKey();

            quizInfoEntity.TemporaryLink = queryParam;

            quizInfoEntity.CreatedAt = DateTime.Now;

            if (quizInfoEntity.OwnerId == null)
            {
                return BadRequest(Constants.OwnerNull);
            }

            quizInfoEntity.Owner =  manager.Repository<User>()
                .FindBy(u => u.Id == quizInfoEntity.OwnerId.Value).SingleOrDefault();

            string mongoId = RandomGenerator.GenerateHexKey();

            quizInfoEntity.QuizId = mongoId;

            await manager.Repository<QuizInfo>().Create(quizInfoEntity);

            manager.Save();

            mongoId = mongo.Create(mongoId);

            if (string.IsNullOrEmpty(mongoId))
            {
                return BadRequest(Constants.MongoDbCreationFailure(nameof(Entities.Models.Mongo.Quiz)));
            }

            var result = mapper.Map<QuizInfoForOwnerDto>(quizInfoEntity);

            var request = HttpContext.Request;

            result.TemporaryLink = LinkUtils.GenerateTemporaryLink(request.IsHttps, request.Host.Value, request.Path, queryParam);

            return CreatedAtRoute("QuizInfoById", new { id = result.Id }, result);
        }

        


        [HttpDelete("{id}")]
        public IActionResult DeleteQuiz(Guid id)
        {
            var quiz = manager.Repository<QuizInfo>().FindBy(q => q.Id == id).SingleOrDefault();

            if(quiz == null)
            {
                return NotFound(Constants.QuizDoesNotExist(id));
            }

            FileUtils.Remove(quiz.PreviewPath);

            string mongoId = quiz.QuizId;

            manager.Repository<QuizInfo>().Delete(quiz);

            manager.Save();

            mongo.Remove(mongoId.ToLower());

            return NoContent();
        }
    }
}
