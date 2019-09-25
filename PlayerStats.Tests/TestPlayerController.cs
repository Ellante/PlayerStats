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
		//NOTE: Add full path to headtohead.json file here
		string _testPathToDataFile = "";

		[TestMethod]
		public void PlayerController_GetAllPlayers_ShouldReturnPlayers()
		{
			var playerController = new PlayerController(_testPathToDataFile);
			playerController.Request = new HttpRequestMessage();
			playerController.Configuration = new HttpConfiguration();

			var response = playerController.GetAllPlayers();
			IEnumerable<Player> players;
			Assert.IsTrue(response.TryGetContentValue<IEnumerable<Player>>(out players));
		}

	}
}
