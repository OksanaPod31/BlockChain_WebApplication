using BlockchainApp.Persistence;
using BlockchainApp.Web.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainApp.Web.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly IBlockchainDbContext _context;
		private static readonly string[] Summaries = new[]
		{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, IBlockchainDbContext context)
		{
			_logger = logger;
			_context = context;
		}
		public BlockchainApp.Domain.BlockchainModels.Transaction GetBlock()
		{
			return _context.Transactions.FirstOrDefault(x => x.TransactionId == 1);
		}

	}
}