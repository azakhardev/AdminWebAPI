using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            return dbBackup.Computers.Include(x => x.ComputersConfigs).Include(x => x.ComputersGroups).Include(x => x.MacAddresses).Include(x => x.ComputersGroups);
        }

        // GET api/<Computers>/5
        [HttpGet("{id}")]
        public tbComputers Get(int id)
        {            
            return dbBackup.Computers.Include(x => x.ComputersConfigs).Include(x => x.ComputersGroups).Include(x => x.MacAddresses).Include(x => x.ComputersGroups).Where(x => x.ID == id).FirstOrDefault();
        }

        //GET api/<Computers>/5/<Configs>
        [HttpGet("{id}/Configs")]
        public List<string> GetConfigs(int id)
        {
            return dbBackup.Computers.Find(id).GetConfigs(id, dbBackup);
        }

        ////GET api/<Computers>/5/<Groups>
        //[HttpGet("{id}/Groups")]
        //public List<string> GetGroups(int id)
        //{
        //    return dbBackup.Computers.Find(id).GetGroups(id, dbBackup);
        //}

        [HttpGet("{id}/Logs")]
        public List<string> GetLogs(int id)
        {
            return dbBackup.Computers.Find(id).GetLogs(id, dbBackup);
        }

        //GET api/<Computers>/5/<MacAdresses>
        [HttpGet("{id}/MacAddresses")]
        public List<string> GetMacAddresses(int id)
        {
            return dbBackup.Computers.Find(id).GetMacAddresses(id, dbBackup);
        }

        // POST api/<Computers>
        [HttpPost]
        public ActionResult<tbComputers> Post([FromBody] tbComputers computer)
        {
            try
            {
                checkComputer.CheckAll(computer);
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
                checkComputer.CheckAll(computer);
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
