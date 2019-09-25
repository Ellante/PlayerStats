using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PlayerStats.Controllers;
using PlayerStats.Models;

namespace PlayerStats.Tests
{
	[TestClass]
	public class TestPlayerController
	{
		private PlayerController GetPlayerController()
		{
			//NOTE: Add full path to headtohead.json file here
			var _testPathToDataFile = "";
			var playerController = new PlayerController(_testPathToDataFile);
			playerController.Request = new HttpRequestMessage();
			playerController.Configuration = new HttpConfiguration();
			return playerController;
		}

		[TestMethod]
		public void PlayerController_GetAllPlayers_ShouldReturnPlayers()
		{
			var playerController = GetPlayerController();
			var response = playerController.GetAllPlayers();
			IEnumerable<Player> players;
			Assert.IsTrue(response.TryGetContentValue<IEnumerable<Player>>(out players));
		}

		[TestMethod]
		public void PlayerController_GetPlayer_ShouldFindPlayerOfRequestedId()
		{
			var requestedId = 52;
			var playerController = GetPlayerController();
			var response = playerController.GetPlayer(requestedId);
			Player player;
			Assert.IsTrue(response.TryGetContentValue<Player>(out player));
			Assert.AreEqual(player.id, requestedId);
		}

		[TestMethod]
		public void PlayerController_GetPlayer_ShouldReturn404Error()
		{
			var requestedId = 1;
			var playerController = GetPlayerController();
			var response = playerController.GetPlayer(requestedId);
			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NotFound);
		}
	}
}
