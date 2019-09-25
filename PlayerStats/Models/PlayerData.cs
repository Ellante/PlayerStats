using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayerStats.Models
{
	public class PlayerData
	{
		public int rank { get; set; }
		public int points { get; set; }
		public int weight { get; set; }
		public int height { get; set; }
		public int age { get; set; }
		public int[] last { get; set; }
	}
}