using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.Repos.Mongo;
using Entities.DTO;
using Entities.Models.SQL;
using Microsoft.AspNetCore.JsonPatch;
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
            /* To do: Create temp user and link the quiz info with it */

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

            /*Will be changed in the future accroding to User Identity */
            quizInfoEntity.Owner = manager.Repository<User>().FindBy(u => u.Id == quizInfoEntity.OwnerId).SingleOrDefault();

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

            // It will be changed according to the User Identity
            result.HasAccessToEdit = true;

            return CreatedAtRoute("QuizInfoById", new { id = result.Id }, result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(Guid id, [FromForm] QuizInfoForCreationDto updatedQuizInfo)
        {
            if (updatedQuizInfo == null)
            {
                return BadRequest(Constants.QuizDataEmpty);
            }

            var oldQuizInfo = manager.Repository<QuizInfo>().FindBy(q => q.Id == id).SingleOrDefault();

            if (oldQuizInfo == null)
            {
                return BadRequest(Constants.QuizDoesNotExist(id));
            }

            var quizInfoEntity = mapper.Map<QuizInfo>(updatedQuizInfo);

            if (updatedQuizInfo.PreviewImage != null)
            {
                if (!FileUtils.IsPreviewValid(updatedQuizInfo.PreviewImage))
                {
                    return BadRequest(Constants.InvalidImage);
                }

                quizInfoEntity.PreviewPath = await FileUtils.UpdateAsync(DirType.Previews, oldQuizInfo.PreviewPath, updatedQuizInfo.PreviewImage);
            }
            else
            {
                FileUtils.Remove(oldQuizInfo.PreviewPath);
                quizInfoEntity.PreviewPath = string.Empty;
            }

            //Possible replacements in the future

            quizInfoEntity.Id = id;

            quizInfoEntity.OwnerId = oldQuizInfo.OwnerId;

            quizInfoEntity.Owner = oldQuizInfo.Owner;

            quizInfoEntity.UpdatedAt = DateTime.Now;

            quizInfoEntity.TemporaryLink = oldQuizInfo.TemporaryLink;

            quizInfoEntity.QuizId = oldQuizInfo.QuizId;

            manager.Detach();

            manager.Repository<QuizInfo>().Update(quizInfoEntity);

            manager.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchRequest(Guid id, [FromBody] JsonPatchDocument<QuizInfoForCreationDto> quizInfoPatch)
        {
            if (quizInfoPatch == null)
            {
                return BadRequest(Constants.QuizDataEmpty);
            }

            var oldQuizInfo = manager.Repository<QuizInfo>().FindBy(q => q.Id == id).SingleOrDefault();

            if (oldQuizInfo == null)
            {
                return BadRequest(Constants.QuizDoesNotExist(id));
            }

            var quizInfoToPatch = mapper.Map<QuizInfoForCreationDto>(oldQuizInfo);

            quizInfoPatch.ApplyTo(quizInfoToPatch);

            mapper.Map(quizInfoToPatch, oldQuizInfo);

            manager.Save();

            return NoContent();
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
