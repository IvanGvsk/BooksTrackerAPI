using AutoMapper;
using BookTrackerApi.Data;
using BookTrackerApi.DTOs;
using BookTrackerApi.Models;
using BookTrackerApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BooksController : ControllerBase
{
    private readonly ILogger<BooksController> _logger;
    private readonly IRepository<Book> _repo;
    private readonly IMapper _mapper;

    public BooksController(IRepository<Book> repo, IMapper mapper, ILogger<BooksController> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        var books = await _repo.GetAllAsync();
        var userBooks = books.Where(b => b.UserId == userId);
        var result = _mapper.Map<IEnumerable<BookDto>>(userBooks);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var book = await _repo.GetByIdAsync(id);
        if (book == null || book.UserId != GetUserId())
            return NotFound();

        var result = _mapper.Map<BookDto>(book);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookDto dto)
    {
        var book = _mapper.Map<Book>(dto);
        book.UserId = GetUserId();
        await _repo.AddAsync(book);
        var result = _mapper.Map<BookDto>(book);
        return CreatedAtAction(nameof(Get), new { id = book.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] BookDto dto)
    {
        _logger.LogInformation("Обновление книги {Title}", dto.Title);
        var book = await _repo.GetByIdAsync(id);
        if (book == null || book.UserId != GetUserId())
        {
            _logger.LogWarning("Книга с ID {BookId} не найдена", id);
            return NotFound();
        }

        _mapper.Map(dto, book);
        await _repo.UpdateAsync(book);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Удаление книги с ID {BookId}", id);
        var book = await _repo.GetByIdAsync(id);
        if (book == null || book.UserId != GetUserId())
        {
            _logger.LogWarning("Книга с ID {BookId} не найдена", id);
            return NotFound();
        }

        await _repo.DeleteAsync(book);
        return NoContent();
    }
}
