using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using PlayerStats.Models;

namespace PlayerStats.Controllers
{
    public class PlayerController : ApiController
	{
		private string _pathToDataFile;

		public PlayerController()
		{
			_pathToDataFile = $"{HttpContext.Current.Server.MapPath("/Data")}\\headtohead.json";
		}

		public PlayerController(string pathToDataFile)
		{
			_pathToDataFile = pathToDataFile;
		}

		private IEnumerable<Player> GetPlayersFromFile()
		{
			var playerList = new PlayerList();
			using (StreamReader r = new StreamReader(_pathToDataFile))
			{
				string jsonString = r.ReadToEnd();
				playerList = JsonConvert.DeserializeObject<PlayerList>(jsonString);
			}
			return playerList.Players;
		}

		[HttpGet]
		[Route("api/players")]
		public HttpResponseMessage GetAllPlayers()
		{
			var players = GetPlayersFromFile();
			return Request.CreateResponse(players.OrderBy(p => p.id));
		}

		[HttpGet]
		[Route("api/players/{id}")]
		public HttpResponseMessage GetPlayer(int id)
		{
			var players = GetPlayersFromFile();
			var player = players.FirstOrDefault(p => p.id == id);

			if (player == null)
				return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound,
					$"The player of id {id} does not exist.");

			return Request.CreateResponse(player);
		}
	}
}
