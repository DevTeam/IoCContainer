namespace WpfApp.VewModels
{
    /// <summary>
    /// Design time view model.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class ClockViewModelDesignTime: IClockViewModel
    {
        public string Time { get; } = "00:00:00";

        public string Date { get; } = "01.01.2018";
    }
}
