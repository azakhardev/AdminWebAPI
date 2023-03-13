using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System.Collections.Generic;
using System.Globalization;
using WebAPI.Tables;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Computers")]
    [ApiController]
    public class ComputersCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();
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
        public void Post(string computerName, string backupStatus, string computerStatus, string description, DateTime lastBackup)
        {
            dbBackup.Computers.Add(new tbComputers()
            {
                ComputerName = computerName,
                BackupStatus = backupStatus,
                ComputerStatus = computerStatus,
                Description = description,
                LastBackup = lastBackup
            });

            dbBackup.SaveChanges();
        }

        // PUT api/<Computers>/5
        [HttpPut("{id}")]
        public void Put(int id, string newComputerName, string newBackupStatus, string newComputerStatus, string newDescription)
        {
            // = dbBackup.Computers.Find(id).ComputerName
            tbComputers updatedComputer = dbBackup.Computers.Find(id);

            updatedComputer.ComputerName = newComputerName;
            updatedComputer.BackupStatus = newBackupStatus;
            updatedComputer.ComputerStatus = newComputerStatus;
            updatedComputer.Description = newDescription;
            //updatedComputer.LastBackup = newLastBackup;

            dbBackup.SaveChanges();
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
