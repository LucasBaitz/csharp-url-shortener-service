using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.DTOs;
using UrlShortener.Application.Intefaces;

namespace UrlShortener.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;
        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _urlService.GetAllUrls());
        }

        [HttpGet]
        [Route("{shortenedUrl}")]
        public async Task<IActionResult> GoToUrl(string shortenedUrl)
        {
            string url = await _urlService.GetUrlByShortenedUrl(shortenedUrl);

            return Redirect(url);
        }

        [HttpGet]
        [Route("{id}/Data")]
        public async Task<IActionResult> GetShortUrlInfo(Guid id)
        {
            var urlData = await _urlService.GetUrlById(id);

            return Ok(urlData);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateShortUrl(CreateShortUrlDTO urlDto)
        {
            var createdUrl = await _urlService.CreateShortUrl(urlDto);

            return CreatedAtAction(nameof(GetShortUrlInfo), new { id = createdUrl.Id }, createdUrl);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            await _urlService.DeleteUrl(id);
            return NoContent();
        }
    }
}
