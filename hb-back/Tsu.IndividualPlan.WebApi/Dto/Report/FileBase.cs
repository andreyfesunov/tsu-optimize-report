namespace Tsu.IndividualPlan.WebApi.Dto.Report
{
    public class FileBase
    {
        /// <summary>
        /// Контент
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; set; }
    }
}
