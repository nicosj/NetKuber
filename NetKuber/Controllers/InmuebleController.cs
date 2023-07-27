using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NetKuber.Data.Inmuebles;
using NetKuber.Dtos.InmuebleDtos;
using NetKuber.Middleware;
using NetKuber.Models;

namespace NetKuber.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InmuebleController
{
    private readonly IInmuebleRepository _repository;
    private IMapper _mapper;
    public InmuebleController(IInmuebleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InmuebleResponseDto>>> GetAllInmuebles()
    {
        var inmuebles =await _repository.GetAllInmuebles();
        return new JsonResult(_mapper.Map<IEnumerable<InmuebleResponseDto>>(inmuebles));
    }
    [HttpGet("{id}",Name="GetInmuebleById")]
    public async Task<ActionResult<InmuebleResponseDto>> GetInmuebleById(int id)
    {
        var inmueble =await _repository.GetInmuebleById(id);
        if (inmueble is null)
        {
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = $"El inmueble no existe{id}" });
        }
        return new JsonResult(_mapper.Map<InmuebleResponseDto>(inmueble));
    }
    [HttpPost]
    public async Task<ActionResult<InmuebleResponseDto>> CreateInmueble([FromBody] InmuebleRequestDto request)
    {
        var inmueble = _mapper.Map<Inmueble>(request);
        await _repository.CreateInmueble(inmueble);
        if ((await _repository.SaveChanges()))
        {
            //var inmres= _mapper.Map<InmuebleResponseDto>(inmueble);
            //return CreatedAtRoute(nameof(GetInmuebleById), new {inmres.id}, inmres);
            return new JsonResult(_mapper.Map<InmuebleResponseDto>(inmueble));
        }
        throw new MiddlewareException(HttpStatusCode.BadRequest, new { mensaje = "No se pudo crear el inmueble" });
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteInmueble(int id)
    {
        var inmueble = await _repository.GetInmuebleById(id);
        if (inmueble is null)
        {
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = $"El inmueble no existe{id}" });
        }
        _repository.DeleteInmueble(inmueble);
        if (await _repository.SaveChanges())
        {
            return new JsonResult(_mapper.Map<InmuebleResponseDto>(inmueble));
        }
        throw new MiddlewareException(HttpStatusCode.BadRequest, new { mensaje = "No se pudo eliminar el inmueble" });
    }

}