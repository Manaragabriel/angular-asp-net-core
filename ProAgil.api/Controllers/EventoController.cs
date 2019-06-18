using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProAgil.Repository;
using ProAgil.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ProAgil.api.dto;
using System.IO;
using System.Net.Http.Headers;

namespace ProAgil.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController:ControllerBase
    {
        private readonly IProAgilRepository _repo;
        private readonly IMapper _mapper;
        public EventoController(IProAgilRepository repo,IMapper mapper)
        {
            _mapper= mapper;
            _repo= repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try{
                var ret= await _repo.GetAllEventosAsync(true);
                var resultados= _mapper.Map<IEnumerable<EventoDTO>>(ret);
                return Ok(resultados);
            }catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Erro banco");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try{
                var ret= await _repo.GetAsyncEventoById(id,true);
                return Ok(ret);
            }catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Erro banco");
            }
        }
        [HttpGet("gettema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try{
                var ret= await _repo.GetAllEventosByTema(tema,true);
                return Ok(ret);
            }catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Erro banco");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDTO model)
        {
            try{
               
                var evento= _mapper.Map<Evento>(model);
                _repo.Add(evento);
                if(await _repo.SaveChangesAsync()){
                    return Created($"/api/evento/{model.id}",model);
                }
            }catch(System.Exception ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"{ex}");
            }
            return BadRequest();
        }
        [HttpPost("upload")]
        public async Task<IActionResult> upload(){
            
            try{
                var folder= Path.Combine("Resources","img");
                var path_save= Path.Combine(Directory.GetCurrentDirectory(),folder);
                if(Request.Form.Files[0].Length > 0){
                    var nome= ContentDispositionHeaderValue.Parse(Request.Form.Files[0].ContentDisposition).FileName;
                    var full_path= Path.Combine(path_save,nome.Replace("\""," ").Trim());
                    using(var stream= new FileStream(full_path,FileMode.Create)){
                        Request.Form.Files[0].CopyTo(stream);
                    }
                }
            }catch(SystemException ex ){
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"{ex}");
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDTO model)
        {
            try{
                var evento= await _repo.GetAsyncEventoById(id,false);
                if(evento==null) return NotFound();
                model.id= id;
                _mapper.Map(model,evento); 
                _repo.Update(evento);
                if(await _repo.SaveChangesAsync()){
                    return Created($"/api/evento/{model.id}",model);
                }
            }catch(System.Exception ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"{ex}");
            }
            return BadRequest();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try{
                var evento= await _repo.GetAsyncEventoById(id,false);
                if(evento==null) return NotFound();
                _repo.Delete(evento);
                if(await _repo.SaveChangesAsync()){
                    return Ok();
                }
            }catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Erro banco");
            }
            return BadRequest();
        }
    }

    }

        

