using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using WebAPI.Tables;

namespace WebAPI.FormatCheck
{
    public class ConfigCheck
    {
        public void CheckAll(ConfigsTb config)
        {
            ConfigNameCheck(config);
            AlgorithmCheck(config);
            MaxPackageAmountCheck(config);
            MaxPackageSizeCheck(config);
            ScheduleCheck(config);
        }

        public void ConfigNameCheck(ConfigsTb config)
        {
            if (Regex.IsMatch(config.ConfigName, @"^[A-Za-z0-9_]{3,50}$"))
                return;
            throw new FormatException("Invalid config name.");
        }

        public void AlgorithmCheck(ConfigsTb config)
        {
            if (Regex.IsMatch(config.Algorithm, @"^Full$|^Incremental$|^Differential$"))
                return;
            throw new FormatException("Invalid algorithm. Please selecet Full, Incremental or Differential algorithm.");
        }

        public void MaxPackageAmountCheck(ConfigsTb config)
        {
            if (config.MaxPackageAmount <= 10)
                return;
            throw new FormatException("Maximal package amount can't be greater than 10");
        }

        public void MaxPackageSizeCheck(ConfigsTb config)
        {
            if (config.MaxPackageSize <= 10)
                return;
            throw new FormatException("Maximal package size can't be greater than 10");
        }

        public void ScheduleCheck(ConfigsTb config)
        {
            if (Regex.IsMatch(config.Schedule, @""))
                return;
            throw new FormatException("Invalid schedule");
        }

        //public void CheckSources(tbSources source)
        //{
        //    if (Regex.IsMatch(source.SourcePath, @"^[A-Za-z]:\\(?:[^\\/:*?""<>|\r\n]+\\)*[^\\/:*?""<>|\r\n]*$"))
        //        return;
        //    throw new FormatException("Invalid source");
        //}

        //public void CheckDestiantions(tbDestinations destination)
        //{            
        //    if (Regex.IsMatch(destination.DestinationPath, @"^[A-Za-z]:\\(?:[^\\/:*?""<>|\r\n]+\\)*[^\\/:*?""<>|\r\n]*$"))
        //        return;
        //    throw new FormatException("Invalid destination");

        //}
    }
}
