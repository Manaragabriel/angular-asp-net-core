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
using Microsoft.AspNetCore.Authorization;

namespace ProAgil.api.Controllers{
   
    [Route("api/[controller]")]
    [ApiController]
    public class LoteController:ControllerBase{

        private readonly IProAgilRepository _repo;
        private readonly IMapper _mapper;
        public LoteController(IProAgilRepository repo, IMapper map){
            _repo= repo;
            _mapper= map;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(Lote lote){
            try{
                _repo.Add(lote);
                await _repo.SaveChangesAsync();
                return this.StatusCode(StatusCodes.Status200OK);
            }catch(System.Exception ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"{ex}");
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id){
            try{
                var model= await _repo.Get_lote(id);
                if(model == null) return NotFound();
                _repo.Delete(model);
                await _repo.SaveChangesAsync();
                return this.StatusCode(StatusCodes.Status200OK);
            }catch(System.Exception ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"{ex}");
            }
            return BadRequest();
        }
    }


}