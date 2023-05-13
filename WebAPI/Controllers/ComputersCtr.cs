using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using WebAPI.FormatCheck;
using WebAPI.Tables;
using WebAPI.Tables.Help_Tables;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Computers")]
    [ApiController]
    public class ComputersCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();
        ComputerCheck checkComputer = new ComputerCheck();

        // Všechny počítače
        [HttpGet]
        public IEnumerable<ComputersTb> Get()
        {
            return dbBackup.Computers.Include(x => x.ComputersConfigs).Include(x => x.ComputersGroups).Include(x => x.MacAddresses).Include(x => x.ComputersGroups);
        }

        // Určitý počítač
        [HttpGet("{id}")]
        public ComputersTb Get(int id)
        {
            return dbBackup.Computers.Include(x => x.ComputersConfigs).Include(x => x.ComputersGroups).Include(x => x.MacAddresses).Include(x => x.ComputersGroups).Where(x => x.ID == id).FirstOrDefault();
        }

        // Všechny configy pro určitý počítač
        [HttpGet("{computerId}/Configs")]
        public List<ConfigsTb> GetConfigs(int computerId)
        {
            List<ConfigsTb> configs = new List<ConfigsTb>();

            foreach (var config in dbBackup.Computers.Find(computerId).GetConfigs(computerId, dbBackup))
            {
                configs.Add(config);
            }

            foreach (var computersGroups in dbBackup.ComputersGroups.Where(x => x.ComputerID == computerId))
            {
                foreach (var groupConfig in dbBackup.Groups.Find(computersGroups.ID).GetConfigs(computersGroups.ID, dbBackup))
                {
                    configs.Add(groupConfig);
                }
            }

            return configs;
        }

        // Všechny skupiny pro určitý počítač
        [HttpGet("{id}/Groups")]
        public List<GroupsTb> GetGroups(int id)
        {
            return dbBackup.Computers.Find(id).GetGroups(id, dbBackup);
        }

        // Všechny Logy pro určitý počítač a config
        [HttpGet("{computerId}/{configId}/Logs")]
        public List<LogsTb> GetLogs(int computerId, int configId)
        {
            return dbBackup.Computers.Find(computerId).GetLogs(computerId, configId, dbBackup);
        }


        // Všechny snapshoty pro určitý počítač
        [HttpGet("{id}/Snapshots")]
        public List<string> GetSnapshots(int id)
        {
            return dbBackup.Computers.Find(id).GetSnapshots(id, dbBackup);
        }


        // Všechny Mac-Adresy pro určitý počítač
        [HttpGet("{id}/MacAddresses")]
        public List<string> GetMacAddresses(int id)
        {
            return dbBackup.Computers.Find(id).GetMacAddresses(id, dbBackup);
        }

        // Přidání nového počítače
        [HttpPost]
        public ActionResult<int> Post([FromBody] ComputersTb computer)
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

            return computer.ID;
        }

        // Změna počítače
        [HttpPut("{id}")]
        public ActionResult<ComputersTb> Put(int id, [FromBody] ComputersTb computer)
        {
            ComputersTb updatedComputer = this.dbBackup.Computers.Find(id);

            if (computer.ComputerName != null)
                updatedComputer.ComputerName = computer.ComputerName;
            if (computer.BackupStatus != null)
                updatedComputer.BackupStatus = computer.BackupStatus;
            if (computer.ComputerStatus != null)
                updatedComputer.ComputerStatus = computer.ComputerStatus;
            if (computer.Description != null)
                updatedComputer.Description = computer.Description;
            if (computer.LastBackup != null)
                updatedComputer.LastBackup = computer.LastBackup;
            if (computer.ComputersConfigs != null)
                updatedComputer.ComputersConfigs = computer.ComputersConfigs;
            if (computer.ComputersGroups != null)
                updatedComputer.ComputersGroups = computer.ComputersGroups;
            if (computer.MacAddresses != null)
                updatedComputer.MacAddresses = computer.MacAddresses;

            try
            {
                checkComputer.CheckAll(updatedComputer);
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            dbBackup.SaveChanges();
            return updatedComputer;
        }

        // Změna snapshotu
        [HttpPut("Snapshot")]
        public ActionResult<string> PutSnapshot([FromBody] SnapshotPut snapshot)
        {
            ComputersConfigsTb computersConfigs = dbBackup.ComputersConfigs.Where(x => x.ComputerID == snapshot.ComputerID).Where(x => x.ConfigID == snapshot.ConfigID).FirstOrDefault();
            computersConfigs.Snapshot = snapshot.Snapshot;
            dbBackup.SaveChanges();

            return snapshot.Snapshot;
        }

        // Odstranění určitého počítače
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                dbBackup.Computers.Remove(dbBackup.Computers.Find(id));
                dbBackup.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            return "Computer deleted succesfully.";
        }

        // Odstranění určité Mac-Adresy
        [HttpDelete("{id}/MacAddress/{macId}")]
        public ActionResult<string> DeleteMacAddress(int id, int macId)
        {
            try
            {
                dbBackup.MacAddresses.Remove(dbBackup.MacAddresses.Where(x => x.ComputerID == id).Where(x => x.ID == macId).FirstOrDefault());
                dbBackup.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            return "MacAddress deleted succesfully.";
        }
    }
}
