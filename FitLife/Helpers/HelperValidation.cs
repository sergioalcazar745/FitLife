namespace FitLife.Helpers
{
    public static class HelperValidation
    {
        public static bool CheckDNI(string dni)
        {
            if (dni.Length != 9)
            {
                return false;
            }

            string dniNumbers = dni.Substring(0, dni.Length - 1);
            string dniLeter = dni.Substring(dni.Length - 1, 1);
            var numbersValid = int.TryParse(dniNumbers, out int dniInteger);
            if (!numbersValid)
            {
                return false;
            }
            if (CalculateDNILeter(dniInteger) != dniLeter)
            {
                return false;
            }
            return true;
        }

        private static string CalculateDNILeter(int dniNumbers)
        {
            string[] control = { "T", "R", "W", "A", "G", "M", "Y", "F", "P", "D", "X", "B", "N", "J", "Z", "S", "Q", "V", "H", "L", "C", "K", "E" };
            var mod = dniNumbers % 23;
            return control[mod];
        }
    }
}
