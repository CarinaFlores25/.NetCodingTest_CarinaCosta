using Icatu.EmployeeManageAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Icatu.EmployeeManageAPI.Models;

namespace EmployeeTest
{

    public class EmployeeTest
    {
        private readonly TestContext _testContext;
        private EmployeeItem _item;


        public EmployeeTest()
        {
            _testContext = new TestContext();
            _item = new EmployeeItem { ID = 1, Nome = "Carina", Email = "teste", Departamento = "RH" };
        }

        [Fact]
        public async Task Employee_GetById_ValuesReturnsOkResponse()
        {
            var response = await _testContext.Client.GetAsync("/api/employee/1");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Employee_Post_ValuesReturnsCreatedResponse()
        {
            object data = new
            {
                Nome = "Agent Name",
                Email = "teste2",
                Departamento = "TI"
            };

            var myContent = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _testContext.Client.PostAsync("/api/employee", byteContent);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);

        }

        [Fact]
        public async Task Employee_Delete_ValuesReturnsNoContentResponse()
        {
            var response = await _testContext.Client.DeleteAsync("/api/employee/1");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Employee_GetTodoItens_ValuesReturnsBadRequestResponse()
        {
            var response = await _testContext.Client.GetAsync("/api/employee/a/a");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Employee_GetTodoItens_ValuesReturnsOKResponse()
        {
            var response = await _testContext.Client.GetAsync("/api/employee/1/1");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ValueEmployees_GetById_ReturnsBadRequestResponse()
        {
            var response = await _testContext.Client.GetAsync("/api/employee/XXX");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }

}
