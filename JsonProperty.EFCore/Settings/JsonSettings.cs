namespace JsonProperty.EFCore.Settings
{
    public static class JsonSettings
    {
        private static bool _strictTypeSerialization = true;
        private static bool _settingsUsed = false;
        public static bool AllowChangeStrictParamAfterItUsed { get; set; } = false;

        public static bool StrictTypeSerialization
        {
            get
            {
                _settingsUsed = true;
                return _strictTypeSerialization;
            }
            set
            {
                if (_settingsUsed && !AllowChangeStrictParamAfterItUsed)
                    throw new Exception($"{nameof(JsonSettings)} were already used to get {nameof(StrictTypeSerialization)} param! Set it before.");
                _strictTypeSerialization = value;
            }
        }
    }
}