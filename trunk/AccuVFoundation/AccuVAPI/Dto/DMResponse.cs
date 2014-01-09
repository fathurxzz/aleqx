namespace AccuVAPI.Dto
{
    public class DmResponse
    {
        public const string CODE_OK = "DM-0000";
        public const string CODE_GENERIC_ERROR = "DM-0100";

        public string Code { get; set; }
        public string Description { get; set; }
        public object Result { get; set; }
    }

    public class DmResponse<T> : DmResponse
    {
        public new T Result { get; set; }
    }
}