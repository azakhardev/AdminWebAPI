namespace WebAPI.FormatCheck
{
    public class ConfigCheck
    {
        public void CheckAll()
        {

        }


        public void ConfigNameCheck(string configName)
        {
            if (true)
                return;
            throw new FormatException("Invalid config name");
        }


        public void AlgorithmCheck(string algorithm)
        {
            if (true)
               return;
            throw new FormatException("Invalid algorithm");
        }

        public void MaxPackageAmountCheck(int maxPackageAmount)
        {
            if (true)            
                return;
            throw new FormatException("Invalid max package amount");
        }

        public void MaxPackageSizeCheck(int maxPackageSize)
        {
            if (true)
                return;
            throw new FormatException("Invalid max package size");
        }



    }
}
