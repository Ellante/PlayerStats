using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
			var testPathToDataFile = "";
			var playerController = new PlayerController(testPathToDataFile);
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
		public void PlayerController_GetPlayer_ShouldReturn404ErrorForNonExistentPlayer()
		{
			var requestedId = 1;
			var playerController = GetPlayerController();
			var response = playerController.GetPlayer(requestedId);
			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NotFound);
		}

		[TestMethod]
		public void PlayerController_DeletePlayer_ShouldDeletePlayerFromPlayerList()
		{
			var toDeleteId = 95;
			var playerController = GetPlayerController();

			var getToDeletePlayerResponse = playerController.GetPlayer(toDeleteId);
			Assert.IsTrue(getToDeletePlayerResponse.StatusCode == System.Net.HttpStatusCode.OK);

			var deletePlayerResponse = playerController.DeletePlayer(toDeleteId);
			Assert.IsTrue(deletePlayerResponse.StatusCode == System.Net.HttpStatusCode.OK);

			var getDeletedPlayerResponse = playerController.GetPlayer(toDeleteId);
			Assert.IsTrue(getDeletedPlayerResponse.StatusCode == System.Net.HttpStatusCode.NotFound);
		}

		[TestMethod]
		public void PlayerController_DeletePlayer_ShouldReturn404ErrorForNonExistentPlayer()
		{
			var toDeleteId = 1;
			var playerController = GetPlayerController();
			var response = playerController.DeletePlayer(toDeleteId);
			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NotFound);
		}
	}
}
