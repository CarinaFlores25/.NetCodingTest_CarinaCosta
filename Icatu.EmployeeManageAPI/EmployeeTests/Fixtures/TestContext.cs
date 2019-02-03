using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.TestHost;
using Icatu.EmployeeManageAPI;

namespace EmployeeTest
{
    public class TestContext
    {
        public HttpClient Client { get; set; }
        private TestServer _server;
        public TestContext()
        {
            SetupClient();
        }
        private void SetupClient()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = _server.CreateClient();
        }
    }
}

