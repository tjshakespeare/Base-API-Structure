using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Base_API_Structure.Data;
using Base_API_Structure.DTOs;
using Base_API_Structure.Models;
using Base_API_Structure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Base_API_Structure.Controllers
{
    // ControllerBase is the MVC Controller parent class without any View section. Used for web APIs
    [Route("api/base")]
    [ApiController]
    public class BaseController : ControllerBase 
    {
        private readonly IDatabaseRepository _repository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public BaseController(IDatabaseRepository repository, IMapper mapper, IFileService fileService) 
        {
            _repository = repository;
            _mapper = mapper;
            _fileService = fileService;
        }

        [HttpGet]
        public ActionResult<BaseReadDTO> GetAll() {
            var items = _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<BaseReadDTO>>(items));
        }

        [HttpGet("{id}", Name="GetById")]
        public ActionResult<BaseReadDTO> GetById(int id) 
        {
            var baseModel = _repository.GetById(id);
            if(baseModel == null) return NotFound();
            return Ok(_mapper.Map<BaseReadDTO>(baseModel));
        }

        [HttpPost]
        public ActionResult<BaseReadDTO> Create(BaseCreateDTO createDto)
        {
            var baseModel = _mapper.Map<BaseModel>(createDto);
            _repository.Create(baseModel);
            _repository.SaveChanges();

            var commandReadDTO = _mapper.Map<BaseReadDTO>(baseModel);

            return CreatedAtRoute(nameof(GetById), new {Id = commandReadDTO.Id}, commandReadDTO);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, BaseUpdateDTO updateDto) 
        {
            var itemFromRepository = _repository.GetById(id);
            if(itemFromRepository == null) return NotFound(); 

            _mapper.Map(updateDto, itemFromRepository);

            _repository.Update(itemFromRepository);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PatchCommand(int id, JsonPatchDocument<BaseUpdateDTO> patchDocument)
        {
            var itemFromRepository = _repository.GetById(id);
            if(itemFromRepository == null) return NotFound(); 

            var itemToPatch = _mapper.Map<BaseUpdateDTO>(itemFromRepository);
            patchDocument.ApplyTo(itemToPatch, ModelState);

            if(!TryValidateModel(itemToPatch)) return ValidationProblem(ModelState);
            
            _mapper.Map(itemToPatch, itemFromRepository);
            _repository.Update(itemFromRepository);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")] 
        public ActionResult DeleteCommand(int id) 
        {
            var itemFromRepository = _repository.GetById(id);
            if(itemFromRepository == null) return NotFound(); 

            _repository.Delete(itemFromRepository);
            _repository.SaveChanges();

            return NoContent();
        }

        // upload file(s) to server that palce under path: rootDirectory/subDirectory
        [HttpPost("upload")]
        public IActionResult UploadFile([FromForm(Name = "files")] List<IFormFile> files, [FromForm(Name = "subDirectory")] string subDirectory)
        {
            try
            {
                _fileService.SaveFile(files, subDirectory);

                return Ok(new { files.Count, Size = _fileService.ConvertSize(files.Sum(f => f.Length)) });
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        // download file(s) to client according path: rootDirectory/subDirectory with single zip file
        [HttpGet("download/{subDirectory}")]
        public IActionResult DownloadFiles(string subDirectory)
        {
            try
            {
                var (fileType, archiveData, archiveName) = _fileService.GetFile(subDirectory);

                return File(archiveData, fileType, archiveName);
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }
    }
}