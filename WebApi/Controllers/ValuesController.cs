using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Dotnet.Proto.Models;
using Google.Protobuf.Collections;

namespace Dotnet.Proto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<Repeats> Get()
        {
            var repeats = new Repeats();
            repeats.Num.AddRange(new List<int> { 5, 4, 3, 2, 1 });
            return repeats;
        }

        // GET api/values/proto
        [HttpGet("proto")]
        public ActionResult<Person> GetProto()
        {
            Person p = new Person{
                Id = 1,
                Email = "someemail@someplace.com",
                Name = "github@wagnerjt"
            };

            Console.WriteLine(p.CalculateSize());
            return p;
        }

        [HttpPost("person")]
        public ActionResult<Person> DoNothingButReturn(Person p)
        {
            return p;
        }
    }
}
