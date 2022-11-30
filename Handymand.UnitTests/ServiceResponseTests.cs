using Handymand.Models.DTOs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handymand.UnitTests
{
	[TestFixture]
	public class ServiceResponseTests
	{
		[Test]
		public void ServiceResponse_EncryptAndDecryptString_ActAsExpected()
		{
			// Arrange

			string key = "7061737323313233";
			var sr =  new ServiceResponse<bool>();

			byte[] iv = sr.GenerateIv();

			string forEncryption = "testamCriptarea";

			// Act

			var ecryptedString = sr.EncryptString(forEncryption,key, iv);

			var decryptedString = sr.DecryptStringAES(ecryptedString, key, iv);
			// Assert

			Assert.That(decryptedString.Equals(forEncryption));

		}
	}
}
