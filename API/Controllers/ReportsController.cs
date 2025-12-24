using Application.IntegrationEvents;
using Application.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ICacheService _cache;

        public ReportsController(IPublishEndpoint publishEndpoint, ICacheService cache)
        {
            _publishEndpoint = publishEndpoint;
            _cache = cache;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestReport([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required.");

            string cacheKey = $"report_lock:{email.Trim().ToLower()}";

            var lastRequest = await _cache.GetAsync<string>(cacheKey);

            if (!string.IsNullOrEmpty(lastRequest))
            {
                return StatusCode(429, new
                {
                    message = "You already requested a report. Try again in 24 hours."
                });
            }

            var eventMessage = new ReportRequestedEvent
            {
                RequestId = Guid.NewGuid(),
                UserEmail = email,
                ReportType = "PDF",
                RequestedAt = DateTime.UtcNow
            };

            await _publishEndpoint.Publish(eventMessage);

            await _cache.SetAsync(cacheKey, DateTime.UtcNow.ToString(), TimeSpan.FromHours(24));

            return Accepted(new { 
                requestId = eventMessage.RequestId,
                message = "Request received. The report will be sent by email."
            });
        }
    }
}
