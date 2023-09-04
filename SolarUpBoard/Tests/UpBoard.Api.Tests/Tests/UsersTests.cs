using Board.Api.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpBoard.Api.Tests.Tests
{
    public class UsersTests : IClassFixture<BoardWebApplicationFactory>
    {
        private readonly BoardWebApplicationFactory _webApplicationFactory;

        public UsersTests(BoardWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }
    }
}
