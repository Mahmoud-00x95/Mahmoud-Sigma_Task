using System.Threading.Tasks;
using Common.DataContexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class Tests
    {
        private CSVContext _context = new CSVContext();
        [TestMethod]
        public async Task IsAddCandidate()
        {
            int res = await _context.InsertOrUpdateCSVCandidate(new Core.Entities.Candidate() 
            { Email = "test@example.com" , FirstName="A", LastName="B", TextComment = "Some random text"  });

            var candidate = await _context.GetCandidateByEmailAddress("test@example.com");

            Assert.AreEqual(1, res);
            Assert.AreEqual("test@example.com", candidate?.Email);
        }

        [TestMethod]
        public async Task IsUpdateCandidate()
        {
            // First call
            await _context.InsertOrUpdateCSVCandidate(new Core.Entities.Candidate()
                { Email = "test@example.com", FirstName = "A", LastName = "B", TextComment = "Some random text" });

            // Second call with different data in order to update
            await _context.InsertOrUpdateCSVCandidate(new Core.Entities.Candidate() 
            { Email = "test@example.com", FirstName = "(Updated)A", LastName = "(Updated)B", TextComment = "(Updated)Some random text" });

            var candidate = await _context.GetCandidateByEmailAddress("test@example.com");
        

            Assert.AreEqual("(Updated)A", candidate?.FirstName);
            Assert.AreEqual("(Updated)B", candidate?.LastName);
            Assert.AreEqual("(Updated)Some random text", candidate?.TextComment);
        }

    }
}