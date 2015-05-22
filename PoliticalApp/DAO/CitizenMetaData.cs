using System.ComponentModel.DataAnnotations;

namespace Server.PoliticalAppDataEntities
{
    [MetadataType(typeof(ICitizenMetaData))]
    public partial class Citizen
    {
    }

    internal interface ICitizenMetaData
    {
        [Required(ErrorMessage = "Citizen Name is required")]
        string CitizenName { get; set; }
    }
}
