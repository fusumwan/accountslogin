namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils
{
    public static class ByteConverter
    {
        public static byte[] ByteArrayTobyteArray(int[] byteArray)
        {
            byte[] result = new byte[byteArray.Length];
            for (int i = 0; i < byteArray.Length; i++)
            {
                result[i] = (byte)byteArray[i];
            }
            return result;
        }
    }

}
