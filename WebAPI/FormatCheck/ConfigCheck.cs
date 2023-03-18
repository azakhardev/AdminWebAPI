using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using WebAPI.Tables;

namespace WebAPI.FormatCheck
{
    public class ConfigCheck
    {
        public void CheckAll(tbConfigs config)
        {
            ConfigNameCheck(config);
            AlgorithmCheck(config);
            MaxPackageAmountCheck(config);
            MaxPackageSizeCheck(config);
            ScheduleCheck(config);
        }

        public void ConfigNameCheck(tbConfigs config)
        {
            if (Regex.IsMatch(config.ConfigName, @"^[A-Za-z0-9_]{3,50}$"))
                return;
            throw new FormatException("Invalid config name.");
        }

        public void AlgorithmCheck(tbConfigs config)
        {
            if (Regex.IsMatch(config.Algorithm, @"^Full$|^Incremental$|^Differential$"))
                return;
            throw new FormatException("Invalid algorithm. Please selecet Full, Incremental or Differential algorithm.");
        }

        public void MaxPackageAmountCheck(tbConfigs config)
        {
            if (config.MaxPackageAmount <= 10)
                return;
            throw new FormatException("Maximal package amount can't be greater than 10");
        }

        public void MaxPackageSizeCheck(tbConfigs config)
        {
            if (config.MaxPackageSize <= 10)
                return;
            throw new FormatException("Maximal package size can't be greater than 10");
        }

        public void ScheduleCheck(tbConfigs config)
        {
            if (Regex.IsMatch(config.Schedule, @"^(*|\d{1,2}|\d{1,2}-\d{1,2}|\d{1,2}/\d{1,2}|\d{1,2},\d{1,2}) (*|\d{1,2}|\d{1,2}-\d{1,2}|\d{1,2}/\d{1,2}|\d{1,2},\d{1,2}) (*|\d{1,2}|\d{1,2}-\d{1,2}|\d{1,2}/\d{1,2}|\d{1,2},\d{1,2}) (*|\d{1,2}|\d{1,2}-\d{1,2}|\d{1,2}/\d{1,2}|\d{1,2},\d{1,2}) (*|\d{1,2}|\d{1,2}-\d{1,2}|\d{1,2}/\d{1,2}|\d{1,2},\d{1,2})$"))
                return;
            throw new FormatException("Invalid schedule");
        }
    }
}
