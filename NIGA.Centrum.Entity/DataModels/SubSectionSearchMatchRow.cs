namespace NIGA.Centrum.Entity.DataModels
{
    public class SubSectionSearchMatchRow
    {
        public long SubSectionId { get; set; }
        public string SubSectionName { get; set; }
        public int? ParentSubSectionId { get; set; }
        public int Rank { get; set; }
    }
}
