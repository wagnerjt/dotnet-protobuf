﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Dotnet.Proto.Models;

namespace Dotnet.Proto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
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
