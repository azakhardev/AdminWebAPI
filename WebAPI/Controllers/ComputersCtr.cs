using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using WebAPI.FormatCheck;
using WebAPI.Tables;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Computers")]
    [ApiController]
    public class ComputersCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();
        ComputerCheck checkComputer = new ComputerCheck();
        // GET: api/<Computers>
        [HttpGet]
        public IEnumerable<tbComputers> Get()
        {
            return dbBackup.Computers;
        }

        // GET api/<Computers>/5
        [HttpGet("{id}")]
        public tbComputers Get(int id)
        {
            return dbBackup.Computers.Find(id);
        }

        // POST api/<Computers>
        [HttpPost]
        public ActionResult<tbComputers> Post([FromBody] tbComputers computer)
        {
            try
            {
                checkComputer.ChechkAll(computer);
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            dbBackup.Computers.Add(computer);
            dbBackup.SaveChanges();

            return computer;
        }

        // PUT api/<Computers>/5
        [HttpPut("{id}")]
        public ActionResult<tbComputers> Put(int id, [FromBody] tbComputers computer)
        {
            tbComputers updatedComputer = this.dbBackup.Computers.Find(id);

            try
            {
                checkComputer.ChechkAll(computer);
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            updatedComputer.ComputerName = computer.ComputerName;
            updatedComputer.BackupStatus = computer.BackupStatus;
            updatedComputer.ComputerStatus = computer.ComputerStatus;
            updatedComputer.Description = computer.Description;
            updatedComputer.LastBackup = computer.LastBackup;
            dbBackup.SaveChanges();

            return updatedComputer;
        }

        // DELETE api/<Computers>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dbBackup.Computers.Remove(dbBackup.Computers.Find(id));
            dbBackup.SaveChanges();
        }
    }
}
