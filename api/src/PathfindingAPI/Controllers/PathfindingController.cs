﻿using Application.Interfaces;
using Contracts;
using Contracts.Request;
using Contracts.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathfindingAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class PathfindingController : ControllerBase
    {
        private readonly IPathfindingService _pathfindingService;
        private readonly IPathfindingRequestMapper _requestMapper;
        private readonly IPathfindingResponseMapper _responseMapper;

        public PathfindingController(IPathfindingService pathfindingService,
            IPathfindingRequestMapper requestMapper,
            IPathfindingResponseMapper responseMapper)
        {
            _pathfindingService = pathfindingService;
            _requestMapper = requestMapper;
            _responseMapper = responseMapper;
        }

        [HttpPost(ApiRoutes.PathfindingRoutes.BreadthFirstSearch)]
        [ProducesResponseType(typeof(PathfindingResponseDto), 200)]
        [ProducesResponseType(typeof(ValidationErrorResponseDto), 400)]
        public IActionResult BreadthFirstSearch(PathfindingRequestDto pathfindingRequestDto)
        {
            var errors = ValidateRequest(pathfindingRequestDto);

            if (errors.Any())
            {
                return BadRequest(new ValidationErrorResponseDto
                {
                    ErrorMessages = errors
                });
            }

            var grid = _requestMapper.MapPathfindingRequestDto(pathfindingRequestDto);

            var pathfindingResult = _pathfindingService.BreadthFirstSearch(grid);

            return Ok(_responseMapper.MapPathfindingResult(pathfindingResult));
        }

        [HttpPost(ApiRoutes.PathfindingRoutes.Dijkstra)]
        [ProducesResponseType(typeof(PathfindingResponseDto), 200)]
        [ProducesResponseType(typeof(ValidationErrorResponseDto), 400)]
        public IActionResult Dijkstra(PathfindingRequestDto pathfindingRequestDto)
        {
            var errors = ValidateRequest(pathfindingRequestDto);

            if (errors.Any())
            {
                return BadRequest(new ValidationErrorResponseDto
                {
                    ErrorMessages = errors
                });
            }

            var grid = _requestMapper.MapPathfindingRequestDto(pathfindingRequestDto);

            var pathfindingResult = _pathfindingService.Dijkstra(grid);

            return Ok(_responseMapper.MapPathfindingResult(pathfindingResult));
        }

        private static IEnumerable<string> ValidateRequest(PathfindingRequestDto pathfindingRequestDto)
        {
            var validator = new PathfindingRequestDtoValidator();
            var validationResult = validator.Validate(pathfindingRequestDto);

            return validationResult.Errors.Select(error => error.ErrorMessage);
        }
    }
}
