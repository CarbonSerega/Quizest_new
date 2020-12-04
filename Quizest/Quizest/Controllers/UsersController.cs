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
    public class UsersController : ControllerBase
    {
        private readonly ISQLRepositoryManager repository; 
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public UsersController(ISQLRepositoryManager repository, ILoggerManager logger, IMapper mapper) 
        { 
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }


    }
}
