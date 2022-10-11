using Handymand.Repository.DatabaseRepositories;
using Handymand.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Handymand.UnitTests
{
    [TestFixture]
    public class SkillServiceTests
    {
        private Mock<ISkillRepository> _skillRepository;
        [SetUp]
        public void Setup()
        {
            _skillRepository = new Mock<ISkillRepository>();

        }

        [Test]
        public async Task GetAll_WhenNoSkillExists_ReturnNull()
        {

            var skillService = new SkillService(_skillRepository.Object);

           
            var result = await skillService.GetAll();

            Assert.That(result is null);
            
        }
    }
}