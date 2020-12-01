using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Quizest.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class QuizzesController : ControllerBase
    {
        private readonly ISQLRepositoryManager repository; 
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public QuizzesController(ISQLRepositoryManager repository, ILoggerManager logger, IMapper mapper) 
        { 
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllQuizInfos()
        {
            var quizInfos = repository.QuizInfoRepository.GetAllQuizInfos();

            var quizInfosDto = mapper.Map<IEnumerable<QuizInfoDto>>(quizInfos);

            return Ok(quizInfosDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetQuizInfoById(Guid id)
        {
            var quizInfo = repository.QuizInfoRepository.GetQuizInfo(id);

            if (quizInfo == null) 
            { 
                logger.LogInfo($"Quiz with id: {id} doesn't exist."); 
                return NotFound(); 
            }

            var quizInfoDto = mapper.Map<QuizInfoDto>(quizInfo); 

            return Ok(quizInfoDto);
        }
    }
}
